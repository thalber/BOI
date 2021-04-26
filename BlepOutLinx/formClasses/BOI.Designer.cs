namespace Blep
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
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPathStatus = new System.Windows.Forms.Label();
            this.lblProcessStatus = new System.Windows.Forms.Label();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelDesc = new System.Windows.Forms.Label();
            this.labelHead = new System.Windows.Forms.Label();
            this.rwp = new System.Diagnostics.Process();
            this.btn_Help = new System.Windows.Forms.Button();
            this.buttonUprootPart = new System.Windows.Forms.Button();
            this.buttonClearMeta = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonBrowseAUDB = new System.Windows.Forms.Button();
            this.buttonOption = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.MaskModeSelect = new System.Windows.Forms.ComboBox();
            this.textBox_MaskInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TagInputBox = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
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
            this.Modlist.AllowDrop = true;
            resources.ApplyResources(this.Modlist, "Modlist");
            this.Modlist.FormattingEnabled = true;
            this.Modlist.Name = "Modlist";
            this.Modlist.Sorted = true;
            this.Modlist.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Modlist_ItemCheck);
            this.Modlist.SelectedIndexChanged += new System.EventHandler(this.Modlist_SelectionChanged);
            this.Modlist.DragDrop += new System.Windows.Forms.DragEventHandler(this.Modlist_DragDrop);
            this.Modlist.DragEnter += new System.Windows.Forms.DragEventHandler(this.Modlist_DragEnter);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btnSelectPath, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnLaunch, 2, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
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
            // btnLaunch
            // 
            resources.ApplyResources(this.btnLaunch, "btnLaunch");
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnLaunch_MouseClick);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.labelDesc, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelHead, 0, 0);
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
            this.tableLayoutPanel3.SetColumnSpan(this.tableLayoutPanel5, 2);
            this.tableLayoutPanel5.Controls.Add(this.buttonBrowseAUDB, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonClearMeta, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonUprootPart, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btn_Help, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonOption, 3, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // buttonBrowseAUDB
            // 
            resources.ApplyResources(this.buttonBrowseAUDB, "buttonBrowseAUDB");
            this.buttonBrowseAUDB.Name = "buttonBrowseAUDB";
            this.buttonBrowseAUDB.UseVisualStyleBackColor = true;
            this.buttonBrowseAUDB.Click += new System.EventHandler(this.OpenAudbBrowser);
            // 
            // buttonOption
            // 
            resources.ApplyResources(this.buttonOption, "buttonOption");
            this.buttonOption.Name = "buttonOption";
            this.buttonOption.UseVisualStyleBackColor = true;
            this.buttonOption.Click += new System.EventHandler(this.buttonOption_Click);
            // 
            // tableLayoutPanel6
            // 
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.textBox_MaskInput, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.TagInputBox, 0, 5);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel3.SetRowSpan(this.tableLayoutPanel6, 3);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.MaskModeSelect, 1, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // MaskModeSelect
            // 
            resources.ApplyResources(this.MaskModeSelect, "MaskModeSelect");
            this.MaskModeSelect.FormattingEnabled = true;
            this.MaskModeSelect.Name = "MaskModeSelect";
            this.MaskModeSelect.TextChanged += new System.EventHandler(this.MaskModeSelect_TextChanged);
            // 
            // textBox_MaskInput
            // 
            resources.ApplyResources(this.textBox_MaskInput, "textBox_MaskInput");
            this.textBox_MaskInput.Name = "textBox_MaskInput";
            this.textBox_MaskInput.TextChanged += new System.EventHandler(this.textBoxMaskInput_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // TagInputBox
            // 
            resources.ApplyResources(this.TagInputBox, "TagInputBox");
            this.TagInputBox.Name = "TagInputBox";
            this.TagInputBox.TextChanged += new System.EventHandler(this.TagTextChanged);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.Modlist, 0, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // BlepOut
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.tableLayoutPanel3);
            this.MaximizeBox = false;
            this.Name = "BlepOut";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Activated += new System.EventHandler(this.BlepOut_Activated);
            this.Deactivate += new System.EventHandler(this.BlepOut_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BlepOut_FormClosing);
            this.Enter += new System.EventHandler(this.BlepOut_Activated);
            this.Leave += new System.EventHandler(this.BlepOut_Deactivate);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.FolderBrowserDialog TargetSelect;
        private System.Windows.Forms.CheckedListBox Modlist;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelHead;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox MaskModeSelect;
        private System.Windows.Forms.TextBox textBox_MaskInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox TagInputBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonBrowseAUDB;
    }
}

