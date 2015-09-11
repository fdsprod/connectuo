using System.Drawing;
using Ninject;
using ConnectUO.Framework.Diagnostics;
using ConnectUO.Forms;
using System;
using ConnectUO.Framework.Debug;
using ConnectUO.Framework.Web;
using ConnectUO.Framework.Data;
using ConnectUO.Framework.Services;
using System.Text;
using ConnectUO.Framework.Windows.Forms;

namespace ConnectUO.Controls
{
    public sealed class PlayShardListItemButton : ServerListItemButton
    {
        public PlayShardListItemButton(ServerListItem item)
            : base("Play", item, 85, 10, new Size(60, 12)) { }

        public override void OnClick(IKernel kernel)
        {
            IStorageService storageService = kernel.Get<IStorageService>();
            ShellForm form = kernel.Get<ShellForm>();

            Asserter.AssertIsNotNull(form, "form");
            Asserter.AssertIsNotNull(Item.Server, "Item.Server");

            form.SetStatus("Preparing to play {0}...", Item.Server.Name);

            string uri = CuoUri.BuildPlayString(Item.Server);

            if (Item.Server.HasPatches)
            {
                Tracer.Verbose("Server has patches, checking...");

                if (Item.Server.Public)
                {
                    form.SetStatus("Retrieving patching information for {0}...", Item.Server.Name);

                    try
                    {
                        ServerPatch[] patches = storageService.GetPatches((int)Item.Server.Id);
                        Tracer.Verbose("Found {0} patches...", patches.Length);

                        StringBuilder sb = new StringBuilder();

                        for (int i = 0; i < patches.Length; i++)
                        {
                            sb.AppendFormat("{0}|{1}{2}", patches[i].PatchUrl, patches[i].Version, i + 1 < patches.Length ? ";" : "");
                        }

                        uri = string.Join(string.Format("&{0}=", CuoUri.PatchesTolken), new string[] { uri, sb.ToString() });
                    }
                    catch (Exception ex)
                    {
                        Tracer.Error(ex);
                        MessageBoxEx.Show(form, "Unable to get patch information for this server.  See the debug log for details.", "ConnectUO 2.0");
                    }
                }
                else
                {
                    try
                    {
                        LocalPatch[] patches = storageService.GetLocalPatches((int)Item.Server.Id);

                        Tracer.Verbose("Found {0} patches...", patches.Length);

                        StringBuilder sb = new StringBuilder();

                        for (int i = 0; i < patches.Length; i++)
                        {
                            sb.AppendFormat("{0}|{1}{2}", patches[i].PatchUrl, patches[i].Version, i + 1 < patches.Length ? ";" : "");
                        }

                        uri = string.Join(string.Format("&{0}=", CuoUri.PatchesTolken), new string[] { uri, sb.ToString() });
                    }
                    catch (Exception ex)
                    {
                        Tracer.Error(ex);
                        MessageBoxEx.Show(form, "Unable to get patch information for this server.  See the debug log for details.", "ConnectUO 2.0");
                    }
                }
            }

            Tracer.Verbose("Play URI: {0}", uri);
            uri = string.Format("cuo://{0}", Uri.EscapeDataString(Convert.ToBase64String(Encoding.UTF8.GetBytes(uri))));

            kernel.Get<CuoUri>().Play(uri);

            form.SetStatus(string.Empty);
        }
    }
}
