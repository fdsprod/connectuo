namespace ConnectUO.Controls
{
    partial class ServerListsGroupControl
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
            this.gbServerLists = new System.Windows.Forms.GroupBox();
            this.extendedFlowLayoutPanel2 = new ConnectUO.Controls.ExtendedFlowLayoutPanel();
            this.btnPublicServers = new ConnectUO.Controls.ArrowButton();
            this.btnFavoriteServers = new ConnectUO.Controls.ArrowButton();
            this.btnLocalServer = new ConnectUO.Controls.ArrowButton();
            this.btnManageMyShards = new ConnectUO.Controls.ArrowButton();
            this.gbServerLists.SuspendLayout();
            this.extendedFlowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbServerLists
            // 
            this.gbServerLists.Controls.Add(this.extendedFlowLayoutPanel2);
            this.gbServerLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbServerLists.Location = new System.Drawing.Point(0, 0);
            this.gbServerLists.Name = "gbServerLists";
            this.gbServerLists.Size = new System.Drawing.Size(150, 98);
            this.gbServerLists.TabIndex = 29;
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
            this.extendedFlowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.extendedFlowLayoutPanel2.Name = "extendedFlowLayoutPanel2";
            this.extendedFlowLayoutPanel2.Size = new System.Drawing.Size(144, 79);
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
            // 
            // ServerListsGroupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbServerLists);
            this.Name = "ServerListsGroupControl";
            this.Size = new System.Drawing.Size(150, 98);
            this.gbServerLists.ResumeLayout(false);
            this.extendedFlowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbServerLists;
        private ExtendedFlowLayoutPanel extendedFlowLayoutPanel2;
        private ArrowButton btnPublicServers;
        private ArrowButton btnFavoriteServers;
        private ArrowButton btnLocalServer;
        private ArrowButton btnManageMyShards;
    }
}
