using System.Drawing;
using ConnectUO.Framework.Windows.Forms;
using ConnectUO.Forms;
using Ninject;
using ConnectUO.Framework.Services;
using System.Windows.Forms;

namespace ConnectUO.Controls
{
    public sealed class RemoveCustomShardListItemButton : ServerListItemButton
    {
        public RemoveCustomShardListItemButton(ServerListItem item) 
            : base("Remove", item, 85, 25, new Size(60, 12) ){ }

        public override void OnClick(IKernel kernel)
        {
            ShellForm form = kernel.Get<ShellForm>();
            IStorageService storageService = kernel.Get<IStorageService>();

            if (MessageBoxEx.Show(form, "Are you sure you want to remove this server?",
                "ConnectUO 2.0", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                storageService.DeleteServer(Item.Server);
                Item.Parent.RefreshDataSource();
            }
        }
    }
}
