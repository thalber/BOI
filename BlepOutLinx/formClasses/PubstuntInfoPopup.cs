using System;
using System.Windows.Forms;

namespace Blep
{
    public partial class PubstuntInfoPopup : Form
    {
        public PubstuntInfoPopup()
        {
            
            InitializeComponent();
        }

        
        private void btnExit_Click(object sender, EventArgs e)
        {
            
            Close();
        }
    }
}
