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

       

        //public bool AddHotKey(string code, string hotKey)
        //{
        //    try
        //    {
        //        List<string> list = new List<string>(Preset.capacityList);
        //        for (int i = 0; i < list.Capacity; ++i)
        //            list.Insert(i, hotKey);
        //        dictionary.Add(code, list);
                
                
        //        //dictionary[code] = hotkey;
        //        dictionary[code].Add(hotKey);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Code no found!");
        //        return false;
        //    }
        //    MessageBox.Show("Key added successfully.");
        //    return true;
        //}

        //public bool RemoveHotKey(string code)
        //{
        //    try
        //    {
        //        //dictionary[code] = "";
        //        for (int i = 0; i < dictionary[code].Count; ++i)
        //            dictionary[code][i] = "";
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Code no found!");
        //        return false;
        //    }
        //    MessageBox.Show("Key successfully removed.");
        //    return true;
        //}

        //public bool AddCode(string code)
        //{
        //    try
        //    {
        //        //dictionary.Add(code, "");
        //        List<string> list = new List<string>(Preset.capacityList);
        //        for (int i = 0; i < list.Capacity; ++i)
        //            list.Insert(i, "");
        //        dictionary.Add(code, list);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("The code already exists!");
        //        return false;
        //    }
        //    MessageBox.Show("Code added successfully.");
        //    return true;
        //}

        //public bool RemoveCode(string code)
        //{
        //    if (dictionary.Remove(code))
        //    {
        //        MessageBox.Show("Code successfully removed.");
        //        return true;
        //    }
        //    MessageBox.Show("Code not found!");
        //    return false;
        //}

        
        //public void foo()
        //{

        //    string fileName = "Коды пульта (слева направо).txt";
        //    if (readData(dictionary, fileName))
        //    {
        //        fileName = "Data.txt";
        //        writeData(dictionary, fileName);
        //    }
        //}
        
        

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
