namespace BlepOutLinx
{
    partial class BlepOut
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlepOut));
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.TargetSelect = new System.Windows.Forms.FolderBrowserDialog();
            this.Modlist = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPathStatus = new System.Windows.Forms.Label();
            this.lblProcessStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelDesc = new System.Windows.Forms.Label();
            this.labelHead = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rwp = new System.Diagnostics.Process();
            this.btn_Help = new System.Windows.Forms.Button();
            this.buttonUprootPart = new System.Windows.Forms.Button();
            this.buttonClearMeta = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOption = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectPath
            // 
            resources.ApplyResources(this.btnSelectPath, "btnSelectPath");
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.buttonSelectPath_Click);
            // 
            // TargetSelect
            // 
            resources.ApplyResources(this.TargetSelect, "TargetSelect");
            this.TargetSelect.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.TargetSelect.ShowNewFolderButton = false;
            // 
            // Modlist
            // 
            resources.ApplyResources(this.Modlist, "Modlist");
            this.Modlist.CheckOnClick = true;
            this.Modlist.FormattingEnabled = true;
            this.Modlist.Name = "Modlist";
            this.Modlist.Sorted = true;
            this.Modlist.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Modlist_ItemCheck);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btnSelectPath, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnLaunch, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // btnLaunch
            // 
            resources.ApplyResources(this.btnLaunch, "btnLaunch");
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnLaunch_MouseClick);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.lblPathStatus, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblProcessStatus, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // lblPathStatus
            // 
            resources.ApplyResources(this.lblPathStatus, "lblPathStatus");
            this.lblPathStatus.AutoEllipsis = true;
            this.lblPathStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPathStatus.Name = "lblPathStatus";
            this.lblPathStatus.UseMnemonic = false;
            // 
            // lblProcessStatus
            // 
            resources.ApplyResources(this.lblProcessStatus, "lblProcessStatus");
            this.lblProcessStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProcessStatus.Name = "lblProcessStatus";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.labelDesc, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelHead, 0, 0);
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // labelDesc
            // 
            resources.ApplyResources(this.labelDesc, "labelDesc");
            this.labelDesc.Name = "labelDesc";
            // 
            // labelHead
            // 
            resources.ApplyResources(this.labelHead, "labelHead");
            this.labelHead.Name = "labelHead";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.Modlist, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // rwp
            // 
            this.rwp.StartInfo.Domain = "";
            this.rwp.StartInfo.LoadUserProfile = false;
            this.rwp.StartInfo.Password = null;
            this.rwp.StartInfo.StandardErrorEncoding = null;
            this.rwp.StartInfo.StandardOutputEncoding = null;
            this.rwp.StartInfo.UserName = "";
            this.rwp.SynchronizingObject = this;
            // 
            // btn_Help
            // 
            resources.ApplyResources(this.btn_Help, "btn_Help");
            this.btn_Help.Name = "btn_Help";
            this.btn_Help.UseVisualStyleBackColor = true;
            this.btn_Help.Click += new System.EventHandler(this.btn_Help_Click);
            // 
            // buttonUprootPart
            // 
            resources.ApplyResources(this.buttonUprootPart, "buttonUprootPart");
            this.buttonUprootPart.Name = "buttonUprootPart";
            this.buttonUprootPart.UseVisualStyleBackColor = true;
            this.buttonUprootPart.Click += new System.EventHandler(this.buttonUprootPart_Click);
            // 
            // buttonClearMeta
            // 
            resources.ApplyResources(this.buttonClearMeta, "buttonClearMeta");
            this.buttonClearMeta.Name = "buttonClearMeta";
            this.buttonClearMeta.UseVisualStyleBackColor = true;
            this.buttonClearMeta.Click += new System.EventHandler(this.buttonClearMeta_Click);
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.buttonClearMeta, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonUprootPart, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btn_Help, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonOption, 3, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // buttonOption
            // 
            resources.ApplyResources(this.buttonOption, "buttonOption");
            this.buttonOption.Name = "buttonOption";
            this.buttonOption.UseVisualStyleBackColor = true;
            this.buttonOption.Click += new System.EventHandler(this.buttonOption_Click);
            // 
            // BlepOut
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel5);
            this.MaximizeBox = false;
            this.Name = "BlepOut";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Activated += new System.EventHandler(this.BlepOut_Activated);
            this.Deactivate += new System.EventHandler(this.BlepOut_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BlepOut_FormClosing);
            this.Enter += new System.EventHandler(this.BlepOut_Activated);
            this.Leave += new System.EventHandler(this.BlepOut_Deactivate);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.FolderBrowserDialog TargetSelect;
        private System.Windows.Forms.CheckedListBox Modlist;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelHead;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblPathStatus;
        private System.Windows.Forms.Label lblProcessStatus;
        private System.Diagnostics.Process rwp;
        private System.Windows.Forms.Button btn_Help;
        private System.Windows.Forms.Label labelDesc;
        public System.Windows.Forms.Button buttonClearMeta;
        public System.Windows.Forms.Button buttonUprootPart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button buttonOption;
    }
}

