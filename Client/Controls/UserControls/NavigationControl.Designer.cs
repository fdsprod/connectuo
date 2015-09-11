namespace ConnectUO.Controls
{
    partial class NavigationControl
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
            this.helpAndSupportGroupControl = new ConnectUO.Controls.HelpAndSupportGroupControl();
            this.optionsGroupControl = new ConnectUO.Controls.OptionsGroupControl();
            this.serverListsGroupControl = new ConnectUO.Controls.ServerListsGroupControl();
            this.connectionGroupControl = new ConnectUO.Controls.ConnectionGroupControl();
            this.SuspendLayout();
            // 
            // helpAndSupportGroupControl
            // 
            this.helpAndSupportGroupControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.helpAndSupportGroupControl.Location = new System.Drawing.Point(5, 209);
            this.helpAndSupportGroupControl.Name = "helpAndSupportGroupControl";
            this.helpAndSupportGroupControl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.helpAndSupportGroupControl.Size = new System.Drawing.Size(151, 104);
            this.helpAndSupportGroupControl.TabIndex = 3;
            this.helpAndSupportGroupControl.ClientHelpClick += new System.EventHandler(this.helpAndSupportGroupControl_ClientHelpClick);
            this.helpAndSupportGroupControl.FAQClick += new System.EventHandler(this.helpAndSupportGroupControl_FAQClick);
            this.helpAndSupportGroupControl.ReportBugClick += new System.EventHandler(this.helpAndSupportGroupControl_ReportBugClick);
            this.helpAndSupportGroupControl.ServerHelpClick += new System.EventHandler(this.helpAndSupportGroupControl_ServerHelpClick);
            // 
            // optionsGroupControl
            // 
            this.optionsGroupControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.optionsGroupControl.Location = new System.Drawing.Point(5, 146);
            this.optionsGroupControl.Name = "optionsGroupControl";
            this.optionsGroupControl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.optionsGroupControl.Size = new System.Drawing.Size(151, 63);
            this.optionsGroupControl.TabIndex = 2;
            this.optionsGroupControl.SettingsClick += new System.EventHandler(this.optionsGroupControl_SettingsClick);
            this.optionsGroupControl.AboutClick += new System.EventHandler(this.optionsGroupControl_AboutClick);
            // 
            // serverListsGroupControl
            // 
            this.serverListsGroupControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.serverListsGroupControl.Location = new System.Drawing.Point(5, 45);
            this.serverListsGroupControl.Name = "serverListsGroupControl";
            this.serverListsGroupControl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.serverListsGroupControl.Size = new System.Drawing.Size(151, 101);
            this.serverListsGroupControl.TabIndex = 1;
            this.serverListsGroupControl.PublicServersClick += new System.EventHandler(this.serverListsGroupControl_PublicServersClick);
            this.serverListsGroupControl.FavoriteServersClick += new System.EventHandler(this.serverListsGroupControl_FavoriteServersClick);
            this.serverListsGroupControl.LocalServersClick += new System.EventHandler(this.serverListsGroupControl_LocalServersClick);
            // 
            // connectionGroupControl
            // 
            this.connectionGroupControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectionGroupControl.Location = new System.Drawing.Point(5, 5);
            this.connectionGroupControl.Name = "connectionGroupControl";
            this.connectionGroupControl.Size = new System.Drawing.Size(151, 40);
            this.connectionGroupControl.TabIndex = 0;
            this.connectionGroupControl.UpdateServersClick += new System.EventHandler(this.connectionGroupControl_UpdateServersClick);
            // 
            // NavigationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.helpAndSupportGroupControl);
            this.Controls.Add(this.optionsGroupControl);
            this.Controls.Add(this.serverListsGroupControl);
            this.Controls.Add(this.connectionGroupControl);
            this.Name = "NavigationControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(161, 320);
            this.ResumeLayout(false);

        }

        #endregion

        private ConnectionGroupControl connectionGroupControl;
        private ServerListsGroupControl serverListsGroupControl;
        private OptionsGroupControl optionsGroupControl;
        private HelpAndSupportGroupControl helpAndSupportGroupControl;
    }
}
