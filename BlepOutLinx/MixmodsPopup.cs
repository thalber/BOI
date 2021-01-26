using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Blep
{
    public partial class MixmodsPopup : Form
    {
        public MixmodsPopup(List<string> slist)
        {
            InitializeComponent();
            foreach (string s in slist)
            {
                listView.Items.Add(s);
            }
        }
        

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
