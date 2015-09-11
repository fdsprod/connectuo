using System.Drawing;
using Ninject;
using ConnectUO.Framework.Services;
using ConnectUO.Forms;
using ConnectUO.Framework;

namespace ConnectUO.Controls
{
    public sealed class AddToFavoritesShardListItemButton : ServerListItemButton
    {
        public AddToFavoritesShardListItemButton(ServerListItem item)
            : base("Favorite", item, 85, 25, new Size(60, 12)) { }

        public override void OnClick(IKernel kernel)
        {
            Item.Server.Favorite = true;

            kernel.Get<IStorageService>().SaveChanges();
            kernel.Get<IShellController>().CurrentView = Views.FavoritesServers;
        }
    }
}
