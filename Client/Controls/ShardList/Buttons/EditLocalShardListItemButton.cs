using System.Drawing;
using Ninject;

namespace ConnectUO.Controls
{
    public sealed class EditLocalShardListItemButton : ServerListItemButton
    {
        public EditLocalShardListItemButton(ServerListItem item)
            : base("Edit", item, 85, 40, new Size(60, 12)) { }

        public override void OnClick(IKernel kernel)
        {
            EditLocalServerControl control = kernel.Get<EditLocalServerControl>();

            control.Server = Item.Server;
            control.Visible = true;
        }
    }
}
