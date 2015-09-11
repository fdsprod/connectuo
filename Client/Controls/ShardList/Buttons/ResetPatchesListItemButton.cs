using System.Drawing;
using ConnectUO.Framework.Debug;
using ConnectUO.Forms;
using ConnectUO.Framework.Services;
using Ninject;
using ConnectUO.Framework.Windows.Forms;
using System.Windows.Forms;
using System.IO;
using System;
using ConnectUO.Framework.Diagnostics;

namespace ConnectUO.Controls
{
    public sealed class ResetPatchesListItemButton : ServerListItemButton
    {
        public ResetPatchesListItemButton(ServerListItem item)
            : base("Reset Patches", item, 85, 55, new Size(60, 12)) { }

        public override void OnClick(IKernel kernel)
        {
            IStorageService storageService = kernel.Get<IStorageService>();
            ISettingsService settingsService = kernel.Get<ISettingsService>();
            ShellForm form = kernel.Get<ShellForm>();

            Asserter.AssertIsNotNull(form, "form");

            if (MessageBoxEx.Show(form, "This will remove all applied patches for this server from your computer.  Are you sure you want to continue?",
                "ConnectUO 2.0", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                if (storageService.ServerIsCurrentlyBeingPlayed(Item.Server))
                {
                    MessageBoxEx.Show(form,
                        "ConnectUO has detected that you are currently playing this server and cannot reset the patches until the Ultima Online client connected bas been closed.", "ConnectUO 2.0");
                    return;
                }
                else
                {
                    storageService.ResetPatches((int)Item.Server.Id);

                    try
                    {
                        string serverDirectory = Path.Combine(settingsService.PatchDirectory, Uri.EscapeDataString(Item.Server.Name));

                        if (Directory.Exists(serverDirectory))
                        {
                            FileInfo[] files = new DirectoryInfo(serverDirectory).GetFiles();

                            for (int i = 0; i < files.Length; i++)
                            {
                                try
                                {
                                    files[i].Delete();
                                }
                                catch (Exception ex)
                                {
                                    //These aren't crucial, just log as info...
                                    Tracer.Info(ex);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //These aren't crucial, just log as info...
                        Tracer.Info(ex);
                    }
                }

                MessageBoxEx.Show(form, "Patch Reset Complete.", "ConnectUO 2.0");
            }
        }
    }
}
