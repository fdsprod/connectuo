using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConnectUO.Data;
using ConnectUO.Framework;
using ConnectUO.Framework.Web;
using Fds.Core;
using Fds.Core.Diagnostics;
using System.Threading;
using ConnectUO.Framework.Data;
using Fds.Core.Windows.Forms;
using System.Xml;
using System.Net;
using Fds.ComponentModel.Services;
using ConnectUO.Framework.Services;
using System.ComponentModel;
using Ninject;
using ConnectUO.Controls;

namespace ConnectUO.Forms
{
    public enum ServerListViewState
    {
        [Description("Public Servers")]
        PublicServers,
        [Description("Favorites")]
        FavoritesServers,
        [Description("Local Servers")]
        LocalServers,
        [Description("None")]
        None
    }
    
    public sealed partial class frmMain : frmSkinnable
    {
        private System.Windows.Forms.Timer _rssTimer;
        private System.Windows.Forms.Timer _updateTimer;
        private ServerListViewState _viewState;
        private ServerComparer _comparer = new ServerComparer();
        private bool _pendingSave = false;
        private int[] _lastScrollPositions = new int[3];

        private IStorageService _storageService;
        private IApplicationService _applicationService;
        private ISettingsService _settingsService;
        private IKernel _kernel;

        public ServerListViewState ViewState
        {
            get { return _viewState; }
            set 
            {
                if (_viewState != value)
                {
                    _lastScrollPositions[(int)_viewState] = shardListControl.ScrollPosition;
                    _viewState = value;

                    OnServerListChanged(this, EventArgs.Empty);
                }
            }
        }
        
        public frmMain(IKernel kernel)
        {
            _kernel = kernel;
            _applicationService = _kernel.Get<IApplicationService>();
            _storageService = _kernel.Get<IStorageService>();
            _settingsService = _kernel.Get<ISettingsService>();

            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RefreshServerView();
            BringToFront();

            btnPublicServers.Selected = true;

            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 600000;
            _updateTimer.Enabled = true;
            _updateTimer.Tick += new EventHandler(CheckForUpdates);
            _updateTimer.Start();

            _rssTimer = new System.Windows.Forms.Timer();
            _rssTimer.Interval = 20000;
            _rssTimer.Enabled = true;
            _rssTimer.Tick += new EventHandler(OnRssTimerTick);
            _rssTimer.Start();

            string[] sortByStrings = Enum.GetNames(typeof(ServerListOrderBy));

            for (int i = 0; i < sortByStrings.Length; i++)
            {
                string itemName = "Default";

                if (i > 0)
                {
                    itemName = Utility.ProperSpace(sortByStrings[i]);
                }

                cboSortBy.Items.Add(itemName);
            }

            cboSortBy.SelectedIndex = 0;

            _storageService.Error += new EventHandler<ConnectUO.Framework.ErrorEventArgs>(Database_Error);
            _storageService.ServersUpdateBegin += new EventHandler<EventArgs>(Database_ServersUpdateBegin);
            _storageService.ServersUpdateComplete += new EventHandler<EventArgs>(Database_ServersUpdateComplete);
            
            ThreadPool.QueueUserWorkItem(delegate(object o)
            {
                _storageService.UpdateServers(new CallbackState(null));
                OnRssTimerTick(o, EventArgs.Empty);
            });
        }

        private void OnRssTimerTick(object sender, EventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate()
                {
                    txtNews.Clear();
                });

                WebClient client = new WebClient();

                XmlDocument rssDocument = new XmlDocument();
                XmlDocument itemDocument = new XmlDocument();

                string xml = client.DownloadString("http://www.connectuo.com/forum/external.php?forumids=7");

                rssDocument.LoadXml(xml);
                XmlNodeList rssItems = rssDocument.SelectNodes("//rss/channel/item");

                const int max = 3;

                for (int i = 0; i < rssItems.Count && i < max; i++)
                {
                    xml = string.Format("<root>{0}</root>", rssItems[i].InnerXml);

                    itemDocument.LoadXml(xml);

                    string title = itemDocument.SelectSingleNode("//root/title").InnerText;
                    string link = itemDocument.SelectSingleNode("//root/link").InnerText.Replace("&goto=newpost", "");
                    string pubDate = itemDocument.SelectSingleNode("//root/pubDate").InnerText;
                    string description = itemDocument.SelectSingleNode("//root/description").InnerText;

                    string newsItem = string.Format("{0} - {1}{2}{3}{2}{4}{2}{2}", pubDate, title, Environment.NewLine, description, link);

                    Invoke((MethodInvoker)delegate()
                    {
                        txtNews.Text += newsItem;
                    });
                }
            }
            catch (Exception ex)
            {
                Tracer.Error(ex);

                Invoke((MethodInvoker)delegate()
                {
                    txtNews.Text = "An error occured while retrieving the news, please check the debug.log for details.";
                });
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            shardListControl.Invalidate();
        }

