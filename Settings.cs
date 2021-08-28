using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DR
{
    
    
    public partial class Settings : Form
    {
        const string fileNameHotKeys = "listKeyboardKeys.dat";
        
        public Dictionary <string, Hotkey> dictionaryKeyboardKeys = new Dictionary <string, Hotkey>();
        
        public Settings()
        {
            InitializeComponent();
            code.ReadOnly = true;
            hotkey.ReadOnly = true;
            removebinding.Enabled = false;
        }

        public bool readHotKeys()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream(fileNameHotKeys, FileMode.Open))
                {
                    dictionaryKeyboardKeys = (Dictionary<string, Hotkey>)formatter.Deserialize(fs);
                    return true;
                }
            }
            catch
            {
                MessageBox.Show("File " + fileNameHotKeys + " not found!");
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

        void writeHotKeys()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(fileNameHotKeys, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, dictionaryKeyboardKeys);
            }
        }

        private void removecode_Click(object sender, EventArgs e)
        {
            RemoteControl.preset.dictionaryBindings.Remove(code.Text);
            code.Clear();
            hotkey.Clear();
            code.Focus();
        }

        bool preparingOfSending(ref string message)
        {
            StringBuilder temp = new StringBuilder();

            char[] sep = { '+' };
            string[] parts = message.Split(sep);
            
            int i = 0;
            try
            {
                for ( ; i < parts.Length; ++i)
                {

                    temp.Append(dictionaryKeyboardKeys[parts[i]].hotkeyForSending);
                }
            }
            catch
            {
                message = null;
                MessageBox.Show("Key " + parts[i] + " not found in the list!");
                return false;
            }
            
            message = temp.ToString();
            return true;
            
        }
        
        private void addbinding_Click(object sender, EventArgs e)
        {
            if (code.Text == "" || code.Text.Length > 2)
            {
                code.Clear();
                hotkey.Clear();
                code.Focus();
                //return;
            }
            else if (hotkey.Text == "")
            {
                MessageBox.Show("Press the keys on your keyboard!");
                hotkey.Focus();
                //return;
            }
            else
            {
                string message = hotkey.Text;

                if (preparingOfSending(ref message))
                {

                    Hotkey hkey = new Hotkey(hotkey.Text, message);

                    if (RemoteControl.preset.processName == "")
                        RemoteControl.preset.processName = processName.Text;

                    if (RemoteControl.preset.dictionaryBindings.ContainsKey(code.Text)) // если код уже есть в словаре
                    {
                        // если такая привязка уже есть
                        if (RemoteControl.preset.dictionaryBindings[code.Text].hotkeyTextBox == hkey.hotkeyTextBox
                              && RemoteControl.preset.dictionaryBindings[code.Text].hotkeyForSending == hkey.hotkeyForSending) 
                        {
                            MessageBox.Show("The binding already exists!");
                            addbinding.Enabled = false;
                            hotkey.Focus();
                            //return;
                        }
                        else
                            RemoteControl.preset.dictionaryBindings[code.Text] = hkey; // то меняем ему привязку
                    }
                    else // если кода нет в словаре, то добавляем новый код с привязкой
                    {

                        RemoteControl.preset.dictionaryBindings.Add(code.Text, hkey);
                        

                    }
                    //hotkey.Clear();
                    hotkey.Focus();
                    addbinding.Enabled = false;
                    //code.Focus();
                    
                }
                else
                {
                    //hotkey.Clear();
                    hotkey.Focus();
                    addbinding.Enabled = false;
                    //code.Focus();
                }
            }
        }

        private void removebinding_Click(object sender, EventArgs e)
        {
            
            
            //try
            //{
            RemoteControl.preset.RemoveBinding(code.Text);
            //if (RemoteControl.preset.dictionaryBindings.Remove(code.Text))
            //{
                //code.Clear();
                //hotkey.Clear();
                //code.Focus();
            //}
            //else
                //MessageBox.Show("Code not found!");
            //code.Clear();
            hotkey.Clear();
            hotkey.Focus();
            //removebinding.Enabled = false;
        }

        private void addpreset_Click(object sender, EventArgs e)
        {
            if (RemoteControl.listPresets.Exists(pN => pN.name == presetName.Text))
                MessageBox.Show("Пресет с таким именем уже существует!");
            else
            {
                presetName.Items.Add(presetName.Text);
                Preset preset = new Preset();
                preset.name = presetName.Text;
                RemoteControl.preset = preset;
                RemoteControl.listPresets.Add(preset);
                //RemoteControl.savePresets();
                //RemoteControl.settings.Close();
            }
        }

        private void presetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("presetName_SelectedIndexChanged");
            RemoteControl.preset = RemoteControl.listPresets.Find(pN => pN.name == presetName.SelectedItem.ToString());
            RemoteControl.form.PresetNameText = RemoteControl.preset.name;
            code.Clear();
            hotkey.Clear();
            code.Focus();

        }

        private void removepreset_Click(object sender, EventArgs e)
        {
            if (RemoteControl.listPresets.Count != 0)
            {
                RemoteControl.listPresets.Remove(RemoteControl.preset);
                presetName.Items.Remove(presetName.Text);
                if (RemoteControl.listPresets.Count != 0)
                {
                    presetName.SelectedIndex = 0;
                    RemoteControl.preset = RemoteControl.listPresets[0];
                    //RemoteControl.presetName = RemoteControl.preset.name;
                    RemoteControl.form.PresetNameText = RemoteControl.preset.name;
                    
                }
                else
                {
                    presetName.Items.Clear();
                    presetName.Text = "";
                    RemoteControl.preset = null;
                    //RemoteControl.presetName = "";
                    //RemoteControl.processName = "";
                    RemoteControl.form.PresetNameText = "";
                }
                
            }
            else
                MessageBox.Show("Список пресетов пуст!");
               
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (RemoteControl.listPresets.Count > 1 && (RemoteControl.listPresets[0] != RemoteControl.preset))
            {
                RemoteControl.listPresets.RemoveAt(RemoteControl.listPresets.FindIndex(pN => pN == RemoteControl.preset));
                RemoteControl.listPresets.Insert(0, RemoteControl.preset);

            }
            
            RemoteControl.savePresets();
            MessageBox.Show("Изменения успешно сохранены!");

//////////////////////// разово
            //BinaryFormatter formatter = new BinaryFormatter();

            //using (FileStream fs = new FileStream("listHotKeys.dat", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, dictionaryHotKeys);
            //}
/////////////////////////
        }

        private void Settings_Activated(object sender, EventArgs e)
        {
            RemoteControl.flagSettings = true;
            Console.WriteLine("Settings_Activated");
        }

        private void Settings_Deactivate(object sender, EventArgs e)
        {
            RemoteControl.flagSettings = false;
            Console.WriteLine("Settings_Deactivated");
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (presetName.Items.Count > 0)
                RemoteControl.presetNameBeforeClosing = presetName.Text;
            else
                RemoteControl.presetNameBeforeClosing = "";
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoteControl.settings = null;
            RemoteControl.flagSettings = false;
        }

        private void hotkey_KeyDown(object sender, KeyEventArgs e)
        {
            hotkey.Clear();
            
            string key;
            
            if (e.KeyCode.ToString() == "Return")
                key = "Enter";
            else
                key = e.KeyCode.ToString();
            if (e.Alt)
            {
                e.Handled = true;
            }

            if (e.Control && e.Alt && e.Shift)
            {
               hotkey.Text = "Control" + "+" + "Shift" + "+" + "Alt" + "+" + key;
            }
            else if (e.Control && e.Shift)
                hotkey.Text = "Control" + "+" + "Shift" + "+" + key;
            else if (e.Control && e.Alt)
                hotkey.Text = "Control" + "+" + "Alt" + "+" + key;
            else if (e.Shift && e.Alt)
                hotkey.Text = "Shift" + "+" + "Alt" + "+" + key;
            else if (e.Control || e.Alt || e.Shift)
            {
                if (key != Keys.ControlKey.ToString() && key != Keys.Menu.ToString() && key != Keys.ShiftKey.ToString())
                    hotkey.Text = e.Modifiers.ToString() + "+" + key;
                else
                {
                    hotkey.Text = key;
                }
            }
            else
            {
                hotkey.Text = key;
            }

        }

        private void hotkey_TextChanged(object sender, EventArgs e)
        {
            if (hotkey.Text.Length > 0 && code.Text.Length > 0)
            {
                removebinding.Enabled = true;
                addbinding.Enabled = true;
            }
            else
            {
                removebinding.Enabled = false;
                addbinding.Enabled = false;
            }
        }

        private void code_TextChanged(object sender, EventArgs e)
        {
            if (hotkey.Text.Length > 0 && code.Text.Length > 0)
            {
                removebinding.Enabled = true;
                addbinding.Enabled = true;
            }
            else
            {
                removebinding.Enabled = false;
                addbinding.Enabled = false;
            }
        }

        private void hotkey_Enter(object sender, EventArgs e)
        {
            if (hotkey.Text.Length > 0 && code.Text.Length > 0)
            {
                removebinding.Enabled = true;
                addbinding.Enabled = true;
            }
            else
            {
                removebinding.Enabled = false;
                addbinding.Enabled = false;
            }
        }

        private void code_Enter(object sender, EventArgs e)
        {
            if (hotkey.Text.Length > 0 && code.Text.Length > 0)
            {
                removebinding.Enabled = true;
                addbinding.Enabled = true;
            }
            else
            {
                removebinding.Enabled = false;
                addbinding.Enabled = false;
            }
        }

        private void presetName_TextChanged(object sender, EventArgs e)
        {
            if (presetName.Text.Length > 0)
            {
                removepreset.Enabled = true;
            }
            else
                removepreset.Enabled = false;
        }

        private void presetName_Enter(object sender, EventArgs e)
        {
            if (presetName.Text.Length > 0)
            {
                removepreset.Enabled = true;
            }
            else
                removepreset.Enabled = false;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if (hotkey.Text.Length > 0 && code.Text.Length > 0)
            {
                removebinding.Enabled = true;
                addbinding.Enabled = true;
            }
            else
            {
                removebinding.Enabled = false;
                addbinding.Enabled = false;
            }
        }
    }

    [Serializable]
    public class Hotkey
    {


        public Hotkey(string textBox, string message)
        {
            hotkeyTextBox = textBox;
            hotkeyForSending = message;
        }

        public string hotkeyTextBox { get; set; }
        public string hotkeyForSending { get; set; }

        //void fromTextBoxToSending()
        //{
        //    StringBuilder temp = new StringBuilder();

        //    char[] sep = { '+' };
        //    string[] parts = hotkeyTextBox.Split(sep);

        //    for (int i = 0; i < parts.Length; ++i)
        //    {

        //        if (parts[i] == "Control")
        //        {
        //            temp.Append('^');
        //        }
        //        else if (parts[i] == "Shift")
        //        {
        //            temp.Append('+');
        //        }
        //        else if (parts[i] == "Alt")
        //        {
        //            temp.Append('%');
        //        }
        //        else if (parts[i] == "Space")
        //        {
        //            temp.Append(' ');
        //        }
        //        else if (parts[i] == "OemPeriod")
        //        {
        //            temp.Append('.');
        //        }
        //        else if (parts[i] == "Oemcomma")
        //        {
        //            temp.Append(',');
        //        }
        //        else
        //            temp.Append(parts[i]);
        //    }


        //    hotkeyForSending = temp.ToString();

        //}


    }
}
