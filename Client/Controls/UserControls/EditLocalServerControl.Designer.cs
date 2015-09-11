namespace ConnectUO.Controls
{
    partial class EditLocalServerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtHostAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkRemoveEnc = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClearAll = new ConnectUO.Controls.ArrowButton();
            this.btnIncreaseVersion = new ConnectUO.Controls.ArrowButton();
            this.btnRemovePatch = new ConnectUO.Controls.ArrowButton();
            this.btnAddUrl = new ConnectUO.Controls.ArrowButton();
            this.btnAddFile = new ConnectUO.Controls.ArrowButton();
            this.lstPatches = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSubmit = new ConnectUO.Controls.ArrowButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Host Address:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(423, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Port:";
            // 
            // txtServerName
            // 
            this.txtServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerName.Location = new System.Drawing.Point(118, 23);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(417, 20);
            this.txtServerName.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(118, 49);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(417, 116);
            this.txtDescription.TabIndex = 5;
            // 
            // txtHostAddress
            // 
            this.txtHostAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHostAddress.Location = new System.Drawing.Point(118, 171);
            this.txtHostAddress.Name = "txtHostAddress";
            this.txtHostAddress.Size = new System.Drawing.Size(299, 20);
            this.txtHostAddress.TabIndex = 6;
            // 
            // txtPort
            // 
            this.txtPort.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtPort.Location = new System.Drawing.Point(458, 171);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(77, 20);
            this.txtPort.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.chkRemoveEnc);
            this.groupBox1.Controls.Add(this.txtServerName);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtHostAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(541, 222);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Information";
            // 
            // chkRemoveEnc
            // 
            this.chkRemoveEnc.AutoSize = true;
            this.chkRemoveEnc.Location = new System.Drawing.Point(118, 197);
            this.chkRemoveEnc.Name = "chkRemoveEnc";
            this.chkRemoveEnc.Size = new System.Drawing.Size(136, 17);
            this.chkRemoveEnc.TabIndex = 8;
            this.chkRemoveEnc.Text = "Patch Client Encryption";
            this.chkRemoveEnc.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.btnClearAll);
            this.groupBox2.Controls.Add(this.btnIncreaseVersion);
            this.groupBox2.Controls.Add(this.btnRemovePatch);
            this.groupBox2.Controls.Add(this.btnAddUrl);
            this.groupBox2.Controls.Add(this.btnAddFile);
            this.groupBox2.Controls.Add(this.lstPatches);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(541, 138);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Patch Information";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(25, 105);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Selectable = true;
            this.btnClearAll.Selected = false;
            this.btnClearAll.Size = new System.Drawing.Size(51, 14);
            this.btnClearAll.TabIndex = 15;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnIncreaseVersion
            // 
            this.btnIncreaseVersion.Enabled = false;
            this.btnIncreaseVersion.Location = new System.Drawing.Point(25, 85);
            this.btnIncreaseVersion.Name = "btnIncreaseVersion";
            this.btnIncreaseVersion.Selectable = true;
            this.btnIncreaseVersion.Selected = false;
            this.btnIncreaseVersion.Size = new System.Drawing.Size(93, 14);
            this.btnIncreaseVersion.TabIndex = 14;
            this.btnIncreaseVersion.Text = "Increase Version";
            this.btnIncreaseVersion.Click += new System.EventHandler(this.btnIncreaseVersion_Click);
            // 
            // btnRemovePatch
            // 
            this.btnRemovePatch.Enabled = false;
            this.btnRemovePatch.Location = new System.Drawing.Point(25, 65);
            this.btnRemovePatch.Name = "btnRemovePatch";
            this.btnRemovePatch.Selectable = true;
            this.btnRemovePatch.Selected = false;
            this.btnRemovePatch.Size = new System.Drawing.Size(50, 14);
            this.btnRemovePatch.TabIndex = 13;
            this.btnRemovePatch.Text = "Remove";
            this.btnRemovePatch.Click += new System.EventHandler(this.btnRemovePatch_Click_1);
            // 
            // btnAddUrl
            // 
            this.btnAddUrl.Location = new System.Drawing.Point(25, 45);
            this.btnAddUrl.Name = "btnAddUrl";
            this.btnAddUrl.Selectable = true;
            this.btnAddUrl.Selected = false;
            this.btnAddUrl.Size = new System.Drawing.Size(54, 14);
            this.btnAddUrl.TabIndex = 12;
            this.btnAddUrl.Text = "Add URL";
            this.btnAddUrl.Click += new System.EventHandler(this.btnAddUrl_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(25, 25);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Selectable = true;
            this.btnAddFile.Selected = false;
            this.btnAddFile.Size = new System.Drawing.Size(50, 14);
            this.btnAddFile.TabIndex = 11;
            this.btnAddFile.Text = "Add File";
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // lstPatches
            // 
            this.lstPatches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPatches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPatches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstPatches.FullRowSelect = true;
            this.lstPatches.GridLines = true;
            this.lstPatches.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstPatches.Location = new System.Drawing.Point(126, 19);
            this.lstPatches.MultiSelect = false;
            this.lstPatches.Name = "lstPatches";
            this.lstPatches.Size = new System.Drawing.Size(409, 112);
            this.lstPatches.TabIndex = 0;
            this.lstPatches.UseCompatibleStateImageBehavior = false;
            this.lstPatches.View = System.Windows.Forms.View.Details;
            this.lstPatches.SelectedIndexChanged += new System.EventHandler(this.lstPatches_SelectedIndexChanged);
            this.lstPatches.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPatches_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Path";
            this.columnHeader1.Width = 345;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Version";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "Zip files|*.zip|Rar files|*.rar|All files|*.*";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(570, 415);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(14, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 384);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSubmit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 360);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(541, 24);
            this.panel2.TabIndex = 10;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.BackColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(510, 6);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Selectable = true;
            this.btnSubmit.Selected = false;
            this.btnSubmit.Size = new System.Drawing.Size(25, 14);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "OK";
            // 
            // EditLocalServerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EditLocalServerControl";
            this.Size = new System.Drawing.Size(570, 415);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtHostAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstPatches;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox chkRemoveEnc;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private ArrowButton btnAddFile;
        private ArrowButton btnIncreaseVersion;
        private ArrowButton btnRemovePatch;
        private ArrowButton btnAddUrl;
        private ArrowButton btnClearAll;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ArrowButton btnSubmit;
    }
}
