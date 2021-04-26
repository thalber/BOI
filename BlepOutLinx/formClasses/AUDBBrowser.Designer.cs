
namespace Blep
{
    partial class AUDBBrowser
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRefreshEntries = new System.Windows.Forms.Button();
            this.labelEntryName = new System.Windows.Forms.Label();
            this.labelEntryAuthors = new System.Windows.Forms.Label();
            this.labelEntryDescription = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.listAUDBEntries = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listDeps = new System.Windows.Forms.ListBox();
            this.labelOperationStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Controls.Add(this.labelEntryName, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelEntryAuthors, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelEntryDescription, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.listAUDBEntries, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listDeps, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelOperationStatus, 5, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.buttonDownload, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonRefreshEntries, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(460, 317);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonRefreshEntries
            // 
            this.buttonRefreshEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.buttonRefreshEntries, 2);
            this.buttonRefreshEntries.Location = new System.Drawing.Point(256, 1);
            this.buttonRefreshEntries.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRefreshEntries.Name = "buttonRefreshEntries";
            this.buttonRefreshEntries.Size = new System.Drawing.Size(203, 45);
            this.buttonRefreshEntries.TabIndex = 4;
            this.buttonRefreshEntries.Text = "Refresh list";
            this.buttonRefreshEntries.UseVisualStyleBackColor = true;
            this.buttonRefreshEntries.Click += new System.EventHandler(this.RefreshTriggered);
            // 
            // labelEntryName
            // 
            this.labelEntryName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEntryName.AutoSize = true;
            this.labelEntryName.BackColor = System.Drawing.Color.White;
            this.labelEntryName.Location = new System.Drawing.Point(338, 48);
            this.labelEntryName.Margin = new System.Windows.Forms.Padding(1);
            this.labelEntryName.Name = "labelEntryName";
            this.labelEntryName.Size = new System.Drawing.Size(120, 38);
            this.labelEntryName.TabIndex = 6;
            this.labelEntryName.Text = "[name]";
            this.labelEntryName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelEntryAuthors
            // 
            this.labelEntryAuthors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEntryAuthors.AutoSize = true;
            this.labelEntryAuthors.BackColor = System.Drawing.Color.White;
            this.labelEntryAuthors.Location = new System.Drawing.Point(338, 89);
            this.labelEntryAuthors.Margin = new System.Windows.Forms.Padding(1);
            this.labelEntryAuthors.Name = "labelEntryAuthors";
            this.labelEntryAuthors.Size = new System.Drawing.Size(120, 38);
            this.labelEntryAuthors.TabIndex = 7;
            this.labelEntryAuthors.Text = "[authors]";
            this.labelEntryAuthors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelEntryDescription
            // 
            this.labelEntryDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEntryDescription.AutoSize = true;
            this.labelEntryDescription.BackColor = System.Drawing.Color.White;
            this.labelEntryDescription.Location = new System.Drawing.Point(338, 130);
            this.labelEntryDescription.Margin = new System.Windows.Forms.Padding(1);
            this.labelEntryDescription.Name = "labelEntryDescription";
            this.tableLayoutPanel1.SetRowSpan(this.labelEntryDescription, 2);
            this.labelEntryDescription.Size = new System.Drawing.Size(120, 79);
            this.labelEntryDescription.TabIndex = 8;
            this.labelEntryDescription.Text = "[description]";
            this.labelEntryDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 129);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.tableLayoutPanel1.SetRowSpan(this.label2, 2);
            this.label2.Size = new System.Drawing.Size(80, 81);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(256, 88);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 40);
            this.label3.TabIndex = 3;
            this.label3.Text = "Author(s)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonDownload
            // 
            this.buttonDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.buttonDownload, 2);
            this.buttonDownload.Location = new System.Drawing.Point(1, 1);
            this.buttonDownload.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(122, 45);
            this.buttonDownload.TabIndex = 9;
            this.buttonDownload.Text = "Download or update";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // listAUDBEntries
            // 
            this.listAUDBEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.listAUDBEntries, 2);
            this.listAUDBEntries.FormattingEnabled = true;
            this.listAUDBEntries.IntegralHeight = false;
            this.listAUDBEntries.Location = new System.Drawing.Point(125, 48);
            this.listAUDBEntries.Margin = new System.Windows.Forms.Padding(1);
            this.listAUDBEntries.Name = "listAUDBEntries";
            this.tableLayoutPanel1.SetRowSpan(this.listAUDBEntries, 5);
            this.listAUDBEntries.Size = new System.Drawing.Size(129, 267);
            this.listAUDBEntries.TabIndex = 0;
            this.listAUDBEntries.SelectedIndexChanged += new System.EventHandler(this.listAUDBEntries_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label5, 2);
            this.label5.Location = new System.Drawing.Point(1, 47);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 40);
            this.label5.TabIndex = 12;
            this.label5.Text = "Dependencies";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listDeps
            // 
            this.listDeps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.listDeps, 2);
            this.listDeps.FormattingEnabled = true;
            this.listDeps.IntegralHeight = false;
            this.listDeps.Location = new System.Drawing.Point(2, 89);
            this.listDeps.Margin = new System.Windows.Forms.Padding(1);
            this.listDeps.Name = "listDeps";
            this.tableLayoutPanel1.SetRowSpan(this.listDeps, 4);
            this.listDeps.Size = new System.Drawing.Size(120, 226);
            this.listDeps.TabIndex = 13;
            // 
            // labelOperationStatus
            // 
            this.labelOperationStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOperationStatus.AutoSize = true;
            this.labelOperationStatus.BackColor = System.Drawing.Color.White;
            this.labelOperationStatus.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOperationStatus.Location = new System.Drawing.Point(338, 212);
            this.labelOperationStatus.Margin = new System.Windows.Forms.Padding(1);
            this.labelOperationStatus.Name = "labelOperationStatus";
            this.labelOperationStatus.Size = new System.Drawing.Size(120, 103);
            this.labelOperationStatus.TabIndex = 10;
            this.labelOperationStatus.Text = "[opstatus]";
            this.labelOperationStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(256, 211);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(80, 105);
            this.label4.TabIndex = 11;
            this.label4.Text = "Status";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AUDBBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 341);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MinimumSize = new System.Drawing.Size(500, 380);
            this.Name = "AUDBBrowser";
            this.ShowIcon = false;
            this.Text = "Browse AUDB";
            this.Activated += new System.EventHandler(this.RefreshTriggered);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listAUDBEntries;
        private System.Windows.Forms.Button buttonRefreshEntries;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelEntryName;
        private System.Windows.Forms.Label labelEntryAuthors;
        private System.Windows.Forms.Label labelEntryDescription;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelOperationStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listDeps;
    }
}