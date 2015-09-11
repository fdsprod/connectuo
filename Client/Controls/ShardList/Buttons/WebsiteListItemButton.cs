using System.Drawing;
using System.Diagnostics;
using Ninject;

namespace ConnectUO.Controls
{
    public sealed class WebsiteListItemButton : ServerListItemButton
    {
        public WebsiteListItemButton(ServerListItem item)
            : base("Website", item, 85, 55, new Size(60, 12)) { }

        public override void OnClick(IKernel kernel)
        {
            Process.Start(Item.Server.Url);
        }
    }
}
