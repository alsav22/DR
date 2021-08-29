using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Management;
using System.IO.Ports;
using System.Threading;

namespace DR
{
    class RemoteControl
    {
        const string fileNameKeyboardKeys = "listKeyboardKeys.dat";
        public static Dictionary<string, Hotkey> dictionaryKeyboardKeys = new Dictionary<string, Hotkey>();

        const string fileNamePresets = "presets.dat";
        public static List <Preset> listPresets = new List <Preset>();
        
        public static bool flagSettings = false; // флаг работы в режиме настроек
        public static string presetNameBeforeClosing = ""; // имя пресета перед закрытием

        public static Device device;
        public static Settings settings;
        public static Preset preset;
        public static Form1 form;

        public void start()
        {
            device = new Device();

            if (device.ID_VID != "" && device.ID_PID != "")
            {
                if (deviceSearch(ref device))
                {
                    connection(ref device);
                }
                else
                    MessageBox.Show("Device not found!");
            }
            
            form = new Form1(device.portName, device.ID_VID, device.ID_PID);
            form.Show();
            
            if (device.portName != "" && device.portName != null)
            {
                MessageBox.Show("The device is connected and ready for use.");
                
                // обработчик событий, куда буду приходить сообщения с COM порта
                device.serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                
                form.ActiveControl = null;
                //form.WindowState = FormWindowState.Minimized;
            }

            if (device.portName != "") // если устройство найдено и подключено
            {
                if (readKeyboardKeys()) // загрузка из файла списка всех клавиш
                                        // в файле строки типа: Control ^, или PageUp {PgUp},
                                        // т.е., до пробела то, что выводится при нажатии на клавишу,
                                        // после пробела то, что нужно отправлять через SendKeys.
                {
                    settings = new Settings();
                    flagSettings = true;
                    presetNameBeforeClosing = "";
                }
                else
                {
                    MessageBox.Show("File " + fileNameKeyboardKeys + " not found!");
                    return;
                }
                
                if (!loadPresets() || listPresets.Count == 0) // загрузка списка пресетов
                {
                    MessageBox.Show("There are no presets. Create at least one.");
                    
                    preset   = new Preset();
                }
                else
                {
                    preset = listPresets[0];
                    
                    form.PresetNameText = preset.name;
                    presetNameBeforeClosing = preset.name;
                    
                    //settings = new Settings();
                    
                    for (int i = 0; i < listPresets.Count; ++i)
                    {
                        settings.presetName.Items.Add(listPresets[i].name);
                    }
                }
            }
            
        }

        public bool readKeyboardKeys()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream(fileNameKeyboardKeys, FileMode.Open))
                {
                    dictionaryKeyboardKeys = (Dictionary<string, Hotkey>)formatter.Deserialize(fs);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            /////////////////// разово, для первой инициализации словаря с горячими клавишами
            //string fileName = "listKeyboardKeys.txt";
            //try
            //{

            //    using (StreamReader sr = new StreamReader(fileName))
            //    {
            //        string str;
            //        while ((str = sr.ReadLine()) != null)
            //        {

            //            string[] parts = str.Split(' ');
            //            if (parts.Length >= 2)
            //            {
            //                if (parts[0] == "Space")
            //                    parts[1] = " ";
            //                dictionaryKeyboardKeys.Add(parts[0], new Hotkey(parts[0], parts[1]));

            //            }
            //            else
            //            {
            //                dictionaryKeyboardKeys.Add(parts[0], new Hotkey(parts[0], null));

            //            }
            //        }

            //        sr.Close();
            //        writeHotKeys();

            //        return true;
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("File " + fileName + " not found!");
            //    return false;
            //}

        }

