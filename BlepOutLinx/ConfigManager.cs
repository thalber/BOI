using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using BlepOutLinx;

namespace BlepOutIn
{
    class BoiConfigManager
    {
        public static JObject confjo;
        public static void ReadConfig()
        {
            if (File.Exists(Path.Combine(BlepOut.BOIpath, "cfg.txt"))) File.Delete(Path.Combine(BlepOut.BOIpath, "cfg.txt"));
            try
            {
                confjo = null;
                string tfcont = File.ReadAllText(BlepOut.cfgpath);
                confjo = JObject.Parse(tfcont);
            }
            catch (JsonException joe)
            {
                Debug.WriteLine("ERROR PARSING BOI CONFIG FILE:");
                Debug.Indent();
                Debug.WriteLine(joe);
                Debug.Unindent();
            }
            catch (IOException ioe)
            {
                Debug.WriteLine("ERROR OPENING BOI CONFIG FILE:");
                Debug.Indent();
                Debug.WriteLine(ioe);
                Debug.Unindent();
            }
        }
        public static void WriteConfig()
        {
            try
            {
                File.WriteAllText(BlepOut.cfgpath, confjo.ToString());
            }
            catch (NullReferenceException)
            {
                Debug.WriteLine("Can not save config: nothing to write");
            }
            catch (IOException ioe)
            {
                Debug.WriteLine("ERROR SERIALIZING BOI CONFIG FILE:");
                Debug.Indent();
                Debug.WriteLine(ioe);
                Debug.Unindent();
            }
        }

        public static string TarPath
        {
            get
            {
                if (confjo == null || !confjo.ContainsKey("tarpath")) return string.Empty;
                return (string)confjo["tarpath"];
            }
            set
            {
                if (confjo == null)
                {
                    confjo = new JObject();
                    confjo.Add("tarpath", value);
                }
                if (confjo.ContainsKey("tarpath")) confjo["tarpath"] = value;
                else
                {
                    confjo.Add("tarpath", value);
                }
            }
        }
    }
}
