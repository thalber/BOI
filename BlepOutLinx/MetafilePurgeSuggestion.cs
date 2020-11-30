using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace BlepOutIn
{
    public partial class MetafilePurgeSuggestion : Form
    {
        public MetafilePurgeSuggestion(BlepOutLinx.BlepOut mainform)
        {
            mf = mainform;
            InitializeComponent();
            mf.Enabled = false;
        }
        BlepOutLinx.BlepOut mf;

        private void buttonUproot_Click(object sender, EventArgs e)
        {
            string[] modfoldercontents = Directory.GetFiles(BlepOutLinx.BlepOut.ModFolder);
            foreach (string path in modfoldercontents)
            {
                var fi = new FileInfo(path);
                if (fi.Extension == ".modHash" || fi.Extension == ".modMeta")
                {
                    File.Delete(path);
                }
            }
            buttonUproot.Visible = false;
            buttonCancel.Text = "Back";
            label2.Text = "Hash and meta files deleted successfully. Your karma just went up a notch.";
        }

        private void MetafilePurgeSuggestion_FormClosed(object sender, FormClosedEventArgs e)
        {
            mf.Enabled = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
