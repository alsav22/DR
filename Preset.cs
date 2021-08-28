using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;



namespace DR
{
    //public class DataForHotkeys
    //{
    //    public Dictionary<string, string> dictionaryHotKeys = new Dictionary<string, string>();

    //    //public static List<string> listKeysTextBox = new List<string>() 
    //    //{"Control", "Shift", "Alt" };
    //    //public static List<string> listKeysForSaving = new List<string>() {"^", "+", "%" };

    //    //public DataForHotkeys()
    //    //{
    //    //    listKeysTextBox.AddRange(Enum.GetNames(typeof(Keys)));
    //    //    if (readData())
    //    //        MessageBox.Show("Done!");
    //    //    else
    //    //        MessageBox.Show("Error!");
    //    //    try
    //    //    {
    //    //        File.WriteAllLines("listKeysTextBox.txt", listKeysTextBox);
    //    //    }
    //    //    catch
    //    //    {
    //    //        MessageBox.Show("Error!");
    //    //    }

            
    //    //}

    //    bool readData()
    //    {
    //        string fileName = "listKeysTextBox.txt";
    //        StreamReader sr;
    //        try
    //        {
    //            sr = new StreamReader(fileName);
    //        }
    //        catch
    //        {
    //            MessageBox.Show("File not found!");
    //            return false;
    //        }

    //        string str;
    //        while ((str = sr.ReadLine()) != null)
    //        {

    //            string[] parts = str.Split(' ');
    //            if (!dictionaryHotKeys.ContainsKey(parts[0]))
    //            {
    //                if (parts.Length > 1)
    //                {
    //                    if (parts[0] == "Space")
    //                        parts[1] = " ";
                        
    //                    dictionaryHotKeys.Add(parts[0], parts[1]);
                        
    //                }
    //                else
    //                {
                        
    //                    dictionaryHotKeys.Add(parts[0], "");
                        
    //                }
    //            }
    //        }

    //        sr.Close();
    //        return true;
    //    }
    
    
    //}
    
    
    [Serializable]
    public class Preset
    {
        public Dictionary <string, Hotkey> dictionaryBindings = new Dictionary <string, Hotkey>();
        public string processName = "";
        public string name = "";
        public const int capacityList = 2;



        public bool AddBinding(string code, string hotKey, string message)
        {
            Hotkey hkey = new Hotkey(hotKey, message);

            //if (RemoteControl.preset.processName == "")
                //RemoteControl.preset.processName = processName.Text;

            if (dictionaryBindings.ContainsKey(code)) // если код уже есть в словаре
            {
                // если такая привязка уже есть
                if (dictionaryBindings[code].hotkeyTextBox == hkey.hotkeyTextBox
                      && dictionaryBindings[code].hotkeyForSending == hkey.hotkeyForSending)
                {
                    
                    return false;
                }
                else
                {
                    dictionaryBindings[code] = hkey; // то меняем ему привязку
                    return true;
                }
            }
            else // если кода нет в словаре, то добавляем новый код с привязкой
            {
                dictionaryBindings.Add(code, hkey);
                return true;
            }
        }

        public bool RemoveBinding(string code)
        {
            if (dictionaryBindings.Remove(code))
                return true;
            else
                return false;
        }

        
        
        
        
        
        

        //public bool readData(Dictionary<string, List <string> > data, String fileName)
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();

        //    using (FileStream fs = new FileStream(fileName, FileMode.Open))
        //    {
        //        //dictionary = (Dictionary<string, string>)formatter.Deserialize(fs);
        //        dictionary = (Dictionary<string, List <string> >)formatter.Deserialize(fs);
        //    }

            // при создании нового сериализованного файла данных: "potdictionarymini.dat",
            // из текстового файла: "potplayermini.txt",
            // закомментировать то, что выше (кроме первой строки), и раскомментировать то, что ниже.

            //fileName = "potplayermini.txt";
            //StreamReader sr;
            //try
            //{
            //    sr = new StreamReader(fileName);
            //}
            //catch
            //{
            //    MessageBox.Show("File not found!");
            //    return false;
            //}

            //string str;
            //while ((str = sr.ReadLine()) != null)
            //{

            //    string[] parts = str.Split(' ');
            //    if (parts.Length >= 3)
            //    {
            //        if (parts[2] == "SPACE")
            //            parts[2] = " ";
            //        dictionary.Add(parts[0], parts[2]);
            //        dictionary.Add(parts[1], parts[2]);
            //    }
            //    else
            //    {
            //        dictionary.Add(parts[0], "");
            //        dictionary.Add(parts[1], "");
            //    }
            //}

            //sr.Close();

            //fileName = "potplayermini.dat";
            //using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, dictionary);

            //    //Console.WriteLine("Объект сериализован");
            //}
            

        //    return true;
        //}
        
        //void writeData(Dictionary<string, List <string> > data, String fileName)
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();

        //    using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        //    {
        //        formatter.Serialize(fs, dictionary);

        //        //Console.WriteLine("Объект сериализован");
        //    }
            
            //StreamWriter sw = new StreamWriter(fileName);
            
            //foreach (KeyValuePair<string, string> kvp in dictionary)
            //{
            //    //Console.WriteLine(kvp.Key + " " + kvp.Value);
            //    sw.WriteLine(kvp.Key + " " + kvp.Value);
               
            //}
            //sw.Close();
            
        //}

        
    }
}
