namespace ConnectUO.Forms
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblWebServiceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOnlineStatus = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.extendedFlowLayoutPanel1 = new ConnectUO.Controls.ExtendedFlowLayoutPanel();
            this.btnUpdateServerlists = new ConnectUO.Controls.ArrowButton();
            this.gbServerLists = new System.Windows.Forms.GroupBox();
            this.extendedFlowLayoutPanel2 = new ConnectUO.Controls.ExtendedFlowLayoutPanel();
            this.btnPublicServers = new ConnectUO.Controls.ArrowButton();
            this.btnFavoriteServers = new ConnectUO.Controls.ArrowButton();
            this.btnLocalServer = new ConnectUO.Controls.ArrowButton();
            this.btnManageMyShards = new ConnectUO.Controls.ArrowButton();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.extendedFlowLayoutPanel5 = new ConnectUO.Controls.ExtendedFlowLayoutPanel();
            this.btnSettings = new ConnectUO.Controls.ArrowButton();
            this.btnAbout = new ConnectUO.Controls.ArrowButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.extendedFlowLayoutPanel3 = new ConnectUO.Controls.ExtendedFlowLayoutPanel();
            this.btnClientHelp = new ConnectUO.Controls.ArrowButton();
            this.btnServerHelp = new ConnectUO.Controls.ArrowButton();
            this.btnFAQ = new ConnectUO.Controls.ArrowButton();
            this.btnReportABug = new ConnectUO.Controls.ArrowButton();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.shardListControl = new ConnectUO.Controls.ShardListControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddLocalServer = new ConnectUO.Controls.ArrowButton();
            this.chkReverseSortBy = new System.Windows.Forms.CheckBox();
            this.cboSortBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNews = new System.Windows.Forms.RichTextBox();
            this.ctxNotifyIcon.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.gbConnection.SuspendLayout();
            this.extendedFlowLayoutPanel1.SuspendLayout();
            this.gbServerLists.SuspendLayout();
            this.extendedFlowLayoutPanel2.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.extendedFlowLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.extendedFlowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.notifyIcon.ContextMenuStrip = this.ctxNotifyIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "ConnectUO 2.0";
            this.notifyIcon.DoubleClick += new System.EventHandler(this.OnNotifyIconDoubleClick);
            // 
            // ctxNotifyIcon
            // 
            this.ctxNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.ctxNotifyIcon.Name = "ctxNotifyIcon";
            this.ctxNotifyIcon.Size = new System.Drawing.Size(119, 48);
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
            this.panel1.Location = new System.Drawing.Point(0, 635);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 35);
            this.panel1.TabIndex = 32;
            // 
            // lblOnlineStatus
            // 
            this.lblOnlineStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOnlineStatus.AutoSize = true;
            this.lblOnlineStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblOnlineStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblOnlineStatus.Location = new System.Drawing.Point(12, 11);
            this.lblOnlineStatus.Name = "lblOnlineStatus";
            this.lblOnlineStatus.Size = new System.Drawing.Size(64, 12);
            this.lblOnlineStatus.TabIndex = 14;
            this.lblOnlineStatus.Text = "Status: Offline";
            this.lblOnlineStatus.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStatus.Location = new System.Drawing.Point(189, 11);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(762, 12);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel.Controls.Add(this.gbConnection);
            this.flowLayoutPanel.Controls.Add(this.gbServerLists);
            this.flowLayoutPanel.Controls.Add(this.gbOptions);
            this.flowLayoutPanel.Controls.Add(this.groupBox1);
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 34);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.flowLayoutPanel.Size = new System.Drawing.Size(141, 601);
            this.flowLayoutPanel.TabIndex = 33;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // gbConnection
            // 
            this.gbConnection.Controls.Add(this.extendedFlowLayoutPanel1);
            this.gbConnection.Location = new System.Drawing.Point(7, 3);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Size = new System.Drawing.Size(131, 43);
            this.gbConnection.TabIndex = 27;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "Connection";
            // 
            // extendedFlowLayoutPanel1
            // 
            this.extendedFlowLayoutPanel1.Controls.Add(this.btnUpdateServerlists);
            this.extendedFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extendedFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.extendedFlowLayoutPanel1.Loaded = false;
            this.extendedFlowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.extendedFlowLayoutPanel1.Name = "extendedFlowLayoutPanel1";
            this.extendedFlowLayoutPanel1.Size = new System.Drawing.Size(125, 24);
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
            this.btnUpdateServerlists.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnUpdateServerlists_MouseClick);
            // 
            // gbServerLists
            // 
            this.gbServerLists.Controls.Add(this.extendedFlowLayoutPanel2);
            this.gbServerLists.Location = new System.Drawing.Point(7, 52);
            this.gbServerLists.Name = "gbServerLists";
            this.gbServerLists.Size = new System.Drawing.Size(131, 101);
            this.gbServerLists.TabIndex = 28;
            this.gbServerLists.TabStop = false;
            this.gbServerLists.Text = "Server Lists";
            // 
            // extendedFlowLayoutPanel2
            // 
            this.extendedFlowLayoutPanel2.Controls.Add(this.btnPublicServers);
            this.extendedFlowLayoutPanel2.Controls.Add(this.btnFavoriteServers);
            this.extendedFlowLayoutPanel2.Controls.Add(this.btnLocalServer);
            this.extendedFlowLayoutPanel2.Controls.Add(this.btnManageMyShards);
            this.extendedFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extendedFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.extendedFlowLayoutPanel2.Loaded = false;
            this.extendedFlowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.extendedFlowLayoutPanel2.Name = "extendedFlowLayoutPanel2";
            this.extendedFlowLayoutPanel2.Size = new System.Drawing.Size(125, 82);
            this.extendedFlowLayoutPanel2.TabIndex = 1;
            this.extendedFlowLayoutPanel2.WrapContents = false;
            // 
            // btnPublicServers
            // 
            this.btnPublicServers.Location = new System.Drawing.Point(3, 3);
            this.btnPublicServers.Name = "btnPublicServers";
            this.btnPublicServers.Selectable = true;
            this.btnPublicServers.Selected = false;
            this.btnPublicServers.Size = new System.Drawing.Size(81, 13);
            this.btnPublicServers.TabIndex = 33;
            this.btnPublicServers.Text = "Public Servers";
            this.btnPublicServers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPublicServers_MouseClick);
            // 
            // btnFavoriteServers
            // 
            this.btnFavoriteServers.Location = new System.Drawing.Point(3, 22);
            this.btnFavoriteServers.Name = "btnFavoriteServers";
            this.btnFavoriteServers.Selectable = true;
            this.btnFavoriteServers.Selected = false;
            this.btnFavoriteServers.Size = new System.Drawing.Size(91, 13);
            this.btnFavoriteServers.TabIndex = 34;
            this.btnFavoriteServers.Text = "Favorite Servers";
            this.btnFavoriteServers.Load += new System.EventHandler(this.btnFavoriteServers_Load);
            this.btnFavoriteServers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnFavoriteServers_MouseClick);
            // 
            // btnLocalServer
            // 
            this.btnLocalServer.Location = new System.Drawing.Point(3, 41);
            this.btnLocalServer.Name = "btnLocalServer";
            this.btnLocalServer.Selectable = true;
            this.btnLocalServer.Selected = false;
            this.btnLocalServer.Size = new System.Drawing.Size(77, 13);
            this.btnLocalServer.TabIndex = 35;
            this.btnLocalServer.Text = "Local Servers";
            this.btnLocalServer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnLocalServer_MouseClick);
            // 
            // btnManageMyShards
            // 
            this.btnManageMyShards.Location = new System.Drawing.Point(3, 60);
            this.btnManageMyShards.Name = "btnManageMyShards";
            this.btnManageMyShards.Selectable = false;
            this.btnManageMyShards.Selected = false;
            this.btnManageMyShards.Size = new System.Drawing.Size(106, 13);
            this.btnManageMyShards.TabIndex = 45;
            this.btnManageMyShards.Text = "Manage My Shards";
            this.btnManageMyShards.Click += new System.EventHandler(this.btnManageMyShards_Click);
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.extendedFlowLayoutPanel5);
            this.gbOptions.Location = new System.Drawing.Point(7, 159);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(131, 61);
            this.gbOptions.TabIndex = 29;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // extendedFlowLayoutPanel5
            // 
            this.extendedFlowLayoutPanel5.Controls.Add(this.btnSettings);
            this.extendedFlowLayoutPanel5.Controls.Add(this.btnAbout);
            this.extendedFlowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extendedFlowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.extendedFlowLayoutPanel5.Loaded = false;
            this.extendedFlowLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.extendedFlowLayoutPanel5.Name = "extendedFlowLayoutPanel5";
            this.extendedFlowLayoutPanel5.Size = new System.Drawing.Size(125, 42);
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
            this.btnSettings.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnSettings_MouseClick);
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
            this.btnAbout.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnAbout_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.extendedFlowLayoutPanel3);
            this.groupBox1.Location = new System.Drawing.Point(7, 226);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 99);
            this.groupBox1.TabIndex = 30;
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
            this.extendedFlowLayoutPanel3.Loaded = false;
            this.extendedFlowLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.extendedFlowLayoutPanel3.Name = "extendedFlowLayoutPanel3";
            this.extendedFlowLayoutPanel3.Size = new System.Drawing.Size(125, 80);
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
            this.btnClientHelp.Click += new System.EventHandler(this.btnClientHelp_Click);
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
            this.btnServerHelp.Click += new System.EventHandler(this.btnServerHelp_Click);
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
            this.btnFAQ.Click += new System.EventHandler(this.btnFAQ_Click);
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
            this.btnReportABug.Click += new System.EventHandler(this.btnReportABug_Click);
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbLogo.BackgroundImage = global::ConnectUO.Properties.Resources.cuo_logo_small;
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLogo.Location = new System.Drawing.Point(339, 254);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(442, 95);
            this.pbLogo.TabIndex = 36;
            this.pbLogo.TabStop = false;
            this.pbLogo.Visible = false;
            // 
            // shardListControl
            // 
            this.shardListControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shardListControl.BackColor = System.Drawing.Color.Transparent;
            this.shardListControl.Location = new System.Drawing.Point(148, 34);
            this.shardListControl.Name = "shardListControl";
            this.shardListControl.ScrollPosition = 0;
            this.shardListControl.SelectedItem = null;
            this.shardListControl.Size = new System.Drawing.Size(807, 539);
            this.shardListControl.TabIndex = 35;
            this.shardListControl.ButtonClicked += new System.EventHandler<ConnectUO.Controls.ShardListItemButtonClickedEventArgs>(this.OnShardListControlButtonClicked);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnAddLocalServer);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(147, 8);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(188, 20);
            this.flowLayoutPanel1.TabIndex = 43;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btnAddLocalServer
            // 
            this.btnAddLocalServer.Location = new System.Drawing.Point(3, 3);
            this.btnAddLocalServer.Name = "btnAddLocalServer";
            this.btnAddLocalServer.Selectable = true;
            this.btnAddLocalServer.Selected = false;
            this.btnAddLocalServer.Size = new System.Drawing.Size(95, 13);
            this.btnAddLocalServer.TabIndex = 1;
            this.btnAddLocalServer.Text = "Add Local Server";
            this.btnAddLocalServer.Visible = false;
            this.btnAddLocalServer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnAddLocalServer_MouseClick);
            // 
            // chkReverseSortBy
            // 
            this.chkReverseSortBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkReverseSortBy.AutoSize = true;
            this.chkReverseSortBy.Location = new System.Drawing.Point(893, 10);
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
            this.cboSortBy.Location = new System.Drawing.Point(776, 7);
            this.cboSortBy.Name = "cboSortBy";
            this.cboSortBy.Size = new System.Drawing.Size(111, 21);
            this.cboSortBy.TabIndex = 41;
            this.cboSortBy.SelectedIndexChanged += new System.EventHandler(this.OnSortBySelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(726, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Sort By:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(501, 12);
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
            this.txtFilter.Location = new System.Drawing.Point(539, 8);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(183, 20);
            this.txtFilter.TabIndex = 39;
            this.txtFilter.TextChanged += new System.EventHandler(this.OnFilterTextChanged);
            // 
            // editLocalShardControl1
            // 
            this.editLocalShardControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editLocalShardControl1.Location = new System.Drawing.Point(148, 34);
            this.editLocalShardControl1.Name = "editLocalShardControl1";
            this.editLocalShardControl1.Server = null;
            this.editLocalShardControl1.Size = new System.Drawing.Size(807, 539);
            this.editLocalShardControl1.TabIndex = 44;
            this.editLocalShardControl1.Visible = false;
            this.editLocalShardControl1.ShardEditAddComplete += new System.EventHandler<System.EventArgs>(this.OnShardEditAddComplete);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtNews);
            this.groupBox2.Location = new System.Drawing.Point(144, 575);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(811, 59);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            // 
            // txtNews
            // 
            this.txtNews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNews.BackColor = System.Drawing.Color.White;
            this.txtNews.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNews.ForeColor = System.Drawing.Color.Gray;
            this.txtNews.Location = new System.Drawing.Point(7, 8);
            this.txtNews.Name = "txtNews";
            this.txtNews.ReadOnly = true;
            this.txtNews.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtNews.Size = new System.Drawing.Size(800, 49);
            this.txtNews.TabIndex = 0;
            this.txtNews.Text = "Retrieving News and Announcements...";
            this.txtNews.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtNews_LinkClicked);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(963, 670);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cboSortBy);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.editLocalShardControl1);
            this.Controls.Add(this.chkReverseSortBy);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.shardListControl);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(819, 600);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConnectUO";
            this.SizeChanged += new System.EventHandler(this.OnMainSizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.ctxNotifyIcon.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel.ResumeLayout(false);
            this.gbConnection.ResumeLayout(false);
            this.extendedFlowLayoutPanel1.ResumeLayout(false);
            this.gbServerLists.ResumeLayout(false);
            this.extendedFlowLayoutPanel2.ResumeLayout(false);
            this.gbOptions.ResumeLayout(false);
            this.extendedFlowLayoutPanel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.extendedFlowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel lblWebServiceStatus;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblOnlineStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.GroupBox gbConnection;
        private ConnectUO.Controls.ExtendedFlowLayoutPanel extendedFlowLayoutPanel1;
        private ConnectUO.Controls.ArrowButton btnUpdateServerlists;
        private System.Windows.Forms.GroupBox gbServerLists;
        private ConnectUO.Controls.ExtendedFlowLayoutPanel extendedFlowLayoutPanel2;
        private ConnectUO.Controls.ArrowButton btnPublicServers;
        private ConnectUO.Controls.ArrowButton btnFavoriteServers;
        private ConnectUO.Controls.ArrowButton btnLocalServer;
        private System.Windows.Forms.GroupBox gbOptions;
        private ConnectUO.Controls.ExtendedFlowLayoutPanel extendedFlowLayoutPanel5;
        private ConnectUO.Controls.ArrowButton btnSettings;
        private System.Windows.Forms.PictureBox pbLogo;
        private ConnectUO.Controls.ShardListControl shardListControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ConnectUO.Controls.ArrowButton btnAddLocalServer;
        private System.Windows.Forms.CheckBox chkReverseSortBy;
        private System.Windows.Forms.ComboBox cboSortBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilter;
        private ConnectUO.Controls.ArrowButton btnAbout;
        private ConnectUO.Controls.EditLocalServerControl editLocalShardControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private ConnectUO.Controls.ExtendedFlowLayoutPanel extendedFlowLayoutPanel3;
        private ConnectUO.Controls.ArrowButton btnServerHelp;
        private ConnectUO.Controls.ArrowButton btnFAQ;
        private ConnectUO.Controls.ArrowButton btnReportABug;
        private ConnectUO.Controls.ArrowButton btnClientHelp;
        private ConnectUO.Controls.ArrowButton btnManageMyShards;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtNews;
        private System.Windows.Forms.ContextMenuStrip ctxNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;



    }
}

