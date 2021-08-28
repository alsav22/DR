using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;

namespace DR
{
    public class Device
    {
        public SerialPort serialPort;
        public String portName = "";
        public String ID_VID = ""; // запись вида 1A86
        public String ID_PID = ""; // запись вида 7523
        public String VID = ""; // запись вида: VID_1A86;
        public String PID = ""; // запись вида: PID_7523;
        public const String fileName = "device.txt";


        public Device()
        {
            readData(fileName, ref ID_VID, ref ID_PID);
            if (ID_VID != "" && ID_PID != "")
            {
                VID = "VID_" + ID_VID;
                PID = "PID_" + ID_PID;
            }
        }

        // запись в файл данных устройства
        public void writeData(String fileName, String vid, String pid)
        {
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(vid);
            sw.WriteLine(pid);
            sw.Close();
        }

        // чтение из файла данных устройства
        public void readData(String fileName, ref String id_vid, ref String id_pid)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(fileName);
            }
            catch
            {
                MessageBox.Show("The file with the Device ID was not found. Connect the device to the USB port and enter the ID of the device connected to the COM port.");
                return;
            }

            id_vid = sr.ReadLine();
            id_pid = sr.ReadLine();
            sr.Close();
        }
    }
}

//        // Helper function to handle regex search
//        string regex(string pattern, string text) // вспомогательная функция
//        {
//            Regex re = new Regex(pattern);
//            Match m = re.Match(text);
//            if (m.Success)
//            {
//                return m.Value;
//            }
//            else
//            {
//                return "";
//            }
//        }

//        public bool deviceSearch(String id_vid, String id_pid) // поиск подключенного устройства 
//        {
//            // Use WMI to get info
//            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
//                "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\"");
//            string vid = "VID_" + id_vid;
//            string pid = "PID_" + id_pid;
//            // Search all serial ports
//            foreach (ManagementObject queryObj in searcher.Get())
//            {
//                //// Parse the data
//                //if (null != queryObj["Name"])
//                //{
//                //    Console.WriteLine("Port = " + regex(@"(\(COM\d+\))", queryObj["Name"].ToString()));
//                //    //namePort = regex(@"(\(COM\d+\))", queryObj["Name"].ToString());
//                //    //namePort = regex(@"(COM\d+)", queryObj["Name"].ToString());
//                //}

//                //PNPDeviceID = USB\VID_1A86&PID_7523\5&1A63D808&0&2
//                if (null != queryObj["PNPDeviceID"])
//                {
//                    //Console.WriteLine("VID = " + regex("VID_([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString()));
//                    //Console.WriteLine("PID = " + regex("PID_([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString()));
//                    VID = regex("VID_([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString());
//                    PID = regex("PID_([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString());
//                    if (VID == vid && PID == pid)
//                    {
//                        if (null != queryObj["Name"])
//                        {
//                            //Console.WriteLine("Port = " + regex(@"(\(COM\d+\))", queryObj["Name"].ToString()));
//                            //namePort = regex(@"(\(COM\d+\))", queryObj["Name"].ToString());
//                            portName = regex(@"(COM\d+)", queryObj["Name"].ToString());
//                            ID_VID = id_vid;
//                            ID_PID = id_pid;
//                        }
//                        break;
//                    }
//                } // if
//            } // foreach

//            if (VID != vid || PID != pid || VID == "" || PID == "")
//            {
//                portName = "";
//                ID_VID = "";
//                ID_PID = "";
//                VID = "";
//                PID = "";
//                return false;
//            }
//            return true;

//        }

//        private void preparingСode(ref string code) // подготовка кода
//        {
//            if (code.Length > 4)
//            {
//                code = "";
//                return;
//            }
//            char[] end = { '\r' };
//            char[] beg = { '%', '+', '^' };
//            char[] begCode = { '8', '0' };
//            code = code.Trim(end);

//            if (code.Length == 3 && code[0] == '8')
//            {
//                code = code.Remove(0, 1);
//                if (code[0] == '0')
//                    code = code.Remove(0, 1);
//            }
//        }

//        public void connection() // подключение устройства к COM порту
//        {
//            serialPort = new SerialPort(portName);

//            serialPort.BaudRate = 9600;
//            serialPort.Parity = Parity.None;
//            serialPort.StopBits = StopBits.One;
//            serialPort.DataBits = 8;
//            serialPort.Handshake = Handshake.None;
//            serialPort.RtsEnable = true;
//            //serialPort.ReadTimeout = 500;

