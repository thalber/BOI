using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace Blep
{
    public static class TagManager
    {
        public static bool ReadTagsFromFile(string filepath)
        {
            try
            {
                string json = File.ReadAllText(filepath);
                ReadTagData(json);
                return true;
            }
            catch (IOException ioe)
            {
                Wood.WriteLine($"ERROR READING TAGS FILE FROM {filepath}:");
                Wood.Indent();
                Wood.WriteLine(ioe);
                Wood.Unindent();
                return false;
            }
        }
        private static void ReadTagData(string json)
        {
            try
            {
                if (!string.IsNullOrEmpty(json)) TagData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            catch (JsonException je)
            {
                Wood.WriteLine("ERROR PARSING TAG DATA FILE:");
                Wood.Indent();
                Wood.WriteLine(je);
                Wood.Unindent();
            }
            
        }
        private static Dictionary<string, string> TagData { get { if (_td == null) _td = new Dictionary<string, string>(); return _td; } set => _td = value; }
        private static Dictionary<string, string> _td;

        private static string GetTDToSave()
        {
            return JsonConvert.SerializeObject(TagData, Formatting.Indented);
        }
        public static bool SaveToFile(string filepath)
        {
            try
            {
                File.WriteAllText(filepath, GetTDToSave());
                return true;
            }
            catch (IOException ioe)
            {
                Wood.WriteLine($"ERROR WRITING TAGS FILE TO {filepath}:");
                Wood.Indent();
                Wood.WriteLine(ioe);
                Wood.Unindent();
                return false;
            }
        }

        public static void SetTagData(string modname, string tagText)
        {
            
            if (!TagData.ContainsKey(modname)) TagData.Add(modname, tagText);
            else TagData[modname] = tagText;
        }
        public static string GetTagString(string modname)
        {
            try
            {
                if (!TagData.ContainsKey(modname)) return string.Empty;
                return TagData[modname];
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }
        public static string[] GetTagsArray(string modname)
        {
            return System.Text.RegularExpressions.Regex.Split(GetTagString(modname), ", |\n|,", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        public static void TagCleanup(string[] modnames)
        {
            Dictionary<string, string> ndic = new Dictionary<string, string>();
            foreach (string mn in modnames)
            {
                if (TagData.ContainsKey(mn) && !ndic.ContainsKey(mn)) ndic.Add(mn, TagData[mn]);
            }
            TagData = ndic;
        }
    }
}
