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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelHead = new System.Windows.Forms.Label();
            this.labelDesc = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.fsw_plugins = new System.IO.FileSystemWatcher();
            this.fsw_mods = new System.IO.FileSystemWatcher();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsw_plugins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fsw_mods)).BeginInit();
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
            this.Modlist.FormattingEnabled = true;
            this.Modlist.Name = "Modlist";
            this.Modlist.Sorted = true;
            this.Modlist.SelectedIndexChanged += new System.EventHandler(this.checklistModlist_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btnSelectPath, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.labelHead, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelDesc, 0, 1);
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // labelHead
            // 
            resources.ApplyResources(this.labelHead, "labelHead");
            this.labelHead.Name = "labelHead";
            // 
            // labelDesc
            // 
            resources.ApplyResources(this.labelDesc, "labelDesc");
            this.labelDesc.Name = "labelDesc";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.Modlist, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // fsw_plugins
            // 
            this.fsw_plugins.EnableRaisingEvents = true;
            this.fsw_plugins.Filter = "*.dll";
            this.fsw_plugins.SynchronizingObject = this;
            // 
            // fsw_mods
            // 
            this.fsw_mods.EnableRaisingEvents = true;
            this.fsw_mods.SynchronizingObject = this;
            // 
            // BlepOut
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.tableLayoutPanel3);
            this.MaximizeBox = false;
            this.Name = "BlepOut";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Activated += new System.EventHandler(this.BlepOut_Activated);
            this.Deactivate += new System.EventHandler(this.BlepOut_Deactivate);
            this.Load += new System.EventHandler(this.BlepOut_Load);
            this.Enter += new System.EventHandler(this.BlepOut_Activated);
            this.Validated += new System.EventHandler(this.BlepOut_Activated);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fsw_plugins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fsw_mods)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.FolderBrowserDialog TargetSelect;
        private System.Windows.Forms.CheckedListBox Modlist;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelHead;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.IO.FileSystemWatcher fsw_plugins;
        private System.IO.FileSystemWatcher fsw_mods;
    }
}

