using System.Drawing;
using ConnectUO.Framework.Services;
using Ninject;

namespace ConnectUO.Controls
{
    public sealed class RemoveFavoriteShardListItemButton : ServerListItemButton
    {
        public RemoveFavoriteShardListItemButton(ServerListItem item)
            : base("Remove", item, 85, 25, new Size(60, 12)) { }

        public override void OnClick(IKernel kernel)
        {            
            Item.Server.Favorite = false;
            kernel.Get<IStorageService>().SaveChanges();
            Item.Parent.RefreshDataSource();
        }
    }
}
