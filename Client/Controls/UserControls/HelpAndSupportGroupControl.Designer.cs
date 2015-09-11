namespace ConnectUO.Controls
{
    partial class HelpAndSupportGroupControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.extendedFlowLayoutPanel3 = new ConnectUO.Controls.ExtendedFlowLayoutPanel();
            this.btnClientHelp = new ConnectUO.Controls.ArrowButton();
            this.btnServerHelp = new ConnectUO.Controls.ArrowButton();
            this.btnFAQ = new ConnectUO.Controls.ArrowButton();
            this.btnReportABug = new ConnectUO.Controls.ArrowButton();
            this.groupBox1.SuspendLayout();
            this.extendedFlowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.extendedFlowLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 98);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Help and Support";
            // 
            // extendedFlowLayoutPanel3
            // 
            this.extendedFlowLayoutPanel3.Controls.Add(this.btnClientHelp);
            this.extendedFlowLayoutPanel3.Controls.Add(this.btnServerHelp);
            this.extendedFlowLayoutPanel3.Controls.Add(this.btnFAQ);
            this.extendedFlowLayoutPanel3.Controls.Add(this.btnReportABug);
            this.extendedFlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extendedFlowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.extendedFlowLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.extendedFlowLayoutPanel3.Name = "extendedFlowLayoutPanel3";
            this.extendedFlowLayoutPanel3.Size = new System.Drawing.Size(144, 79);
            this.extendedFlowLayoutPanel3.TabIndex = 4;
            this.extendedFlowLayoutPanel3.WrapContents = false;
            // 
            // btnClientHelp
            // 
            this.btnClientHelp.Location = new System.Drawing.Point(3, 3);
            this.btnClientHelp.Name = "btnClientHelp";
            this.btnClientHelp.Selectable = false;
            this.btnClientHelp.Selected = false;
            this.btnClientHelp.Size = new System.Drawing.Size(64, 13);
            this.btnClientHelp.TabIndex = 44;
            this.btnClientHelp.Text = "Client Help";
            // 
            // btnServerHelp
            // 
            this.btnServerHelp.Location = new System.Drawing.Point(3, 22);
            this.btnServerHelp.Name = "btnServerHelp";
            this.btnServerHelp.Selectable = false;
            this.btnServerHelp.Selected = false;
            this.btnServerHelp.Size = new System.Drawing.Size(68, 13);
            this.btnServerHelp.TabIndex = 41;
            this.btnServerHelp.Text = "Server Help";
            // 
            // btnFAQ
            // 
            this.btnFAQ.Location = new System.Drawing.Point(3, 41);
            this.btnFAQ.Name = "btnFAQ";
            this.btnFAQ.Selectable = false;
            this.btnFAQ.Selected = false;
            this.btnFAQ.Size = new System.Drawing.Size(31, 13);
            this.btnFAQ.TabIndex = 42;
            this.btnFAQ.Text = "FAQ";
            // 
            // btnReportABug
            // 
            this.btnReportABug.Location = new System.Drawing.Point(3, 60);
            this.btnReportABug.Name = "btnReportABug";
            this.btnReportABug.Selectable = false;
            this.btnReportABug.Selected = false;
            this.btnReportABug.Size = new System.Drawing.Size(73, 13);
            this.btnReportABug.TabIndex = 43;
            this.btnReportABug.Text = "Report a bug";
            // 
            // HelpAndSupportGroupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "HelpAndSupportGroupControl";
            this.Size = new System.Drawing.Size(150, 98);
            this.groupBox1.ResumeLayout(false);
            this.extendedFlowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ExtendedFlowLayoutPanel extendedFlowLayoutPanel3;
        private ArrowButton btnClientHelp;
        private ArrowButton btnServerHelp;
        private ArrowButton btnFAQ;
        private ArrowButton btnReportABug;
    }
}
