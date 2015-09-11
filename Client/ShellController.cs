using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using ConnectUO.Framework.Debug;
using ConnectUO.Forms;
using ConnectUO.Framework.Services;
using System.Windows.Forms;
using ConnectUO.Controls;
using ConnectUO.Framework.Controls;
using ConnectUO.Controls.ShardList;
using System.Reflection;
using System.ComponentModel;
using ConnectUO.Framework;
using ConnectUO.Extensions;

namespace ConnectUO
{
    public sealed class ShellController : IShellController
    {
        private Views _currentView;

        private IKernel _kernel;
        private IShell _shell;
        private ISiteService _siteService;
        private IStorageService _storageService;
        private ISettingsService _settingsService;
        private IApplicationService _applicationService;
        private PublicServerListControl _publicServerListControl;
        private FavoritesServerListControl _favoritesServerListControl;
        private LocalServerListControl _localServerListControl;
        private Control[] _views;
        private string _filterText;
        private ServerListOrderBy _sortBy;
        private bool _sortdescending;

        public string FilterText
        {
            get { return _filterText; }
            set 
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    _publicServerListControl.FilterText = value;
                    _favoritesServerListControl.FilterText = value;
                    _localServerListControl.FilterText = value;
                }
            }
        }

        public ServerListOrderBy SortBy
        {
            get { return _sortBy; }
            set
            {
                if (_sortBy != value)
                {
                    _sortBy = value;
                    _publicServerListControl.OrderBy = value;
                    _favoritesServerListControl.OrderBy = value;
                    _localServerListControl.OrderBy = value;
                }           
            }
        }

        public bool SortDescending
        {
            get { return _sortdescending; }
            set
            {
                if (_sortdescending != value)
                {
                    _sortdescending = value;
                    _publicServerListControl.OrderByReversed = value;
                    _favoritesServerListControl.OrderByReversed = value;
                    _localServerListControl.OrderByReversed = value;
                }
            }
        }

        public Views CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    Control visibleControl = _views[(int)_currentView];

                    visibleControl.Visible = false;

                    if (visibleControl is IDeactivate)
                        ((IDeactivate)visibleControl).Deactivate();

                    _currentView = value;

                    visibleControl = _views[(int)value];

                    visibleControl.Visible = true;

                    if (visibleControl is IActivate)
                        ((IActivate)visibleControl).Activate();

                    _shell.Text = string.Format("ConnectUO - Version: {0} Beta - {1}", _applicationService.ApplicationInfo.Version, value.GetDescription());

                    if (CurrentViewChanged != null)
                    {
                        CurrentViewChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public event EventHandler CurrentViewChanged;

        [Inject]
        public ShellController(IKernel kernel)
        {
            Asserter.AssertIsNotNull(kernel, "kernel");

            _kernel = kernel;
        }

        public void Initialize()
        {
            _shell = _kernel.Get<IShell>();
            _applicationService = _kernel.Get<IApplicationService>();
            _storageService = _kernel.Get<IStorageService>();
            _settingsService = _kernel.Get<ISettingsService>();
            _siteService = _kernel.Get<ISiteService>();

            Asserter.AssertIsNotNull(_shell, "_shell");
            Asserter.AssertIsNotNull(_applicationService, "_applicationService");
            Asserter.AssertIsNotNull(_storageService, "_storageService");
            Asserter.AssertIsNotNull(_settingsService, "_settingsService");
            Asserter.AssertIsNotNull(_siteService, "_siteService");

            _storageService.Error += (s, e) => _shell.SetStatus(string.Format("Error: {0}", e.Exception.Message));
            _storageService.ServersUpdateBegin += (s, e) => _shell.SetStatus("Updating Servers...");
            _storageService.ServersUpdateComplete += (s, e) => _shell.SetStatus(string.Empty);

            CreateViews();

            CurrentView = Views.PublicServers;
        }

        private void CreateViews()
        {
            _views = new Control[] {
                _kernel.Get<PublicServerListControl>(),
                _kernel.Get<FavoritesServerListControl>(),
                _kernel.Get<LocalServerListControl>(),
                _kernel.Get<EditLocalServerControl>(),
                _kernel.Get<SettingsControl>(),
                _kernel.Get<AboutControl>()
            };

            _publicServerListControl = (PublicServerListControl)_views[0];
            _favoritesServerListControl = (FavoritesServerListControl)_views[1];
            _localServerListControl = (LocalServerListControl)_views[2];

            Control contentSite = _siteService.Get<Control>(SiteNames.ContentSite);
            Control navigationSite = _siteService.Get<Control>(SiteNames.NavigationSite);

            for (int i = 0; i < _views.Length; i++)
            {
                Control view = _views[i];

                view.Visible = view is PublicServerListControl;
                view.Dock = DockStyle.Fill;

                contentSite.Controls.Add(view);
            }

            NavigationControl navigationControl = _kernel.Get<NavigationControl>();

            navigationControl.Dock = DockStyle.Top;
            navigationSite.Controls.Add(navigationControl);
        }
    }
}
