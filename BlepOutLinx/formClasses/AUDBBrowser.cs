using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Blep.Backend;

namespace Blep
{
    public partial class AUDBBrowser : Form
    {
        public AUDBBrowser()
        {
            InitializeComponent();
            FetchAndRefresh();
        }

        private void RefreshTriggered(object sender, EventArgs e)
        {
            FetchAndRefresh();
        }
        
        public void FetchAndRefresh()
        {
            VoiceOfBees.FetchList();
            listAUDBEntries.Items.Clear();
            foreach (var rel in VoiceOfBees.EntryList)
            {
                listAUDBEntries.Items.Add(rel);
            }
            DrawBoxes();
        }

        public void DrawBoxes()
        {
            var currEntry = listAUDBEntries.SelectedItem as VoiceOfBees.AUDBEntryRelay;

            labelEntryAuthors.Text = currEntry?.author ?? string.Empty;
            labelEntryDescription.Text = currEntry?.description ?? string.Empty;
            labelEntryName.Text = currEntry?.name ?? string.Empty;
            listDeps.Items.Clear();
            if (currEntry?.deps != null) foreach (var dep in currEntry.deps) listDeps.Items.Add(currEntry);
            labelOperationStatus.Text = "[Idle]";
        }

        private void listAUDBEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawBoxes();
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            
            var currEntry = listAUDBEntries.SelectedItem as VoiceOfBees.AUDBEntryRelay;
            if (currEntry != null)
            {
                labelOperationStatus.Text = $"Downloading {currEntry.name} and dependencies...";
                labelOperationStatus.Text = (currEntry.TryDownload(BlepOut.ModFolder))? $"Downloaded {currEntry.name}." : $"Could not download {currEntry.name}! Check BOILOG.txt for details";
            }
        }
    }
}
