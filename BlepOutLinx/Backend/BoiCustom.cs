using System;
using System.IO;
using System.Diagnostics;

namespace Blep.Backend
{
    public static class BoiCustom
    {
        public static bool BOIC_Bytearr_Compare(byte[] a, byte[] b)
        {
            if (a == null || b == null) return false;
            if (a.Length == 0 || b.Length == 0) return false;
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }
        public static int BOIC_RecursiveDirectoryCopy(string from, string to)
        {
            int errc = 0;
            DirectoryInfo din = new DirectoryInfo(from);
            DirectoryInfo dout = new DirectoryInfo(to);
            if (!din.Exists) { throw new IOException($"An attempt to copy a nonexistent directory ({from}) to {to} has occured."); }
            if (!dout.Exists) Directory.CreateDirectory(to);
            foreach (FileInfo fi in din.GetFiles())
            {
                try { File.Copy(fi.FullName, Path.Combine(to, fi.Name)); }
                catch (IOException ioe)
                {
                    Wood.Write("Could not copy a file during recursive copy process");
                    Wood.Indent();
                    Wood.WriteLine(ioe);
                    Wood.Unindent();
                    errc++;
                }

            }
            foreach (DirectoryInfo di in din.GetDirectories())
            {
                try { errc += BOIC_RecursiveDirectoryCopy(di.FullName, Path.Combine(to, di.Name)); }
                catch (IOException ioe)
                {
                    Wood.Write("Could not copy a subfolder during recursive copy process");
                    Wood.Indent();
                    Wood.WriteLine(ioe);
                    Wood.Unindent();
                    
                }
                
            }
            return errc;
        }
    }

    
}
