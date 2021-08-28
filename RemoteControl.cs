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
                
                device.serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                
                form.ActiveControl = null;
                //form.WindowState = FormWindowState.Minimized;
            }

            if (device.portName != "") // если устройство найдено и подключено
            {

                loadPresets(); // загрузка из файла списка пресетов

                if (!File.Exists(fileNamePresets) || listPresets.Count == 0)
                {
                    MessageBox.Show("There are no presets. Create at least one.");
                    preset = new Preset();
                    settings = new Settings();
                    if (readKeyboardKeys()) // загрузка из файла списка всех горячих клавиш
                                                // в файле строки типа: Control ^, или PageUp {PgUp},
                                                // т.е., до пробела то, что выводится при нажатии на клавишу,
                                                // после пробела то, что нужно отправлять через SendKeys.
                    {
                        flagSettings = true;
                        presetNameBeforeClosing = "";
                        settings.Show();
                        
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    preset = listPresets[0];
                    
                    form.PresetNameText = preset.name;
                    presetNameBeforeClosing = preset.name;
                    
                    settings = new Settings();
                    if (readKeyboardKeys()) // загрузка из файла списка всех клавиш.
                                            // В файле строки, типа: Control ^, или PageUp {PgUp},
                                            // т.е., до пробела то, что выводится при нажатии на клавишу,
                                            // после пробела то, что нужно отправлять через SendKeys.
                    {
                        for (int i = 0; i < listPresets.Count; ++i)
                        {
                            settings.presetName.Items.Add(listPresets[i].name);
                        }
                    }
                    else
                    {
                        return;
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
                MessageBox.Show("File " + fileNameKeyboardKeys + " not found!");
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
                MessageBox.Show("File presets.dat not found!");
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

        private void preparingСode(ref string code) // подготовка кода
        {
            if (code.Length > 4)
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

        void preparingHotkey(ref string hotkeyForOutput)
        {
            if (hotkeyForOutput == " ")
            {
                hotkeyForOutput = "Space";
                return;
            }
            if (hotkeyForOutput == "") // если код есть в словаре, но не привязан к горячей клавише, то
                                       // в строке пишется "NO BINDING".
            {
                hotkeyForOutput = "NO BINDING";
                return;
            }
            if (hotkeyForOutput == null) // если кода нет в словаре, то строка пустая
            {
                hotkeyForOutput = "";
                return;
            }

            
            
            //hotkeyForOutput = hotkeyForOutput.ToUpper();
            // читать по ссылке на английском
            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=net-5.0
            if (hotkeyForOutput.Length == 1 && hotkeyForOutput[0] != '+' && hotkeyForOutput[0] != '%' &&
                hotkeyForOutput[0] != '~' && hotkeyForOutput[0] != '{' && hotkeyForOutput[0] != '}' && hotkeyForOutput[0] != '('
                && hotkeyForOutput[0] != ')' && hotkeyForOutput[0] != '^')
            {

                //SendKeys.SendWait(hotkeyForOutput);
                Console.WriteLine(hotkeyForOutput);
            }
            else if (hotkeyForOutput.Length > 1 && (hotkeyForOutput[0] == '%' || hotkeyForOutput[0] == '+' || hotkeyForOutput[0] == '^'))
            {
                StringBuilder temp = new StringBuilder();
                for (int i = 0; i < hotkeyForOutput.Length; ++i)
                {
                    if (hotkeyForOutput[i] == '%') 
                        temp.Append("Alt+");
                    else if (hotkeyForOutput[i] == '+')
                        temp.Append("Shift+");
                    else if (hotkeyForOutput[i] == '^')
                        temp.Append("Control+");
                    //temp.Append('+');
                }
                //char ch = hotkeyForOutput[0];
                //for (int i = 0; i < hotkey.Length; ++i)
                //{
                //    if (hotkey[0] == '%' || hotkey[0] == '+' || hotkey[0] == '^')
                //        hotkey = hotkey.Remove(0, 1);
                //}
                char[] beg = { '%', '+', '^' };
                hotkeyForOutput = hotkeyForOutput.TrimStart(beg);
                if (hotkeyForOutput == " ")
                    hotkeyForOutput = "Space";
                temp.Append(hotkeyForOutput);
                hotkeyForOutput = temp.ToString();

                Console.WriteLine(hotkeyForOutput);
                //SendKeys.SendWait(temp.ToString() + "{" + hotkey + "}");
            }
            else
            {
                Console.WriteLine(hotkeyForOutput);
                //hotkeyForOutput = temp.ToString();
                //SendKeys.SendWait("{" + hotkey + "}");
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

            //if (hotkey == " ")
            //    hotkey = "Space";
            //if (hotkey == "") // если код есть в словаре, но не привязан к горячей клавише, то
            //                  // в строке пишется "NO BINDING".
            //    hotkey = "NO BINDING";
            
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


  ///////////////////////////////////////////          
            
            //if (code == "D")
                //fff();
            //char ccc = 's';
            //List<string> data = new List<string>();
            //List<string> error = new List<string>();
            //List<string> done = new List<string>();

            //f(ref data);
            
            //int ii = 0;
           
                
            //for (; ii < data.Count; ++ii)
            //{
            //    try
            //    {

            //    SendKeys.SendWait("{" + data[ii] + "}");
            //    }
            //    catch
            //    {
            //        error.Add(data[ii]);
            //        continue;

            //    }
            //    done.Add(data[ii]);
            //}

            //File.WriteAllLines("Error.txt", error);
            //File.WriteAllLines("Done.txt", done);
               
 ///////////////////////////////////////                  
            
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
                //if (hotkey.Length == 1 && !Char.IsLetter(hotkey[0]) && hotkey[0] != '+' && hotkey[0] != '%' &&
                //    hotkey[0] != '~' && hotkey[0] != '{' && hotkey[0] != '}' && hotkey[0] != '('
                //    && hotkey[0] != ')' && hotkey[0] != '^')
                //{

                //    //if (Char.IsLetter(hotkey[0]))
                //    //    SendKeys.SendWait("{" + hotkey + "}");
                //    //else 
                //        SendKeys.SendWait(hotkey);
                //    //Console.WriteLine("SendKeys.SendWait(key);");
                //}
                //else if (hotkey.Length > 1 && (hotkey[0] == '%' || hotkey[0] == '+' || hotkey[0] == '^'))
                //{
                //    StringBuilder temp = new StringBuilder();
                //    for (int i = 0; i < hotkey.Length; ++i)
                //    {
                //        if (hotkey[i] == '%' || hotkey[i] == '+' || hotkey[i] == '^')
                //            temp.Append(hotkey[i]);
                //    }
                //    //char ch = hotkey[0];
                //    //for (int i = 0; i < hotkey.Length; ++i)
                //    //{
                //    //    if (hotkey[0] == '%' || hotkey[0] == '+' || hotkey[0] == '^')
                //    //        hotkey = hotkey.Remove(0, 1);
                //    //}
                //    char[] beg = { '%', '+', '^' };
                //    hotkey = hotkey.TrimStart(beg);
                    
                //    SendKeys.SendWait(temp.ToString() + "{" + hotkey + "}");
                
                //}
                //else
                //{
                //    SendKeys.SendWait("{" + hotkey + "}");
                    

                //}

                ////if (hotkey == " ")
                ////hotkey = "SPACE";
                ////if (settings != null && !settings.IsDisposed)
                ////settings.Invoke(new Action(() => { settings.hotkey.Text = hotkey; }));

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
            //Thread.CurrentThread.IsBackground = true;
            //SerialPort serialPort = (SerialPort)sender;
            //if (device.serialPort == serialPort)
                //Console.WriteLine("==");
            //string message = serialPort.ReadExisting();
            if (flagSettings)
                settingMode();
            else
                workingMode();
            //Thread.CurrentThread.IsBackground = false;
            //Console.WriteLine(hotkey);
            //Console.WriteLine(code);

            //// громкость (+ и - на пульте)
            ////if (message == "82B" || message == "2B")
            ////    SendKeys.SendWait("{" + Data.player[message] + "}");
            //////if (message == "82B\r" || message == "2B\r")
            //////    SendKeys.SendWait("{Up}"); // отправка нажатия
            ////if (message == "82C\r" || message == "2C\r")
            //////    SendKeys.SendWait("{Down}");


            ////// во весь экран (esc на пульте)
            ////if (message == "829" || message == "29")
            ////    SendKeys.SendWait("{ENTER}"); // отправка нажатия

            ////// яркость (1 и 5 на пульте)
            ////if (message == "801\r" || message == "1\r")
            ////    SendKeys.SendWait("{e}"); // отправка нажатия
            ////if (message == "805\r" || message == "5\r")
            ////    SendKeys.SendWait("{w}");

            ////// контрастность (2 и 6 на пульте)
            ////if (message == "802\r" || message == "2\r")
            ////    SendKeys.SendWait("{t}"); // отправка нажатия
            ////if (message == "806\r" || message == "6\r")
            ////    SendKeys.SendWait("{r}");

            ////// насыщенность (3 и 7 на пульте)
            ////if (message == "803\r" || message == "3\r")
            ////    SendKeys.SendWait("{u}"); // отправка нажатия
            ////if (message == "807\r" || message == "7\r")
            ////    SendKeys.SendWait("{y}");

            ////// пауза (OK на пульте)
            ////if (message == "80D\r" || message == "D\r")
            ////    SendKeys.SendWait(" "); // отправка нажатия

            ////// вперёд (стрелка вправо на пульте)
            ////if (message == "810\r" || message == "10\r")
            ////    SendKeys.SendWait("{RIGHT}"); // отправка нажатия
            ////// назад (стрелка влево на пульте)
            ////if (message == "811\r" || message == "11\r")
            ////    SendKeys.SendWait("{LEFT}");

            ////// добавить закладку (рядом с выключить на пульте)
            ////if (message == "80F\r" || message == "F\r")
            ////    SendKeys.SendWait("{p}"); // отправка нажатия

            ////// выход (значок включения/выключения на пульте)
            ////if (message == "80C\r" || message == "C\r")
            ////{
            ////    SendKeys.SendWait("%{F4}");
            ////    //serialPort.Close();
            ////}



        }
}
}
