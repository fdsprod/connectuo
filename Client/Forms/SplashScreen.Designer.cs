namespace ConnectUO.Forms
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.donationButton1 = new ConnectUO.Controls.DonationButton();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.ForeColor = System.Drawing.Color.DarkGray;
            this.lblVersion.Location = new System.Drawing.Point(40, 144);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(48, 13);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "Version: ";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGray;
            this.lblStatus.Location = new System.Drawing.Point(12, 228);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 13;
            // 
            // donationButton1
            // 
            this.donationButton1.BackColor = System.Drawing.Color.White;
            this.donationButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("donationButton1.BackgroundImage")));
            this.donationButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.donationButton1.ForeColor = System.Drawing.Color.White;
            this.donationButton1.Location = new System.Drawing.Point(294, 227);
            this.donationButton1.Name = "donationButton1";
            this.donationButton1.Size = new System.Drawing.Size(74, 14);
            this.donationButton1.TabIndex = 10;
            this.donationButton1.UseVisualStyleBackColor = false;
            // 
            // frmSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ConnectUO.Properties.Resources.cuo_splash_screen;
            this.ClientSize = new System.Drawing.Size(380, 250);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.donationButton1);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSplashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplashScreen";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSplashScreen_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSplashScreen_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private ConnectUO.Controls.DonationButton donationButton1;
        private System.Windows.Forms.Label lblStatus;
    }
}