        void writeKeyboardKeys()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(fileNameKeyboardKeys, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, dictionaryKeyboardKeys);
            }
        }

        static public bool loadPresets()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream(fileNamePresets, FileMode.Open))
                {
                    listPresets = (List<Preset>)formatter.Deserialize(fs);
                    RemoteControl.flagSettings = false;

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        static public void savePresets()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(fileNamePresets, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, listPresets);
                
                
            }
        }

        static string regex(string pattern, string text) // вспомогательная функция
        {
            Regex re = new Regex(pattern);
            Match m = re.Match(text);
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return "";
            }
        }

        public static bool deviceSearch(ref Device device) // поиск подключенного устройства 
        {
            // Use WMI to get info
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\"");
            string vid = "VID_" + device.ID_VID;
            string pid = "PID_" + device.ID_PID;
            // Search all serial ports
            foreach (ManagementObject queryObj in searcher.Get())
            {
                //// Parse the data
                //if (null != queryObj["Name"])
                //{
                //    Console.WriteLine("Port = " + regex(@"(\(COM\d+\))", queryObj["Name"].ToString()));
                //    //namePort = regex(@"(\(COM\d+\))", queryObj["Name"].ToString());
                //    //namePort = regex(@"(COM\d+)", queryObj["Name"].ToString());
                //}

                //PNPDeviceID = USB\VID_1A86&PID_7523\5&1A63D808&0&2
                if (null != queryObj["PNPDeviceID"])
                {
                    //Console.WriteLine("VID = " + regex("VID_([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString()));
                    //Console.WriteLine("PID = " + regex("PID_([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString()));
                    device.VID = regex("VID_([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString());
                    device.PID = regex("PID_([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString());
                    if (device.VID == vid && device.PID == pid)
                    {
                        if (null != queryObj["Name"])
                        {
                            //Console.WriteLine("Port = " + regex(@"(\(COM\d+\))", queryObj["Name"].ToString()));
                            //namePort = regex(@"(\(COM\d+\))", queryObj["Name"].ToString());
                            device.portName = regex(@"(COM\d+)", queryObj["Name"].ToString());
                            //device.ID_VID = id_vid;
                            //device.ID_PID = id_pid;
                        }
                        break;
                    }
                } // if
            } // foreach

            if (device.VID != vid || device.PID != pid || device.VID == "" || device.PID == "")
            {
                device.portName = "";
                device.ID_VID = "";
                device.ID_PID = "";
                device.VID = "";
                device.PID = "";
                return false;
            }
            return true;

        }

        public static void connection(ref Device device) // подключение устройства к COM порту
        {
            device.serialPort = new SerialPort(device.portName);

            device.serialPort.BaudRate = 9600;
            device.serialPort.Parity = Parity.None;
            device.serialPort.StopBits = StopBits.One;
            device.serialPort.DataBits = 8;
            device.serialPort.Handshake = Handshake.None;
            device.serialPort.RtsEnable = true;
            //serialPort.ReadTimeout = 500;

            //device.serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            device.serialPort.Open();

            //Console.WriteLine("Press any key to continue...");
            //Console.WriteLine();
            //Console.ReadKey();
            //serialPort.Close();
        }

        
        // правильный код, с пульта, приходит в виде или трёх цифр (первая 8), с \r на конце, или в виде
        // двух цифр (которые были после 8), с \r на конце, если толко после 8 не было 0, тогда одна последняя цифра,
        // с \r на конце. Например: или 838\r, или 38\r; или 806\r, или 6\r
        // Следующая функция оставляет за каждой клавишей пульта только один 
        // вариан кода: или две цифры, или одна цифра. Например: если коды 838 или 38,
        // то код от этой клавиши пульта будет всегда 38; если коды 806 или 6, то код
        // всегда будет 6.
        
        private void preparingСode(ref string code) // вспомогательная функция,
                                                    // подготовка кода
        {
            if (code.Length > 4) // поступил с пульта изначально неверный код
            {
                code = "";
                return;
            }
            char[] end = { '\r' };
            char[] beg = { '%', '+', '^' };
            char[] begCode = { '8', '0' };
            code = code.Trim(end);

            if (code.Length == 3 && code[0] == '8')
            {
                code = code.Remove(0, 1);
                if (code[0] == '0')
                    code = code.Remove(0, 1);
            }
        }

        private void settingMode() // режим настроек
        {
            string code = device.serialPort.ReadLine();
            Console.WriteLine(code);
            device.serialPort.DiscardInBuffer();

            preparingСode(ref code);
            
            // (Ставятся перед фигурными скобками)
            // SHIFT +
            // CTRL  ^
            // ALT   %

            // SPACE  " " (в файле пишется SPACE) 
            // При добавлении в словарь, SPACE меняется на строку с пробелом.

            // Буквы передавать в нижнем регистре, иначе 
            // будут восприниматься как с Shift

            // Одиночые символы, кроме специальных (читать по ссылке - какие это),
            // можно передавать без фигурных скобок.

             Console.WriteLine(code);
            
            if (settings != null && !settings.IsDisposed)
            {
                settings.Invoke(new Action(() => { settings.code.Text = code; }));
            }
            
            // читать по ссылке на английском
            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=net-5.0
            
            string hotkey = "";
            try
            {
                hotkey = preset.dictionaryBindings[code].hotkeyTextBox; // получение, из словаря, грячей клавиши по коду
            }
            catch
            {
                hotkey = null; // кода нет в словаре
            }

            if (hotkey == null) // если кода нет в словаре, то строка пустая
                hotkey = "";

            if (settings != null && !settings.IsDisposed)
            {
                settings.Invoke(new Action(() => { settings.hotkey.Text = hotkey; 
                                                   settings.hotkey.Focus();}));
            }

            Thread.Sleep(200);
            if (device.serialPort.IsOpen)
                device.serialPort.DiscardInBuffer();
        }

        private void workingMode() // рабочий режим
        {
            string code = device.serialPort.ReadLine();
            //Console.WriteLine(code);
            device.serialPort.DiscardInBuffer();

            preparingСode(ref code);
            
            // (Ставятся перед фигурными скобками)
            // SHIFT +
            // CTRL  ^
            // ALT   %

            // SPACE  " " (в файле пишется SPACE) 
            // При добавлении в словарь, SPACE меняется на строку с пробелом.

            // Буквы передавать в нижнем регистре, иначе 
            // будут восприниматься как с Shift

            // Одиночые символы, кроме специальных (читать по ссылке - какие это),
            // пишут, что можно передавать без фигурных скобок, но с буквами плохо работает,
            // лучше всё (кроме модификаторов) в фигурных передавать.

            Console.WriteLine(code);

            string hotkey = "";
            try
            {
                hotkey = preset.dictionaryBindings[code].hotkeyForSending;
                //hotkey = "{Enter}";
                SendKeys.SendWait(hotkey);

                // читать по ссылке на английском
                //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=net-5.0
            }
            catch
            {

            }
            
            Thread.Sleep(200);
            if (device.serialPort.IsOpen)
                device.serialPort.DiscardInBuffer();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (flagSettings)
                settingMode();
            else
                workingMode();
        }
    }
}
