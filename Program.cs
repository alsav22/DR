using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Management;
using System.IO;

namespace DR
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        
        
        static void Main(string[] args)
        {
            //// Get a list of serial port names.
            //string[] ports = SerialPort.GetPortNames();

            //Console.WriteLine("The following serial ports were found:");

            //// Display each port name to the console.
            //foreach (string port in ports)
            //{
            //    Console.WriteLine(port);
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //RemoteControl.device = new Device();

            //if (RemoteControl.device.ID_VID != "" && RemoteControl.device.ID_PID != "")
            //{
            //    if (RemoteControl.device.deviceSearch(RemoteControl.device.ID_VID, RemoteControl.device.ID_PID))
            //    {
            //        RemoteControl.device.connection();
                    
            //    }
            //    else
            //        MessageBox.Show("Device not found!");
            //}


            //RemoteControl.form = new Form1(RemoteControl.device.portName, RemoteControl.device.ID_VID, RemoteControl.device.ID_PID);
            //RemoteControl.form.Show();
            
            ////if (RemoteControl.lilstPresets.Count == 0)
            //    //form.buttonSettings.Hide();

            //if (RemoteControl.device.portName != "" && RemoteControl.device.portName != null)
            //{
            //    MessageBox.Show("The device is connected and ready for use.");

            //    RemoteControl.form.ActiveControl = null;
            //    RemoteControl.form.WindowState = FormWindowState.Minimized;
            //}

            //if (RemoteControl.device.portName != "")
            //{
            //    RemoteControl RC = new RemoteControl();
            //    RC.start();
            //}

            
            //List<string> list = new List<string>();
            //list.AddRange(Enum.GetNames(typeof(Keys)));
            //string[] arrstr = Enum.GetNames(typeof(Keys));
            //DataForHotkeys obj = new DataForHotkeys();
            //string[] arrstr;
            //arrstr = File.ReadAllLines("Данные для HotKey.txt");
            //list.AddRange(arrstr);
            //char [] trim = {'{', '}'};
            //for (int i = 0; i < list.Count; ++i)
            //{
            //    string[] parts = list[i].Split(' ');
            //    if (parts.Length > 1)
            //    {
            //        if (parts[0] == "Space")
            //            parts[1] = " ";
            //        parts[1] = parts[1].Trim(trim);
            //        parts[1] = "{" + parts[1] + "}";
            //        list[i] = parts[0] + " " + parts[1];
                    

            //    }
            //}
            //while ((str = sr.ReadLine()) != null)
            //{

            //    string[] parts = str.Split(' ');
            //    if (!dictionaryHotKeys.ContainsKey(parts[0]))
            //    {
            //        if (parts.Length > 1)
            //        {
            //            if (parts[0] == "Space")
            //                parts[1] = " ";

            //            dictionaryHotKeys.Add(parts[0], parts[1]);

            //        }
            //        else
            //        {

            //            dictionaryHotKeys.Add(parts[0], "");

            //        }
            //    }
            //}
            
            //File.WriteAllLines("list.txt", list);
            //Preset p = new Preset();
            //p.readHotKeys();

            RemoteControl RC = new RemoteControl();
            RC.start();
            
            Application.Run();
            
            //Environment.Exit(0);
        }

    }
}
