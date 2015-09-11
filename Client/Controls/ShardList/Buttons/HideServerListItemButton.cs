using System.Drawing;

namespace ConnectUO.Controls
{
    public sealed class HideListItemButton : ServerListItemButton
    {
        public HideListItemButton(ServerListItem item)
            : base("Hide", item, 85, 55, new Size(60, 12)) { }

        public override void OnClick(Ninject.IKernel kernel)
        {
            throw new System.NotImplementedException();
        }
    }
}
