using System;
using System.Windows.Forms;

namespace Blep
{
    public partial class InvalidModPopup : Form
    {
        public InvalidModPopup(Blep.BlepOut mainfotm, string modname)
        {
            InitializeComponent();
            mf = mainfotm;
            if (modname != null)
            {
                label1.Text = ($"The mod you are trying to enable ({modname}) is INVALID. Launching the game with it will most likely result in an immediate crash.\nFor more details, see HELP&&INFO window.");
            }
            mf.Enabled = false;
        }

        private Blep.BlepOut mf;

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InvalidModPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            mf.Enabled = true;
        }
    }
}
