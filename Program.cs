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

           

            RemoteControl RC = new RemoteControl();
            RC.start();
            
            Application.Run();
            
            
        }

    }
}