//            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
//            serialPort.Open();

//            //Console.WriteLine("Press any key to continue...");
//            //Console.WriteLine();
//            //Console.ReadKey();
//            //serialPort.Close();
//        }

//        private void workingMode() // рабочий режим
//        {
//            string code = serialPort.ReadLine();
//            //Console.WriteLine(code);
//            serialPort.DiscardInBuffer();

//            preparingСode(ref code);

//            // (Ставятся перед фигурными скобками)
//            // SHIFT +
//            // CTRL  ^
//            // ALT   %

//            // SPACE  " " (в файле пишется SPACE) 
//            // При добавлении в словарь, SPACE меняется на строку с пробелом.

//            // Буквы передавать в нижнем регистре, иначе 
//            // будут восприниматься как с Shift

//            // Одиночые символы, кроме специальных (читать по ссылке - какие это),
//            // можно передавать без фигурных скобок.

//            Console.WriteLine(code);

//            //if (settings != null && !settings.IsDisposed)
//            //settings.Invoke(new Action(() => { settings.code.Text = code; }));

//            string hotkey = "";
//            try
//            {
//                hotkey = RemoteControl.preset.dictionary[code];

//                // читать по ссылке на английском
//                //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=net-5.0
//                if (hotkey.Length == 1 && hotkey[0] != '+' && hotkey[0] != '%' &&
//                    hotkey[0] != '~' && hotkey[0] != '{' && hotkey[0] != '}' && hotkey[0] != '('
//                    && hotkey[0] != ')' && hotkey[0] != '^')
//                {

//                    SendKeys.SendWait(hotkey);
//                    //Console.WriteLine("SendKeys.SendWait(key);");
//                }
//                else if (hotkey.Length > 1 && (hotkey[0] == '%' || hotkey[0] == '+' || hotkey[0] == '^'))
//                {

//                    char ch = hotkey[0];
//                    hotkey = hotkey.Remove(0, 1);
//                    SendKeys.SendWait(ch + "{" + hotkey + "}");
//                }
//                else
//                {
//                    SendKeys.SendWait("{" + hotkey + "}");

//                }

//                //if (hotkey == " ")
//                //hotkey = "SPACE";
//                //if (settings != null && !settings.IsDisposed)
//                //settings.Invoke(new Action(() => { settings.hotkey.Text = hotkey; }));

//            }
//            catch
//            {

//            }
//            //SendKeys.SendWait("e");
//            Thread.Sleep(200);
//            if (serialPort.IsOpen)
//                serialPort.DiscardInBuffer();
//        }

//        private void settingMode() // режим настроек
//        {
//            string code = serialPort.ReadLine();
//            Console.WriteLine(code);
//            serialPort.DiscardInBuffer();

//            preparingСode(ref code);

//            // (Ставятся перед фигурными скобками)
//            // SHIFT +
//            // CTRL  ^
//            // ALT   %

//            // SPACE  " " (в файле пишется SPACE) 
//            // При добавлении в словарь, SPACE меняется на строку с пробелом.

//            // Буквы передавать в нижнем регистре, иначе 
//            // будут восприниматься как с Shift

//            // Одиночые символы, кроме специальных (читать по ссылке - какие это),
//            // можно передавать без фигурных скобок.




//            Console.WriteLine(code);
//            //SendKeys.SendWait(code);
//            if (RemoteControl.settings != null && !RemoteControl.settings.IsDisposed)
//            {
//                RemoteControl.settings.Invoke(new Action(() => { RemoteControl.settings.code.Text = code; }));
//            }


//            //try
//            //{
//            //    hotkey = preset.dictionary[code];

//            //    // читать по ссылке на английском
//            //    //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=net-5.0
//            //    if (hotkey.Length == 1 && hotkey[0] != '+' && hotkey[0] != '%' &&
//            //        hotkey[0] != '~' && hotkey[0] != '{' && hotkey[0] != '}' && hotkey[0] != '('
//            //        && hotkey[0] != ')' && hotkey[0] != '^')
//            //    {

//            //        SendKeys.SendWait(hotkey);
//            //        //Console.WriteLine("SendKeys.SendWait(hotkey);");
//            //    }
//            //    else if (hotkey.Length > 1 && (hotkey[0] == '%' || hotkey[0] == '+' || hotkey[0] == '^'))
//            //    {

