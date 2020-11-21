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
            this.btnApply = new System.Windows.Forms.Button();
            this.Modlist = new System.Windows.Forms.CheckedListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelHead = new System.Windows.Forms.Label();
            this.labelDesc = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.TargetSelect.Disposed += new System.EventHandler(this.TargetSelect_Closed);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // Modlist
            // 
            resources.ApplyResources(this.Modlist, "Modlist");
            this.Modlist.FormattingEnabled = true;
            this.Modlist.Name = "Modlist";
            this.Modlist.SelectedIndexChanged += new System.EventHandler(this.checklistModlist_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btnSelectPath, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRefresh, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnApply, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
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
            this.labelHead.Click += new System.EventHandler(this.labelHead_Click);
            // 
            // labelDesc
            // 
            resources.ApplyResources(this.labelDesc, "labelDesc");
            this.labelDesc.Name = "labelDesc";
            // 
            // BlepOut
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Modlist);
            this.MaximizeBox = false;
            this.Name = "BlepOut";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.BlepOut_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.FolderBrowserDialog TargetSelect;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckedListBox Modlist;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelHead;
        private System.Windows.Forms.Label labelDesc;
    }
}

