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
    public partial class InvalidModPopup : Form
    {
        public InvalidModPopup(BlepOutLinx.BlepOut mainfotm, string modname)
        {
            InitializeComponent();
            mf = mainfotm;
            if (modname != null)
            {
                label1.Text = ($"The mod you are trying to enable ({modname}) is INVALID and can NOT function with BepInEx. This usually means that the mod assembly mixes MonoMod patches and Partiality mods. For more details, see HELP&&INFO window.");
            }
            mf.Enabled = false;
        }
        BlepOutLinx.BlepOut mf;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InvalidModPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            mf.Enabled = true;
        }
    }
}