//            //        char ch = hotkey[0];
//            //        hotkey = hotkey.TrimStart(beg);
//            //        SendKeys.SendWait(ch + "{" + hotkey + "}");
//            //    }
//            //    else
//            //    {
//            //        SendKeys.SendWait("{" + hotkey + "}");

//            //    }
//            string hotkey = "";
//            try
//            {
//                hotkey = RemoteControl.preset.dictionary[code]; // получение, из словаря, грячей клавиши по коду
//            }
//            catch
//            {
//                hotkey = null; // кода нет в словаре
//            }

//            if (hotkey == " ")
//                hotkey = "SPACE";
//            if (hotkey == "") // если код есть в словаре, но не привязан к горячей клавише, то
//                // в строке пишется "NO BINDING".
//                hotkey = "NO BINDING";
//            if (hotkey == null) // если кода нет в словаре, то строка пустая
//                hotkey = "";
//            if (RemoteControl.settings != null && !RemoteControl.settings.IsDisposed)
//            {
//                RemoteControl.settings.Invoke(new Action(() => { RemoteControl.settings.hotkey.Text = hotkey; RemoteControl.settings.hotkey.Focus(); }));
//            }

//            //}
//            //catch
//            //{

//            //}
//            //SendKeys.SendWait("e");
//            Thread.Sleep(200);
//            if (serialPort.IsOpen)
//                serialPort.DiscardInBuffer();
//        }

//        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
//        {
//            //serialPort = (SerialPort)sender;
//            //string message = serialPort.ReadExisting();
//            if (RemoteControl.flagSettings)
//                RemoteControl.device.settingMode();
//            else
//                RemoteControl.device.workingMode();
//            //Console.WriteLine(hotkey);
//            //Console.WriteLine(code);

//            //// громкость (+ и - на пульте)
//            ////if (message == "82B" || message == "2B")
//            ////    SendKeys.SendWait("{" + Data.player[message] + "}");
//            //////if (message == "82B\r" || message == "2B\r")
//            //////    SendKeys.SendWait("{Up}"); // отправка нажатия
//            ////if (message == "82C\r" || message == "2C\r")
//            //////    SendKeys.SendWait("{Down}");


//            ////// во весь экран (esc на пульте)
//            ////if (message == "829" || message == "29")
//            ////    SendKeys.SendWait("{ENTER}"); // отправка нажатия

//            ////// яркость (1 и 5 на пульте)
//            ////if (message == "801\r" || message == "1\r")
//            ////    SendKeys.SendWait("{e}"); // отправка нажатия
//            ////if (message == "805\r" || message == "5\r")
//            ////    SendKeys.SendWait("{w}");

//            ////// контрастность (2 и 6 на пульте)
//            ////if (message == "802\r" || message == "2\r")
//            ////    SendKeys.SendWait("{t}"); // отправка нажатия
//            ////if (message == "806\r" || message == "6\r")
//            ////    SendKeys.SendWait("{r}");

//            ////// насыщенность (3 и 7 на пульте)
//            ////if (message == "803\r" || message == "3\r")
//            ////    SendKeys.SendWait("{u}"); // отправка нажатия
//            ////if (message == "807\r" || message == "7\r")
//            ////    SendKeys.SendWait("{y}");

//            ////// пауза (OK на пульте)
//            ////if (message == "80D\r" || message == "D\r")
//            ////    SendKeys.SendWait(" "); // отправка нажатия

//            ////// вперёд (стрелка вправо на пульте)
//            ////if (message == "810\r" || message == "10\r")
//            ////    SendKeys.SendWait("{RIGHT}"); // отправка нажатия
//            ////// назад (стрелка влево на пульте)
//            ////if (message == "811\r" || message == "11\r")
//            ////    SendKeys.SendWait("{LEFT}");

//            ////// добавить закладку (рядом с выключить на пульте)
//            ////if (message == "80F\r" || message == "F\r")
//            ////    SendKeys.SendWait("{p}"); // отправка нажатия

//            ////// выход (значок включения/выключения на пульте)
//            ////if (message == "80C\r" || message == "C\r")
//            ////{
//            ////    SendKeys.SendWait("%{F4}");
//            ////    //serialPort.Close();
//            ////}



//        }
//    }
//}
