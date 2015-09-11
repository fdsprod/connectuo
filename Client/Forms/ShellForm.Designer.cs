namespace ConnectUO.Forms
{
    partial class ShellForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellForm));
            this.lblWebServiceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOnlineStatus = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkReverseSortBy = new System.Windows.Forms.CheckBox();
            this.cboSortBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contentActionPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.navigationPanel = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.notifyIconContextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWebServiceStatus
            // 
            this.lblWebServiceStatus.Name = "lblWebServiceStatus";
            this.lblWebServiceStatus.Size = new System.Drawing.Size(94, 17);
            this.lblWebServiceStatus.Text = "Connection: Idle";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyIconContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "ConnectUO 2.0";
            this.notifyIcon.DoubleClick += new System.EventHandler(this.OnNotifyIconDoubleClick);
            // 
            // notifyIconContextMenu
            // 
            this.notifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.notifyIconContextMenu.Name = "ctxNotifyIcon";
            this.notifyIconContextMenu.Size = new System.Drawing.Size(119, 48);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblOnlineStatus);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 646);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(958, 24);
            this.panel1.TabIndex = 32;
            // 
            // lblOnlineStatus
            // 
            this.lblOnlineStatus.AutoSize = true;
            this.lblOnlineStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblOnlineStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblOnlineStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblOnlineStatus.Location = new System.Drawing.Point(0, 0);
            this.lblOnlineStatus.Name = "lblOnlineStatus";
            this.lblOnlineStatus.Padding = new System.Windows.Forms.Padding(5, 4, 0, 0);
            this.lblOnlineStatus.Size = new System.Drawing.Size(69, 16);
            this.lblOnlineStatus.TabIndex = 14;
            this.lblOnlineStatus.Text = "Status: Offline";
            this.lblOnlineStatus.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStatus.Location = new System.Drawing.Point(958, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkReverseSortBy
            // 
            this.chkReverseSortBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkReverseSortBy.AutoSize = true;
            this.chkReverseSortBy.Location = new System.Drawing.Point(725, 9);
            this.chkReverseSortBy.Name = "chkReverseSortBy";
            this.chkReverseSortBy.Size = new System.Drawing.Size(66, 17);
            this.chkReverseSortBy.TabIndex = 42;
            this.chkReverseSortBy.Text = "Reverse";
            this.chkReverseSortBy.UseVisualStyleBackColor = true;
            this.chkReverseSortBy.CheckedChanged += new System.EventHandler(this.OnReverseSortByCheckedChanged);
            // 
            // cboSortBy
            // 
            this.cboSortBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSortBy.FormattingEnabled = true;
            this.cboSortBy.Location = new System.Drawing.Point(608, 6);
            this.cboSortBy.Name = "cboSortBy";
            this.cboSortBy.Size = new System.Drawing.Size(111, 21);
            this.cboSortBy.TabIndex = 41;
            this.cboSortBy.SelectedIndexChanged += new System.EventHandler(this.OnSortBySelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(558, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Sort By:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(333, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Filter:";
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilter.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFilter.Location = new System.Drawing.Point(371, 7);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(183, 20);
            this.txtFilter.TabIndex = 39;
            this.txtFilter.TextChanged += new System.EventHandler(this.OnFilterTextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.contentActionPanel);
            this.panel2.Controls.Add(this.chkReverseSortBy);
            this.panel2.Controls.Add(this.txtFilter);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cboSortBy);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(155, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(803, 36);
            this.panel2.TabIndex = 48;
            // 
            // contentActionPanel
            // 
            this.contentActionPanel.Location = new System.Drawing.Point(6, 9);
            this.contentActionPanel.Name = "contentActionPanel";
            this.contentActionPanel.Size = new System.Drawing.Size(326, 18);
            this.contentActionPanel.TabIndex = 43;
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 600000;
            this.updateTimer.Tick += new System.EventHandler(this.CheckForUpdates);
            // 
            // navigationPanel
            // 
            this.navigationPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.navigationPanel.Location = new System.Drawing.Point(0, 0);
            this.navigationPanel.Name = "navigationPanel";
            this.navigationPanel.Padding = new System.Windows.Forms.Padding(0, 28, 0, 0);
            this.navigationPanel.Size = new System.Drawing.Size(155, 646);
            this.navigationPanel.TabIndex = 53;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.BackgroundImage = global::ConnectUO.Properties.Resources.cuo_logo_small;
            this.contentPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(155, 36);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(803, 610);
            this.contentPanel.TabIndex = 52;
            // 
            // ShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(963, 670);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.navigationPanel);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(819, 600);
            this.Name = "ShellForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConnectUO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.SizeChanged += new System.EventHandler(this.OnMainSizeChanged);
            this.notifyIconContextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel lblWebServiceStatus;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblOnlineStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkReverseSortBy;
        private System.Windows.Forms.ComboBox cboSortBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ContextMenuStrip notifyIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.FlowLayoutPanel contentActionPanel;
        private System.Windows.Forms.Panel navigationPanel;
        private System.Windows.Forms.Panel contentPanel;



    }
}

