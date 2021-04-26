using System;
using System.Windows.Forms;

namespace Blep
{
    public partial class PartYeet : Form
    {
        public PartYeet(Blep.BlepOut mainform)
        {
            mf = mainform;
            mf.Enabled = false;
            InitializeComponent();
        }

        private Blep.BlepOut mf;

        private void buttonUproot_Click(object sender, EventArgs e)
        {
            System.IO.Directory.Delete(Blep.BlepOut.RootPath + @"\RainWorld_Data\Managed", true);
            System.IO.Directory.Move(Blep.BlepOut.RootPath + @"\RainWorld_Data\Managed_backup", Blep.BlepOut.RootPath + @"\RainWorld_Data\Managed");
            buttonUproot.Visible = false;
            label2.Text = "Partiality Launcher successfully uninstalled, you're free to go!";
            buttonCancel.Text = "Back";
            mf.buttonUprootPart.Visible = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PartYeet_FormClosed(object sender, FormClosedEventArgs e)
        {
            mf.Enabled = true;
        }
    }
}