        private void Database_ServersUpdateComplete(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate()
            {
                _lastScrollPositions[(int)_viewState] = shardListControl.ScrollPosition; 
                RefreshServerView(); 
                lblStatus.Text = "";
            });
        }

        private void Database_ServersUpdateBegin(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate()
            {
                _lastScrollPositions[(int)_viewState] = shardListControl.ScrollPosition; 
                lblStatus.Text = "Updating Servers...";
            });
        }

        private void Database_Error(object sender, ConnectUO.Framework.ErrorEventArgs e)
        {
            Invoke((MethodInvoker)delegate() { lblStatus.Text = string.Format("Error: {0}", e.Exception.Message); });
        }

        private void OnShardEditAddComplete(object sender, EventArgs e)
        {
            if (_pendingSave)
            {
                _pendingSave = false;

                bool addToDatabase = (from s in _storageService.LocalServers where s.Id == editLocalShardControl1.Server.Id select s).FirstOrDefault() == null;

                if (addToDatabase)
                {
                    _storageService.AddServer(editLocalShardControl1.Server);
                }

                if (editLocalShardControl1.Server.EntityState != System.Data.EntityState.Unchanged)
                {
                    _storageService.SaveChanges();
                }
            }

            editLocalShardControl1.Visible = false;
            RefreshServerView();
        }

        private void OnInvalidateRequired(object sender, EventArgs e)
        {
            shardListControl.Invalidate();
        }
        
        private void CheckForUpdates(object sender, EventArgs e)
        {
            _storageService.UpdateServers(null);
        }

        private void SetStatus(string status)
        {
            lblStatus.Text = status;
        }

        private void SetFlowLayoutLoaded(Control control, bool loaded)
        {
            if (control is ExtendedFlowLayoutPanel)
                ((ExtendedFlowLayoutPanel)control).Loaded = loaded;

            for (int i = 0; i < control.Controls.Count; i++)
            {
                SetFlowLayoutLoaded(control.Controls[i], true);
            }
        }

        private void RefreshServerView()
        {
            int scrollPos = _lastScrollPositions[(int)_viewState];

            Server selected = shardListControl.SelectedItem == null ? null : shardListControl.SelectedItem.Server;

            shardListControl.Items.Clear();
            
            Server[] servers = null;

            switch (_viewState)
            {
                case ServerListViewState.PublicServers:
                    btnPublicServers.Select();
                    servers = _storageService.PublicServers;
                    Text = string.Format("ConnectUO - {0} - Public Servers", _applicationService.FormattedVersionString);
                    break;
                case ServerListViewState.FavoritesServers:
                    btnFavoriteServers.Select();
                    servers = _storageService.FavoriteServers;
                    Text = string.Format("ConnectUO - {0} - Favorites", _applicationService.FormattedVersionString);
                    break;
                default:
                    btnLocalServer.Select();
                    servers = _storageService.LocalServers;
                    Text = string.Format("ConnectUO - {0} - Local Servers", _applicationService.FormattedVersionString);
                    break;
            }

            _comparer.OrderBy = (ServerListOrderBy)cboSortBy.SelectedIndex;
            _comparer.Reverse = chkReverseSortBy.Checked;

            Array.Sort(servers, _comparer);

            for (int i = 0; i < servers.Length; i++)
            {
                Server shard = servers[i];

                if (string.IsNullOrEmpty(txtFilter.Text) ||
                    (shard.Name.ToLower().Contains(txtFilter.Text.ToLower()) || shard.Description.ToLower().Contains(txtFilter.Text.ToLower())))
                {
                    ShardListItem item = new ShardListItem(shard);

                    item.Buttons.Add(new PlayShardListItemButton(item));

                    if (!shard.Favorite && shard.Public)
                    {
                        item.Buttons.Add(new AddToFavoritesShardListItemButton(item));
                        //item.Buttons.Add(new HideListItemButton(item));
                    }

                    if (shard.Favorite && _viewState == ServerListViewState.FavoritesServers)
                    {
                        item.Buttons.Add(new RemoveFavoriteShardListItemButton(item));
                    }

                    if (!shard.Public)
                    {
                        item.Buttons.Add(new EditLocalShardListItemButton(item));
                        item.Buttons.Add(new RemoveCustomShardListItemButton(item));
                    }
                    else
                    {
                        item.Buttons.Add(new WebsiteListItemButton(item));
                    }

                    if (shard.HasPatches)
                    {
                        item.Buttons.Add(new ResetPatchesListItemButton(item));
                    }

                    shardListControl.Items.Add(item);
                }
            }

            shardListControl.ScrollPosition = scrollPos;

            if (selected != null && selected is Server)
            {
                Server selectedServer = (Server)selected;

                for (int i = 0; i < shardListControl.Items.Count; i++)
                {
                    Server shard = (Server)shardListControl.Items[i].Server;

                    if (shard.Name == selectedServer.Name)
                    {
                        shardListControl.SelectedItem = shardListControl.Items[i];
                        break;
                    }
                }
            }

            pbLogo.Visible = shardListControl.Items.Count == 0;
            shardListControl.Invalidate();
        }

        private void OnFilterTextChanged(object sender, EventArgs e)
        {
            RefreshServerView();
            txtFilter.Focus();
        }

        private void OnServerListChanged(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            shardListControl.ScrollPosition = _lastScrollPositions[(int)_viewState];
            btnAddLocalServer.Visible = (_viewState == ServerListViewState.LocalServers);
            RefreshServerView();
        }
        
        private void OnServerUpdateComplete(object sender, EventArgs e)
        {
            RefreshServerView();
        }

        private void OnShardListControlButtonClicked(object sender, ShardListItemButtonClickedEventArgs e)
        {
            if (e.Button is AddToFavoritesShardListItemButton)
            {
                e.Item.Server.Favorite = true;
                _storageService.SaveChanges();

                ViewState = ServerListViewState.FavoritesServers;
            }
            else if (e.Button is PlayShardListItemButton)
            {
                if (e.Item.Server == null)
                {
                    throw new ArgumentNullException("e.Item.Server");
                }

                Tracer.Verbose("Preparing to play {0}...", e.Item.Server.Name);
                lblStatus.Text = string.Format("Preparing to play {0}...", e.Item.Server.Name);

                string uri = CuoUri.BuildPlayString(e.Item.Server);

                if (e.Item.Server.HasPatches)
                {
                    Tracer.Verbose("Server has patches, checking...");

                    if (e.Item.Server.Public)
                    {
                        Tracer.Verbose("Retrieving patching information for {0}...", e.Item.Server.Name);
                        lblStatus.Text = string.Format("Retrieving patching information for {0}...", e.Item.Server.Name);

                        try
                        {
                            ServerPatch[] patches = _storageService.GetPatches((int)e.Item.Server.Id);

                            Tracer.Verbose("Found {0} patches...", patches.Length);

                            StringBuilder sb = new StringBuilder();

                            for (int i = 0; i < patches.Length; i++)
                            {
                                sb.AppendFormat("{0}|{1}{2}", patches[i].PatchUrl, patches[i].Version, i + 1 < patches.Length ? ";" : "");
                            }

                            uri = string.Join(string.Format("&{0}=", CuoUri.PatchesTolken), new string[] { uri, sb.ToString() });
                        }
                        catch (Exception ex)
                        {
                            Tracer.Error(ex);
                            MessageBoxEx.Show(this, "Unable to get patch information for this server.  See the debug log for details.", "ConnectUO 2.0");
                        }
                    }
                    else
                    {
                        try
                        {
                            LocalPatch[] patches = _storageService.GetLocalPatches((int)e.Item.Server.Id);

                            Tracer.Verbose("Found {0} patches...", patches.Length);

                            StringBuilder sb = new StringBuilder();

                            for (int i = 0; i < patches.Length; i++)
                            {
                                sb.AppendFormat("{0}|{1}{2}", patches[i].PatchUrl, patches[i].Version, i + 1 < patches.Length ? ";" : "");
                            }

                            uri = string.Join(string.Format("&{0}=", CuoUri.PatchesTolken), new string[] { uri, sb.ToString() });
                        }
                        catch (Exception ex)
                        {
                            Tracer.Error(ex);
                            MessageBoxEx.Show(this, "Unable to get patch information for this server.  See the debug log for details.", "ConnectUO 2.0");
                        }
                    }
                }

                Tracer.Verbose("Play URI: {0}", uri);
                uri = string.Format("cuo://{0}", Uri.EscapeDataString(Convert.ToBase64String(Encoding.UTF8.GetBytes(uri))));
                
                _kernel.Get<CuoUri>().Play(uri);

                lblStatus.Text = "";
            }
            else if (e.Button is RemoveFavoriteShardListItemButton)
            {
                e.Item.Server.Favorite = false;
                _storageService.SaveChanges();
                RefreshServerView();
            }
            //else if (e.Button is HideListItemButton)
            //{
            //    //_storageService.HideServer(e.Item.Server);
            //    RefreshServerView();
            //}
            else if (e.Button is RemoveCustomShardListItemButton)
            {
                if (MessageBoxEx.Show(this, "Are you sure you want to remove this server?",
                    "ConnectUO 2.0", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    _storageService.DeleteServer(e.Item.Server);
                    RefreshServerView();
                }
            }
            else if (e.Button is EditLocalShardListItemButton)
            {
                editLocalShardControl1.Server = e.Item.Server;
                editLocalShardControl1.Visible = true;
            }
            else if (e.Button is ResetPatchesListItemButton)
            {
                if (MessageBoxEx.Show(this, "This will remove all applied patches for this server from your computer.  Are you sure you want to continue?",
                    "ConnectUO 2.0", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    if (_storageService.ServerIsCurrentlyBeingPlayed(e.Item.Server))
                    {
                        MessageBoxEx.Show(this,
                            "ConnectUO has detected that you are currently playing this server and cannot reset the patches until the Ultima Online client connected bas been closed.", "ConnectUO 2.0");
                        return;
                    }
                    else
                    {
                        _storageService.ResetPatches((int)e.Item.Server.Id);

                        try
                        {
                            string serverDirectory = Path.Combine(_settingsService.PatchDirectory, Uri.EscapeDataString(e.Item.Server.Name));

                            if (Directory.Exists(serverDirectory))
                            {
                                FileInfo[] files = new DirectoryInfo(serverDirectory).GetFiles();

                                for (int i = 0; i < files.Length; i++)
                                {
                                    try
                                    {
                                        files[i].Delete();
                                    }
                                    catch (Exception ex)
                                    {
                                        //These aren't crucial, thus i just log as info...
                                        Tracer.Info(ex);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //These aren't crucial, thus i just log as info...
                            Tracer.Info(ex);
                        }
                    }

                    MessageBoxEx.Show(this, "Patch Reset Complete.", "ConnectUO 2.0");
                }
            }
            else if (e.Button is WebsiteListItemButton)
            {
                Process.Start(e.Item.Server.Url);
            }
            else
            {
                MessageBoxEx.Show(this, String.Format("No click statement for {0}", e.Button.GetType()), "ConnectUO 2.0");
            }

            shardListControl.Invalidate();
        }

        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            shardListControl.Invalidate();
            BringToFront();
        }

        private void OnSortBySelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshServerView();
            cboSortBy.Focus();
        }

        private void OnReverseSortByCheckedChanged(object sender, EventArgs e)
        {
            RefreshServerView();
        }

        private void OnMainSizeChanged(object sender, EventArgs e)
        {
            ShowInTaskbar = !(notifyIcon.Visible = (WindowState == FormWindowState.Minimized));

            Point location = shardListControl.Location;
        }

        private void btnUpdateServerlists_MouseClick(object sender, MouseEventArgs e)
        {
            _storageService.UpdateServers(null);
        }

        private void btnPublicServers_MouseClick(object sender, MouseEventArgs e)
        {
            ViewState = ServerListViewState.PublicServers;
            editLocalShardControl1.Visible = false;
        }

        private void btnFavoriteServers_MouseClick(object sender, MouseEventArgs e)
        {
            ViewState = ServerListViewState.FavoritesServers;
            editLocalShardControl1.Visible = false;
        }

        private void btnLocalServer_MouseClick(object sender, MouseEventArgs e)
        {
            ViewState = ServerListViewState.LocalServers;
            editLocalShardControl1.Visible = false;
        }

        private void btnSettings_MouseClick(object sender, MouseEventArgs e)
        {
            _kernel.Get<frmSettings>().ShowDialog(this);
        }

        private void btnAddLocalServer_MouseClick(object sender, MouseEventArgs e)
        {
            StorageService dataService = (StorageService)_storageService;
            int id = dataService.GetMinId() - 1;// ExecuteScalar<int>("SELECT IFNULL(MIN(Id), 0) - 1 FROM Server WHERE Public = 0");

            Server server = Server.CreateServer(id, "", "", 0, 0, 0, true, true, "", 2593, 0, 0, 0, 0, 0, false, false, false);
            //_storageService.AddToServerContext(server);

            editLocalShardControl1.Server = server;
            editLocalShardControl1.Visible = true;

            _pendingSave = true;
        }

        private void btnAbout_MouseClick(object sender, MouseEventArgs e)
        {
            _kernel.Get<frmAbout>().ShowDialog(this);
        }

        private void btnFavoriteServers_Load(object sender, EventArgs e)
        {
            btnFavoriteServers.Visible = true;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Visible = false;
        }

        private void btnServerHelp_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.connectuo.com/forum/forumdisplay.php?f=4");
        }

        private void btnFAQ_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.connectuo.com/forum/forumdisplay.php?f=22");
        }

        private void btnReportABug_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.runuo.com/bugs/index.php?cmd=add&project_id=1");
        }

        private void btnClientHelp_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.connectuo.com/forum/forumdisplay.php?f=3");
        }

        private void btnManageMyShards_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.connectuo.com/cuo/index.php?do=management");
        }

        private void txtNews_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            shardListControl.Invalidate();
            BringToFront();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
