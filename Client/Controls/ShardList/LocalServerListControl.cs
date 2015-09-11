using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ninject;
using ConnectUO.Framework.Services;
using ConnectUO.Framework.Data;
using ConnectUO.Framework.Controls;
using ConnectUO.Forms;
using ConnectUO.Framework;

namespace ConnectUO.Controls
{
    public partial class LocalServerListControl : ServerListControl
    {
        private IShellController _shellController;
        private ISiteService _siteService;
        private ArrowButton _addLocalServerButton;
        private EditLocalServerControl _editLocalServerControl;

        [Inject]
        public LocalServerListControl(IKernel kernel)
            : base(kernel)
        {
            _shellController = kernel.Get<IShellController>();
            _siteService = kernel.Get<ISiteService>();
            _editLocalServerControl = kernel.Get<EditLocalServerControl>();

            _addLocalServerButton = new ArrowButton();
            _addLocalServerButton.Text = "Add Local Server";
            _addLocalServerButton.Click += addLocalServerButton_Click;

            InitializeComponent();
        }

        private void addLocalServerButton_Click(object sender, EventArgs e)
        {
            long id = -1;

            if (StorageService.LocalServers.Length > 0)
            {
                id = StorageService.LocalServers.OrderBy((s) => s.Id).First().Id - 1;
            }

            Server server = 
                Server.CreateServer(id, "", "", 0, 0, 0, true, true, "", 2593, 0, 0, 0, 0, 0, false, false, false);

            _editLocalServerControl.Server = server;
            _shellController.CurrentView = Views.EditServer;
        }

        protected override Server[] GetServers()
        {
            return StorageService.LocalServers;
        }

        protected override void AddServerItemButtons(ServerListItem item)
        {
            item.Buttons.Add(new PlayShardListItemButton(item));
            item.Buttons.Add(new EditLocalShardListItemButton(item));
            item.Buttons.Add(new RemoveCustomShardListItemButton(item));

            if (item.Server.HasPatches)
            {
                item.Buttons.Add(new ResetPatchesListItemButton(item));
            }
        }

        public override void Activate()
        {
            base.Activate();

            _siteService.Get<Control>(SiteNames.ContentActionsSite).Controls.Add(_addLocalServerButton);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            _siteService.Get<Control>(SiteNames.ContentActionsSite).Controls.Remove(_addLocalServerButton);
        }
    }
}
