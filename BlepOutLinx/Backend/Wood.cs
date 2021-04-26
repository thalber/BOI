using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Blep.Backend
{
    public static class Wood
    {
        public static void Indent()
        {
            IndentLevel++;
        }
        public static void Unindent()
        {
            IndentLevel--;
        }
        public static void WriteLineIf(bool cond, object o)
        {
            if (cond) WriteLine(o);
        }
        public static void WriteLine(object o, int AddedIndent)
        {
            IndentLevel += AddedIndent;
            WriteLine(o);
            IndentLevel -= AddedIndent;
        }
        public static void WriteLine(object o)
        {
            string result = string.Empty;
            for (int i = 0; i < IndentLevel; i++) { result += "\t"; }
            result += o?.ToString() ?? "null";
            result += "\n";
            Write(result);
        }
        public static void WriteLine()
        {
            WriteLine(string.Empty);
        }
        public static void Write(object o)
        {
            if (LogPath == null || !File.Exists(LogPath) || !LogPath.EndsWith(".txt"))
            {
                LogPath = Path.Combine(Directory.GetCurrentDirectory(), "BOILOG.txt");
            }
            FileInfo lf = new FileInfo(LogPath);
            try
            {
                string result = o?.ToString() ?? "null";
                File.AppendAllText(LogPath, result);
            }
            catch (IOException)
            {

            }
        }

        public static string LogPath { get; set; } = string.Empty;

        public static int IndentLevel { get { return _il; } set { _il = Math.Max(value, 0); } }
        private static int _il = 0;
    }
}
