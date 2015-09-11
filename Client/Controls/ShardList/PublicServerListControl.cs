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

namespace ConnectUO.Controls
{
    public partial class PublicServerListControl : ServerListControl
    {
        [Inject]
        public PublicServerListControl(IKernel kernel)
            : base(kernel)
        {
            InitializeComponent();

            StorageService.ServersUpdateComplete += (sender, e) => ControlInvoker.Invoke(this, () => RefreshDataSource());
        }
        
        protected override Server[] GetServers()
        {
            return StorageService.PublicServers;
        }

        protected override void AddServerItemButtons(ServerListItem item)
        {
            item.Buttons.Add(new PlayShardListItemButton(item));

            if (!item.Server.Favorite)
            {
                item.Buttons.Add(new AddToFavoritesShardListItemButton(item));
            }

            item.Buttons.Add(new WebsiteListItemButton(item));

            if (item.Server.HasPatches)
            {
                item.Buttons.Add(new ResetPatchesListItemButton(item));
            }
        }

        public override void Activate()
        {
            base.Activate();
        }
    }
}
