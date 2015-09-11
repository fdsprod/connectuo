namespace ConnectUO.Controls
{
    partial class OptionsGroupControl
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
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.extendedFlowLayoutPanel5 = new ConnectUO.Controls.ExtendedFlowLayoutPanel();
            this.btnSettings = new ConnectUO.Controls.ArrowButton();
            this.btnAbout = new ConnectUO.Controls.ArrowButton();
            this.gbOptions.SuspendLayout();
            this.extendedFlowLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.extendedFlowLayoutPanel5);
            this.gbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOptions.Location = new System.Drawing.Point(0, 0);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(150, 58);
            this.gbOptions.TabIndex = 30;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // extendedFlowLayoutPanel5
            // 
            this.extendedFlowLayoutPanel5.Controls.Add(this.btnSettings);
            this.extendedFlowLayoutPanel5.Controls.Add(this.btnAbout);
            this.extendedFlowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extendedFlowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.extendedFlowLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.extendedFlowLayoutPanel5.Name = "extendedFlowLayoutPanel5";
            this.extendedFlowLayoutPanel5.Size = new System.Drawing.Size(144, 39);
            this.extendedFlowLayoutPanel5.TabIndex = 4;
            this.extendedFlowLayoutPanel5.WrapContents = false;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(3, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Selectable = false;
            this.btnSettings.Selected = false;
            this.btnSettings.Size = new System.Drawing.Size(49, 13);
            this.btnSettings.TabIndex = 41;
            this.btnSettings.Text = "Settings";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(3, 22);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Selectable = false;
            this.btnAbout.Selected = false;
            this.btnAbout.Size = new System.Drawing.Size(38, 13);
            this.btnAbout.TabIndex = 42;
            this.btnAbout.Text = "About";
            // 
            // OptionsGroupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOptions);
            this.Name = "OptionsGroupControl";
            this.Size = new System.Drawing.Size(150, 58);
            this.gbOptions.ResumeLayout(false);
            this.extendedFlowLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private ExtendedFlowLayoutPanel extendedFlowLayoutPanel5;
        private ArrowButton btnSettings;
        private ArrowButton btnAbout;
    }
}
