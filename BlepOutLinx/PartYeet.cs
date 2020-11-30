using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlepOutIn
{
    public partial class PartYeet : Form
    {
        public PartYeet(BlepOutLinx.BlepOut mainform)
        {
            mf = mainform;
            mf.Enabled = false;
            InitializeComponent();
        }
        BlepOutLinx.BlepOut mf;

        private void buttonUproot_Click(object sender, EventArgs e)
        {
            System.IO.Directory.Delete(BlepOutLinx.BlepOut.RootPath + @"\RainWorld_Data\Managed", true);
            System.IO.Directory.Move(BlepOutLinx.BlepOut.RootPath + @"\RainWorld_Data\Managed_backup", BlepOutLinx.BlepOut.RootPath + @"\RainWorld_Data\Managed");
            buttonUproot.Visible = false;
            label2.Text = "Partiality Launcher successfully uninstalled, you're free to go!";
            buttonCancel.Text = "Back";

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PartYeet_FormClosed(object sender, FormClosedEventArgs e)
        {
            mf.Enabled = true;
        }
    }
}
