using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using System.Management;
using System.Runtime.InteropServices;

namespace DR
{
    delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
    
    
    partial class Form1 : Form
    {
        static TextBox _tb;
        
        public string PresetNameText { get { return presetName.Text; } set { presetName.Text = value; } }

        public Form1(string portName, string ID_VID, string ID_PID)
        {
            InitializeComponent();

            /////////////////////////////

            WinEventDelegate dele = new WinEventDelegate(WinEventProc);
            IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, dele, 0, 0, WINEVENT_OUTOFCONTEXT);
            _tb = this.processName;

            /////////////////////////////
            
            this.portName.Text = portName;
            id_vid.Text = ID_VID;
            id_pid.Text = ID_PID;
            //processName.Text = RemoteControl.processNameDefault;
            //presetName.Text  = RemoteControl.presetNameDefault;
            PresetNameText = "";
            if (this.portName.Text != "")
            {
                ok.Hide();
                id_vid.ReadOnly = true;
                id_pid.ReadOnly = true;
                processName.ReadOnly = true;
                presetName.ReadOnly = true;
                buttonSettings.Visible = true;
            }
            else
            {
                buttonSettings.Hide();
                processName.ReadOnly = true;
                presetName.ReadOnly  = true;
            }
            notifyIcon1.Visible = false;
            
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            // делаем нашу иконку скрытой
            notifyIcon1.Visible = false;
            // возвращаем отображение окна в панели
            ShowInTaskbar = true;

            //разворачиваем окно
            WindowState = FormWindowState.Normal;
            ActiveControl = null;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

            // проверяем наше окно, и если оно было свернуто, делаем событие        
            if (WindowState == FormWindowState.Minimized)
            {
                // прячем наше окно из панели
                ShowInTaskbar = false;
                // делаем нашу иконку в трее активной
                notifyIcon1.Visible = true;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (RemoteControl.deviceSearch(ref RemoteControl.device))
            {
                RemoteControl.connection(ref RemoteControl.device);
                portName.Text = RemoteControl.device.portName;
                MessageBox.Show("The device is connected and ready for use.");

                ok.Hide();
                id_vid.ReadOnly = true;
                id_pid.ReadOnly = true;
                processName.ReadOnly = true;
                ActiveControl = null;

                WindowState = FormWindowState.Minimized;

                RemoteControl.device.writeData(Device.fileName, id_vid.Text, id_pid.Text);

            }
            else
            {
                MessageBox.Show("Device not found!");
                id_vid.Focus();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = new DialogResult();
            if (RemoteControl.device.serialPort != null && RemoteControl.device.serialPort.IsOpen)
            {
                RemoteControl.device.serialPort.DiscardInBuffer();
                RemoteControl.device.serialPort.Close();
            
                result = MessageBox.Show("Save changes before closing?", "Saving changes", MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            if (result == DialogResult.Yes)
            {
                if (RemoteControl.listPresets.Count > 1 && (RemoteControl.listPresets[0] != RemoteControl.preset))
                {
                    RemoteControl.listPresets.RemoveAt(RemoteControl.listPresets.FindIndex(pN => pN == RemoteControl.preset));
                    RemoteControl.listPresets.Insert(0, RemoteControl.preset);
                    
                }
                RemoteControl.savePresets();
                MessageBox.Show("Изменения успешно сохранены!", "", MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                
            }
            
            //WindowState = FormWindowState.Normal;
            //Dispose();
            //Application.Exit();
            //Environment.Exit(0);
            
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            
            if (RemoteControl.settings == null)
            {
                Settings settings = new Settings();
                if (RemoteControl.preset != null)
                {
                    settings.processName.Text = RemoteControl.preset.processName;
                    settings.presetName.Text = RemoteControl.preset.name;
                }

                for (int i = 0; i < RemoteControl.listPresets.Count; ++i)
                    settings.presetName.Items.Add(RemoteControl.listPresets[i].name);
                //if (settings.presetName.Items.Count > 0)
                    //settings.presetName.SelectedIndex = 0;
                
                RemoteControl.settings = settings;
            }
            
            RemoteControl.settings.Show();
            if (RemoteControl.settings.presetName.Items.Count > 0)
                RemoteControl.settings.presetName.SelectedIndex = RemoteControl.settings.presetName.Items.IndexOf(RemoteControl.presetNameBeforeClosing);
            RemoteControl.settings.code.Focus();
            
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (RemoteControl.device.serialPort != null && RemoteControl.device.serialPort.IsOpen)
                RemoteControl.device.serialPort.Close();
            
            Application.Exit();
            
        }
///////////////////////////////////
        
        private static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            string wTitle = GetActiveWindowTitle();
            _tb.Clear();
            _tb.AppendText((!String.IsNullOrEmpty(wTitle) ? wTitle : "Empty") + "\r\n"); // вместо этого реализуйте метод записи в файл
        }
 
        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();
 
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
 
 
 //////////////////////////////////////
 
        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
    }
    
}
