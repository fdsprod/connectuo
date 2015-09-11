namespace ConnectUO.Controls
{
    partial class ConnectionGroupControl
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
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.extendedFlowLayoutPanel1 = new ConnectUO.Controls.ExtendedFlowLayoutPanel();
            this.btnUpdateServerlists = new ConnectUO.Controls.ArrowButton();
            this.gbConnection.SuspendLayout();
            this.extendedFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbConnection
            // 
            this.gbConnection.Controls.Add(this.extendedFlowLayoutPanel1);
            this.gbConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbConnection.Location = new System.Drawing.Point(0, 0);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Size = new System.Drawing.Size(198, 41);
            this.gbConnection.TabIndex = 28;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "Connection";
            // 
            // extendedFlowLayoutPanel1
            // 
            this.extendedFlowLayoutPanel1.Controls.Add(this.btnUpdateServerlists);
            this.extendedFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extendedFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.extendedFlowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.extendedFlowLayoutPanel1.Name = "extendedFlowLayoutPanel1";
            this.extendedFlowLayoutPanel1.Size = new System.Drawing.Size(192, 22);
            this.extendedFlowLayoutPanel1.TabIndex = 0;
            this.extendedFlowLayoutPanel1.WrapContents = false;
            // 
            // btnUpdateServerlists
            // 
            this.btnUpdateServerlists.Location = new System.Drawing.Point(3, 3);
            this.btnUpdateServerlists.Name = "btnUpdateServerlists";
            this.btnUpdateServerlists.Selectable = false;
            this.btnUpdateServerlists.Selected = false;
            this.btnUpdateServerlists.Size = new System.Drawing.Size(95, 13);
            this.btnUpdateServerlists.TabIndex = 33;
            this.btnUpdateServerlists.Text = "Update Serverlist";
            // 
            // ConnectionGroupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbConnection);
            this.Name = "ConnectionGroupControl";
            this.Size = new System.Drawing.Size(198, 41);
            this.gbConnection.ResumeLayout(false);
            this.extendedFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbConnection;
        private ExtendedFlowLayoutPanel extendedFlowLayoutPanel1;
        private ArrowButton btnUpdateServerlists;
    }
}
