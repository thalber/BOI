using Blep;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;

namespace Blep
{
    internal class BoiConfigManager
    {
        public static JObject confjo;
        public static void ReadConfig()
        {
            try
            {
                confjo = null;
                string tfcont = File.ReadAllText(BlepOut.cfgpath);
                confjo = JObject.Parse(tfcont);
            }
            catch (JsonException joe)
            {
                Wood.WriteLine("ERROR PARSING BOI CONFIG FILE:");
                Wood.Indent();
                Wood.WriteLine(joe);
                Wood.Unindent();
            }
            catch (IOException ioe)
            {
                Wood.WriteLine("ERROR OPENING BOI CONFIG FILE:");
                Wood.Indent();
                Wood.WriteLine(ioe);
                Wood.Unindent();
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
                Wood.WriteLine("Can not save config: nothing to write");
            }
            catch (IOException ioe)
            {
                Wood.WriteLine("ERROR SERIALIZING BOI CONFIG FILE:");
                Wood.Indent();
                Wood.WriteLine(ioe);
                Wood.Unindent();
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
