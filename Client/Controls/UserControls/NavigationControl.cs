using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConnectUO.Forms;
using ConnectUO.Framework.Debug;
using Ninject;
using System.Diagnostics;
using ConnectUO.Framework.Services;
using ConnectUO.Framework;

namespace ConnectUO.Controls
{
    public partial class NavigationControl : UserControl
    {
        private ShellForm _mainForm;
        private IKernel _kernel;
        private IShellController _shellController;
        private IStorageService _storageService;

        public ShellForm MainForm
        {
            get { return _mainForm; }
            set { _mainForm = value; }
        }

        [Inject]
        public NavigationControl(IKernel kernel)
            : this()
        {
            Asserter.AssertIsNotNull(kernel, "kernel");

            _kernel = kernel;
            _shellController = _kernel.Get<IShellController>();
            _storageService = _kernel.Get<IStorageService>();

            Asserter.AssertIsNotNull(_shellController, "_shell");
            Asserter.AssertIsNotNull(_storageService, "_storageService");

            _shellController.CurrentViewChanged += new EventHandler(shellController_CurrentViewChanged);

            serverListsGroupControl.SelectPublicServers();
        }

        private void shellController_CurrentViewChanged(object sender, EventArgs e)
        {
            switch (_shellController.CurrentView)
            {
                case Views.PublicServers:
                    optionsGroupControl.SelectNone();
                    serverListsGroupControl.SelectPublicServers();
                    break;
                case Views.FavoritesServers:
                    optionsGroupControl.SelectNone();
                    serverListsGroupControl.SelectFavoriteServers();
                    break;
                case Views.LocalServers:
                    optionsGroupControl.SelectNone();
                    serverListsGroupControl.SelectLocalServer();
                    break;
                case Views.Settings:
                    serverListsGroupControl.SelectNone();
                    optionsGroupControl.SelectSettings();
                    break;
                case Views.EditServer:
                    break;
                case Views.About:
                    serverListsGroupControl.SelectNone();
                    optionsGroupControl.SelectAbout();
                    break;
            }
        }

        public NavigationControl()
        {
            InitializeComponent();
        }

        private void connectionGroupControl_UpdateServersClick(object sender, EventArgs e)
        {
            Asserter.AssertIsNotNull(_storageService, "_storageService");
            _storageService.UpdateServers();
        }

        private void serverListsGroupControl_FavoriteServersClick(object sender, EventArgs e)
        {
            Asserter.AssertIsNotNull(_shellController, "_shell");
            _shellController.CurrentView = Views.FavoritesServers;
        }

        private void serverListsGroupControl_LocalServersClick(object sender, EventArgs e)
        {
            Asserter.AssertIsNotNull(_shellController, "_shell");
            _shellController.CurrentView = Views.LocalServers;
        }

        private void serverListsGroupControl_PublicServersClick(object sender, EventArgs e)
        {
            Asserter.AssertIsNotNull(_shellController, "_shell");
            _shellController.CurrentView = Views.PublicServers;
        }

        private void optionsGroupControl_SettingsClick(object sender, EventArgs e)
        {
            Asserter.AssertIsNotNull(_shellController, "_shell");
            _shellController.CurrentView = Views.Settings;
        }

        private void optionsGroupControl_AboutClick(object sender, EventArgs e)
        {
            Asserter.AssertIsNotNull(_shellController, "_shell");
            _shellController.CurrentView = Views.About;
        }

        private void helpAndSupportGroupControl_ClientHelpClick(object sender, EventArgs e)
        {
            Process.Start("http://www.connectuo.com/forum/forumdisplay.php?f=3");
        }

        private void helpAndSupportGroupControl_ServerHelpClick(object sender, EventArgs e)
        {
            Process.Start("http://www.connectuo.com/forum/forumdisplay.php?f=4");
        }

        private void helpAndSupportGroupControl_FAQClick(object sender, EventArgs e)
        {
            Process.Start("http://www.connectuo.com/forum/forumdisplay.php?f=22");
        }

        private void helpAndSupportGroupControl_ReportBugClick(object sender, EventArgs e)
        {
            Process.Start("http://www.runuo.com/bugs/index.php?cmd=add&project_id=1");
        }
    }
}
