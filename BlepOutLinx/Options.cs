using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using BlepOutLinx;
using System.Diagnostics;

namespace BlepOutIn
{
    public partial class Options : Form
    {
        public Options(BlepOut mainForm)
        {
            InitializeComponent();
            mf = mainForm;
            Debug.WriteLine("Options window opened " + DateTime.Now);
            labelREGIONNAME.Text = string.Empty;
            labelREGIONDESC.Text = string.Empty;
            labelSTRUCTURESTATUS.Text = string.Empty;
            regmodlist = new List<RegModData>();
            FetchStuff();
        }
        public BlepOutLinx.BlepOut mf;
        private bool readytoapply = true;
        private RegModData curRmd;
        private List<RegModData> regmodlist;

        private void FetchStuff()
        {
            Debug.WriteLine("Fetching jsons and stuff.");
            CRSlist.Items.Clear();
            regmodlist.Clear();
            if (Directory.Exists(CRSpath))
            {
                string[] CRcts = Directory.GetDirectories(CRSpath);
                foreach (string path in CRcts)
                {
                    regmodlist.Add(new RegModData(path));
                }
                foreach (RegModData rmd in regmodlist)
                {
                    CRSlist.Items.Add(rmd);
                }

            }
            labelCRSCTR.Text = CRSlist.Items.Count.ToString();
            EDTCFGDATA.loadJo();

            if (Directory.Exists(langinplugins))
            {
                labelCOMMODSTATUS.Text = "Everything seems fine.";
                labelCOMMODDETAILS.Text = "Language folder is in the correct spot.";
                buttonMM2P.Visible = false;
            }
            else if (Directory.Exists(langinmods))
            {
                labelCOMMODSTATUS.Text = "Language folder is in the wrong spot!";
                labelCOMMODDETAILS.Text = @"Translation patch files have been found inside Mods but not Plugins. Press the button below to order BOI to move translation patch to RainWorld\BepInEx\plugins, or move it manually if you want it enabled.";
                buttonMM2P.Visible = true;
            }
            else
            {
                labelCOMMODSTATUS.Text = "Nothing found.";
                labelCOMMODDETAILS.Text = string.Empty;
                
                buttonMM2P.Visible = false;
            }
        }
        private void ApplyStuff()
        {
            foreach (RegModData rmd in regmodlist)
            {
                if (rmd.hasBeenChanged) rmd.WriteRegInfo();
            }
            if (EDTCFGDATA.hasBeenChanged)
            {
                EDTCFGDATA.SaveJo();
            }
        }
        private void DrawCRSpage()
        {
            readytoapply = false;
            RegModData rmd = (CRSlist.SelectedIndex != -1) ? CRSlist.Items[CRSlist.SelectedIndex] as RegModData : null;
            curRmd = rmd;
            if (rmd == null)
            {
                labelREGIONNAME.Text = string.Empty;
                labelREGIONDESC.Text = string.Empty;
                labelSTRUCTURESTATUS.Text = string.Empty;
                checkBoxCR.Enabled = false;
                checkBoxCR.Checked = false;
                tbLOADORDER.Enabled = false;
                tbLOADORDER.Clear();
            }
            else
            {
                labelREGIONNAME.Text = rmd.regionName;
                labelREGIONDESC.Text = rmd.description;
                labelSTRUCTURESTATUS.Text = (rmd.structureValid) ? "VALID" : "INVALID";
                checkBoxCR.Enabled = rmd.CurrCfgState != RegModData.CfgState.None;
                tbLOADORDER.Enabled = rmd.CurrCfgState != RegModData.CfgState.None;
                tbLOADORDER.Text = (rmd.loadOrder != null) ? rmd.loadOrder.ToString() : string.Empty;
                checkBoxCR.Checked = rmd.activated;
            }
            readytoapply = true;
            
        }
        private void DrawEDTpage()
        {
            readytoapply = false;
            EDTCFGDATA.loadJo();
            if (EDTCFGDATA.jo == null)
            {
                tableLayoutPanel10.Enabled = false;
            }
            else
            {
                tableLayoutPanel10.Enabled = true;
                textBoxEDT_STARTMAP.Text = EDTCFGDATA.startmap;
                checkBoxEDT_QUICKSTART.Checked = (bool)EDTCFGDATA.skiptitle;
                textBoxEDT_CHARSELECT.Text = (EDTCFGDATA.forcechar == null) ? string.Empty : EDTCFGDATA.forcechar.ToString();
                checkBoxEDT_DISABLERAIN.Checked = (bool)EDTCFGDATA.norain;
                checkBoxEDT_EDT.Checked = (bool)EDTCFGDATA.devtools;
                TextBoxEDT_CHEATKARMA.Text = (EDTCFGDATA.cheatkarma == null) ? string.Empty : EDTCFGDATA.cheatkarma.ToString();
                checkBoxEDT_MAPREVEAL.Checked = (bool)EDTCFGDATA.revealmap;
                checkBoxEDT_FORCEGLOW.Checked = (bool)EDTCFGDATA.forcelight;
                checkBoxEDT_BAKE.Checked = (bool)EDTCFGDATA.bake;
                checkBoxEDT_ENCRYPT.Checked = (bool)EDTCFGDATA.encrypt;
            }
            readytoapply = true;
        }
        private static string CRSpath => BlepOut.ModFolder + @"CustomResources";
        private void Options_Activated(object sender, EventArgs e)
        {
            //this.Enabled = mf.Enabled;
            if (mf.IsMyPathCorrect)
            {
                //StatusUpdate();
                FetchStuff();
            }
            DrawCRSpage();
            DrawEDTpage();
        }
        private void Options_Deactivate(object sender, EventArgs e)
        {
            ApplyStuff();
        }
        private void CRSlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawCRSpage();
        }
        private void checkBoxCR_EnabledChanged(object sender, EventArgs e)
        {
            if (curRmd == null) return;
            curRmd.activated = checkBoxCR.Checked;
        }
        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApplyStuff();
        }
        private void tbLOADORDER_Leave(object sender, EventArgs e)
        {
            if (curRmd == null || !readytoapply) return;
            if (int.TryParse(tbLOADORDER.Text, out int i))
            {
                curRmd.loadOrder = i;
            }
            
        }
        private void EDT_PROPERTY_CHANGED(object sender, EventArgs e)
        {
            if (!readytoapply) return;
            if (sender == textBoxEDT_STARTMAP)
            {
                EDTCFGDATA.startmap = textBoxEDT_STARTMAP.Text;
            }
            else if (sender == checkBoxEDT_QUICKSTART)
            {
                EDTCFGDATA.skiptitle = checkBoxEDT_QUICKSTART.Checked;
            }
            else if (sender == textBoxEDT_CHARSELECT)
            {
                if (int.TryParse(textBoxEDT_CHARSELECT.Text, out int res))
                {
                    EDTCFGDATA.forcechar = res;
                }

            }
            else if (sender == checkBoxEDT_DISABLERAIN)
            {
                EDTCFGDATA.norain = checkBoxEDT_DISABLERAIN.Checked;
            }
            else if (sender == checkBoxEDT_EDT)
            {
                EDTCFGDATA.devtools = checkBoxEDT_EDT.Checked;
            }
            else if (sender == TextBoxEDT_CHEATKARMA)
            {
                if (int.TryParse(TextBoxEDT_CHEATKARMA.Text, out int res))
                {
                    EDTCFGDATA.cheatkarma = res;
                }
            }
            else if (sender == checkBoxEDT_MAPREVEAL)
            {
                EDTCFGDATA.revealmap = checkBoxEDT_MAPREVEAL.Checked;
            }
            else if (sender == checkBoxEDT_FORCEGLOW)
            {
                EDTCFGDATA.forcelight = checkBoxEDT_FORCEGLOW.Checked;
            }
            else if (sender == checkBoxEDT_BAKE)
            {
                EDTCFGDATA.bake = checkBoxEDT_FORCEGLOW.Checked;
            }
            else if (sender == checkBoxEDT_ENCRYPT)
            {
                EDTCFGDATA.encrypt = checkBoxEDT_ENCRYPT.Checked;
            }
        }
        private void buttonclickCOMMOD(object sender, EventArgs e)
        {
            try
            {
                Directory.Move(langinmods, langinplugins);

            }
            catch (IOException ioe)
            {
                Debug.WriteLine("ERROR MOVING LANGUAGE FOLDER:");
                Debug.Indent();
                Debug.WriteLine(ioe);
                Debug.Unindent();
            }
            ApplyStuff();
            FetchStuff();
        }

        string langinplugins => Path.Combine(BlepOut.PluginsFolder, "Language");
        string langinmods => Path.Combine(BlepOut.ModFolder + "Language");
    }

    

}