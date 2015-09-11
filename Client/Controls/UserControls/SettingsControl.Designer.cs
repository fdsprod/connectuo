namespace ConnectUO.Controls
{
    partial class SettingsControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optionsPanel = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkMinimizeOnPlay = new System.Windows.Forms.CheckBox();
            this.btnDefaultPatchDirectory = new System.Windows.Forms.Button();
            this.btnSetPatchDirectory = new System.Windows.Forms.Button();
            this.txtPatchDirectory = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkRemoveEncrpyption = new System.Windows.Forms.CheckBox();
            this.btnSetManually = new System.Windows.Forms.Button();
            this.btnAutoDetectClient = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chkLaunchRazor = new System.Windows.Forms.CheckBox();
            this.txtClientPath = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.optionsPanel.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(485, 235);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optionsPanel);
            this.panel1.Location = new System.Drawing.Point(18, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 204);
            this.panel1.TabIndex = 3;
            // 
            // optionsPanel
            // 
            this.optionsPanel.Controls.Add(this.groupBox4);
            this.optionsPanel.Controls.Add(this.groupBox3);
            this.optionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsPanel.Location = new System.Drawing.Point(0, 0);
            this.optionsPanel.Name = "optionsPanel";
            this.optionsPanel.Padding = new System.Windows.Forms.Padding(5);
            this.optionsPanel.Size = new System.Drawing.Size(449, 204);
            this.optionsPanel.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkMinimizeOnPlay);
            this.groupBox4.Controls.Add(this.btnDefaultPatchDirectory);
            this.groupBox4.Controls.Add(this.btnSetPatchDirectory);
            this.groupBox4.Controls.Add(this.txtPatchDirectory);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(5, 113);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(439, 85);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ConnectUO";
            // 
            // chkMinimizeOnPlay
            // 
            this.chkMinimizeOnPlay.AutoSize = true;
            this.chkMinimizeOnPlay.ForeColor = System.Drawing.Color.Black;
            this.chkMinimizeOnPlay.Location = new System.Drawing.Point(9, 60);
            this.chkMinimizeOnPlay.Name = "chkMinimizeOnPlay";
            this.chkMinimizeOnPlay.Size = new System.Drawing.Size(163, 17);
            this.chkMinimizeOnPlay.TabIndex = 18;
            this.chkMinimizeOnPlay.Text = "Minimize ConnectUO on Play";
            this.chkMinimizeOnPlay.UseVisualStyleBackColor = true;
            // 
            // btnDefaultPatchDirectory
            // 
            this.btnDefaultPatchDirectory.ForeColor = System.Drawing.Color.Black;
            this.btnDefaultPatchDirectory.Location = new System.Drawing.Point(348, 32);
            this.btnDefaultPatchDirectory.Name = "btnDefaultPatchDirectory";
            this.btnDefaultPatchDirectory.Size = new System.Drawing.Size(82, 23);
            this.btnDefaultPatchDirectory.TabIndex = 17;
            this.btnDefaultPatchDirectory.Text = "Default";
            this.btnDefaultPatchDirectory.UseVisualStyleBackColor = true;
            // 
            // btnSetPatchDirectory
            // 
            this.btnSetPatchDirectory.ForeColor = System.Drawing.Color.Black;
            this.btnSetPatchDirectory.Location = new System.Drawing.Point(308, 32);
            this.btnSetPatchDirectory.Name = "btnSetPatchDirectory";
            this.btnSetPatchDirectory.Size = new System.Drawing.Size(34, 23);
            this.btnSetPatchDirectory.TabIndex = 15;
            this.btnSetPatchDirectory.Text = "Set Manually";
            this.btnSetPatchDirectory.UseVisualStyleBackColor = true;
            // 
            // txtPatchDirectory
            // 
            this.txtPatchDirectory.ForeColor = System.Drawing.Color.Black;
            this.txtPatchDirectory.Location = new System.Drawing.Point(9, 34);
            this.txtPatchDirectory.Name = "txtPatchDirectory";
            this.txtPatchDirectory.ReadOnly = true;
            this.txtPatchDirectory.Size = new System.Drawing.Size(293, 20);
            this.txtPatchDirectory.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(6, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Patch Directory:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkRemoveEncrpyption);
            this.groupBox3.Controls.Add(this.btnSetManually);
            this.groupBox3.Controls.Add(this.btnAutoDetectClient);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.chkLaunchRazor);
            this.groupBox3.Controls.Add(this.txtClientPath);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(5, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(439, 108);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ultima Online ";
            // 
            // chkRemoveEncrpyption
            // 
            this.chkRemoveEncrpyption.AutoSize = true;
            this.chkRemoveEncrpyption.ForeColor = System.Drawing.Color.Black;
            this.chkRemoveEncrpyption.Location = new System.Drawing.Point(9, 81);
            this.chkRemoveEncrpyption.Name = "chkRemoveEncrpyption";
            this.chkRemoveEncrpyption.Size = new System.Drawing.Size(136, 17);
            this.chkRemoveEncrpyption.TabIndex = 9;
            this.chkRemoveEncrpyption.Text = "Patch Client Encryption";
            this.chkRemoveEncrpyption.UseVisualStyleBackColor = true;
            // 
            // btnSetManually
            // 
            this.btnSetManually.ForeColor = System.Drawing.Color.Black;
            this.btnSetManually.Location = new System.Drawing.Point(308, 30);
            this.btnSetManually.Name = "btnSetManually";
            this.btnSetManually.Size = new System.Drawing.Size(34, 23);
            this.btnSetManually.TabIndex = 8;
            this.btnSetManually.Text = "Set Manually";
            this.btnSetManually.UseVisualStyleBackColor = true;
            this.btnSetManually.Click += new System.EventHandler(this.btnSetManually_Click_1);
            // 
            // btnAutoDetectClient
            // 
            this.btnAutoDetectClient.ForeColor = System.Drawing.Color.Black;
            this.btnAutoDetectClient.Location = new System.Drawing.Point(348, 30);
            this.btnAutoDetectClient.Name = "btnAutoDetectClient";
            this.btnAutoDetectClient.Size = new System.Drawing.Size(82, 23);
            this.btnAutoDetectClient.TabIndex = 7;
            this.btnAutoDetectClient.Text = "Auto Detect";
            this.btnAutoDetectClient.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Client Path:";
            // 
            // chkLaunchRazor
            // 
            this.chkLaunchRazor.AutoSize = true;
            this.chkLaunchRazor.ForeColor = System.Drawing.Color.Black;
            this.chkLaunchRazor.Location = new System.Drawing.Point(9, 58);
            this.chkLaunchRazor.Name = "chkLaunchRazor";
            this.chkLaunchRazor.Size = new System.Drawing.Size(314, 17);
            this.chkLaunchRazor.TabIndex = 5;
            this.chkLaunchRazor.Text = "Launch Razor with Client (will only launch if Razor is installed)";
            this.chkLaunchRazor.UseVisualStyleBackColor = true;
            // 
            // txtClientPath
            // 
            this.txtClientPath.ForeColor = System.Drawing.Color.Black;
            this.txtClientPath.Location = new System.Drawing.Point(9, 32);
            this.txtClientPath.Name = "txtClientPath";
            this.txtClientPath.ReadOnly = true;
            this.txtClientPath.Size = new System.Drawing.Size(293, 20);
            this.txtClientPath.TabIndex = 6;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(485, 235);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.optionsPanel.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkRemoveEncrpyption;
        private System.Windows.Forms.Button btnSetManually;
        private System.Windows.Forms.Button btnAutoDetectClient;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkLaunchRazor;
        private System.Windows.Forms.TextBox txtClientPath;
        private System.Windows.Forms.Panel optionsPanel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkMinimizeOnPlay;
        private System.Windows.Forms.Button btnDefaultPatchDirectory;
        private System.Windows.Forms.Button btnSetPatchDirectory;
        private System.Windows.Forms.TextBox txtPatchDirectory;
        private System.Windows.Forms.Label label8;
    }
}
