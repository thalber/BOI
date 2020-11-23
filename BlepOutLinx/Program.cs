using System;
using System.Windows.Forms;

namespace BlepOutLinx
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BlepOut Currblep = new BlepOut();
            Application.Run(Currblep);
            
        }
    }
}
