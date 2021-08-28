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
        public Settings()
        {
            InitializeComponent();
            code.ReadOnly = true;
            hotkey.ReadOnly = true;
            removebinding.Enabled = false;
        }

        private void removecode_Click(object sender, EventArgs e)
        {
            RemoteControl.preset.RemoveBinding(code.Text);
            code.Clear();
            hotkey.Clear();
            code.Focus();
        }
        
        private void addbinding_Click(object sender, EventArgs e)
        {
            if (code.Text == "" || code.Text.Length > 2)
            {
                code.Clear();
                hotkey.Clear();
                code.Focus();
            }
            else if (hotkey.Text == "")
            {
                MessageBox.Show("Press the keys on your keyboard!");
                hotkey.Focus();
            }
            else
            {
                if (RemoteControl.preset.AddBinding(code.Text, hotkey.Text))
                {
                    MessageBox.Show("Done!");
                    
                }
                else
                {
                    MessageBox.Show("The binding already exists!");
                }
                   
                hotkey.Focus();
                addbinding.Enabled = false;
                //code.Focus();
            }
        }

        private void removebinding_Click(object sender, EventArgs e)
        {
            RemoteControl.preset.RemoveBinding(code.Text);
            
            hotkey.Clear();
            hotkey.Focus();
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

}
