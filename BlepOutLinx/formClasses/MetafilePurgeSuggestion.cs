using System;
using System.IO;
using System.Windows.Forms;

namespace Blep
{
    public partial class MetafilePurgeSuggestion : Form
    {
        public MetafilePurgeSuggestion(Blep.BlepOut mainform)
        {
            mf = mainform;
            InitializeComponent();
            mf.Enabled = false;
        }

        private Blep.BlepOut mf;

        private void buttonUproot_Click(object sender, EventArgs e)
        {
            
            string[] modfoldercontents = Directory.GetFiles(Blep.BlepOut.ModFolder);
            foreach (string path in modfoldercontents)
            {
                var fi = new FileInfo(path);
                if (fi.Extension == ".modHash" || fi.Extension == ".modMeta")
                {
                    File.Delete(path);
                }
            }
            mf.buttonClearMeta.Visible = false;
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
            Close();
        }
    }
}
