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
using ConnectUO.Framework;
using ConnectUO.Framework.Diagnostics;
using System.Threading;
using ConnectUO.Framework.Data;
using ConnectUO.Framework.Windows.Forms;
using System.Xml;
using System.Net;
using ConnectUO.Framework.Services;
using System.ComponentModel;
using Ninject;
using ConnectUO.Controls;
using System.Collections.Generic;
using ConnectUO.Framework.Debug;
using System.Data;
using ConnectUO.Framework.Controls;
using ConnectUO.Controls.ShardList;
using System.Reflection;

namespace ConnectUO.Forms
{    
    public sealed partial class ShellForm : SkinnableForm, IShell
    {
        private ServerComparer _comparer = new ServerComparer();
        private bool _pendingSave = false;

        private ShellController _controller;

        private IKernel _kernel;
        private ISiteService _siteService;
        private IStorageService _storageService;
        private ISettingsService _settingsService;
        private IApplicationService _applicationService;
        
        [Inject]
        public ShellForm(IKernel kernel)
        {
            Asserter.AssertIsNotNull(kernel, "kernel");

            _kernel = kernel;
            _applicationService = _kernel.Get<IApplicationService>();
            _storageService = _kernel.Get<IStorageService>();
            _settingsService = _kernel.Get<ISettingsService>();
            _siteService = _kernel.Get<ISiteService>();
            _controller = _kernel.Get<ShellController>();

            Asserter.AssertIsNotNull(_applicationService, "_applicationService");
            Asserter.AssertIsNotNull(_storageService, "_storageService");
            Asserter.AssertIsNotNull(_settingsService, "_settingsService");
            Asserter.AssertIsNotNull(_siteService, "_siteService");

            InitializeComponent();

            _siteService.Register(SiteNames.ContentSite, contentPanel);
            _siteService.Register(SiteNames.NavigationSite, navigationPanel);
            _siteService.Register(SiteNames.ContentActionsSite, contentActionPanel);

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            updateTimer.Enabled = true;
            updateTimer.Start();
            
            cboSortBy.DataSource = CreateSortByDataSources();
            cboSortBy.SelectedIndex = 0;

            ThreadPool.QueueUserWorkItem((o) => _storageService.UpdateServers());

            _controller.Initialize();

            BringToFront();
        }

        private object CreateSortByDataSources()
        {
            List<string> sortBys = new List<string>();
            string[] sortByStrings = Enum.GetNames(typeof(ServerListOrderBy));

            for (int i = 0; i < sortByStrings.Length; i++)
            {
                string itemName = "Default";

                if (i > 0)
                {
                    itemName = Utility.ProperSpace(sortByStrings[i]);
                }

                sortBys.Add(itemName);
            }

            return sortBys;
        }
                
        private void OnShardEditAddComplete(object sender, EventArgs e)
        {
            //if (_pendingSave)
            //{
            //    _pendingSave = false;

            //    bool addToDatabase = 
            //        (from s in _storageService.LocalServers 
            //         where s.Id == editLocalShardControl1.Server.Id 
            //         select s).FirstOrDefault() == null;

            //    if (addToDatabase)
            //    {
            //        _storageService.AddServer(editLocalShardControl1.Server);
            //    }

            //    if (editLocalShardControl1.Server.EntityState != EntityState.Unchanged)
            //    {
            //        _storageService.SaveChanges();
            //    }
            //}

            //editLocalShardControl1.Visible = false;
        }

        private void CheckForUpdates(object sender, EventArgs e)
        {
            _storageService.UpdateServers(null);
        }

        public void SetStatus(string status, params object[] args)
        {
            status = string.Format(status, args);

            Tracer.Verbose(status);

            ControlInvoker.Invoke(lblStatus, () => lblStatus.Text = status);
        }
        
        private void OnFilterTextChanged(object sender, EventArgs e)
        {
            txtFilter.Focus();
            _controller.FilterText = txtFilter.Text;
        }
        
        private void OnServerUpdateComplete(object sender, EventArgs e)
        {
            //RefreshServerView();
        }
        
        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            //shardListControl.Invalidate();
            BringToFront();
        }

        private void OnSortBySelectedIndexChanged(object sender, EventArgs e)
        {
            cboSortBy.Focus();
            _controller.SortBy = (ServerListOrderBy)cboSortBy.SelectedIndex;
        }

        private void OnReverseSortByCheckedChanged(object sender, EventArgs e)
        {
            _controller.SortDescending = chkReverseSortBy.Checked;
            //RefreshServerView();
        }

        private void OnMainSizeChanged(object sender, EventArgs e)
        {
            ShowInTaskbar = !(notifyIcon.Visible = (WindowState == FormWindowState.Minimized));

            //Point location = shardListControl.Location;
        }

        private void btnUpdateServerlists_MouseClick(object sender, MouseEventArgs e)
        {
            _storageService.UpdateServers(null);
        }
        
        private void btnAddLocalServer_MouseClick(object sender, MouseEventArgs e)
        {
        }
        
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Visible = false;
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
            //shardListControl.Invalidate();
            BringToFront();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
