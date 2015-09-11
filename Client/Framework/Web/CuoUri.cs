using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ConnectUO.Data;
using ConnectUO.Forms;
//using ConnectUO.Config;
using ConnectUO.Framework.Ultima;
using Fds.Core;
using Fds.Core.Diagnostics;
using Microsoft.Win32;
using System.Threading;

namespace ConnectUO.Framework.Web
{
    public class CuoUri
    {
        private static Logger _log = new Logger(typeof(CuoUri));

        public const string Id = "id";
        public const string NameTolken = "n";
        public const string AllowRazorTolken = "rzr";
        public const string PatchesTolken = "pch";
        public const string PortTolken = "p";
        public const string HostTolken = "ip";
        public const string RemoveEncTolken = "renc";

        string[] _requiredFields = new string[] { Id, HostTolken, PortTolken, NameTolken };

        private string _url;

        public CuoUri(string url)
        {
            if (url.Contains("//"))
            {
                string[] split = url.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);

                if (split.Length >= 2)
                {
                    url = split[1];
                }
            }

            _url = url.Replace("/", "");
        }

        public void Play()
        {
            try
            {
                Dictionary<string, string> table = new Dictionary<string, string>();
                string dataString = string.Empty;
                string[] split = null;

                dataString = _url;
                dataString = Uri.UnescapeDataString(dataString);

                bool encoded = Regex.IsMatch(dataString, "^[A-Za-z0-9+/=]+$", RegexOptions.IgnoreCase);

                if (encoded)
                {
                    dataString = Encoding.UTF8.GetString(Convert.FromBase64String(dataString));
                }

                _log.Debug("dataString = {0}", dataString);

                split = dataString.Split('&');

                for (int i = 0; i < split.Length; i++)
                {
                    string[] keyValue = split[i].Split('=');

                    if (keyValue.Length == 2)
                    {
                        string key = keyValue[0];
                        string value = keyValue[1];

                        table.Add(key, value);
                    }
                }

                for (int i = 0; i < _requiredFields.Length; i++)
                {
                    if (!table.ContainsKey(_requiredFields[i]))
                    {
                        MessageBoxEx.Show(Program.MainForm, string.Format("One or more of the following fields was missing from the ConnectUO Url Protocol string: {0}", string.Join(" ", _requiredFields)));
                        return;
                    }
                }

                string idString = table[Id];
                string name = table[NameTolken];
                string portString = table[PortTolken];
                string hostAddress = table[HostTolken];
                string allowRazor;
                string removeEnc;
                string patches;

                bool razor = Program.Database.LaunchRazor;
                bool enc;

                if (!table.TryGetValue(AllowRazorTolken, out allowRazor))
                {
                    allowRazor = "false";
                }

                if (!table.TryGetValue(RemoveEncTolken, out removeEnc))
                {
                    removeEnc = "true";
                }

                if (!table.TryGetValue(PatchesTolken, out patches))
                {
                    patches = "";
                }

                int port;
                int id; 

                if (!int.TryParse(idString, out id))
                {
                    MessageBoxEx.Show(Program.MainForm, "Invalid server id.");
                    return;
                }

                if (!int.TryParse(portString, out port) || (port > 65536 || port < 0))
                {
                    MessageBoxEx.Show(Program.MainForm, "Invalid port number, please use a valid port number between 0 and 65535");
                    return;
                }

                bool shardAllowsRazor;
                bool.TryParse(allowRazor, out shardAllowsRazor);
                bool.TryParse(removeEnc, out enc);

                //Make sure the user wants razor, and that shard allows it.
                razor = razor && shardAllowsRazor;

                if (string.IsNullOrEmpty(Program.Database.UltimaOnlineDirectory))
                {
                    MessageBoxEx.Show(Program.MainForm, "ConnectUO was unable to find the directory that Ultima Online is installed to.  This must be set in order to launch the client.");
                    return;
                }

                if (string.IsNullOrEmpty(Program.Database.UltimaOnlineExe))
                {
                    MessageBoxEx.Show(Program.MainForm, "ConnectUO was unable to find the client executable.  This must be set in order to launch the client.");
                    return;
                }
                
                string folderName = name;
                Utility.EnsureValidFolderName(ref folderName);

                string serverDirectory = Path.Combine(Program.Database.PatchDirectory, folderName);
                string[] patchFiles = null;

                Utility.EnsureDirectory(serverDirectory);

                List<ServerPatch> patchList = new List<ServerPatch>();

                split = patches.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < split.Length; i++)
                {
                    string[] patchVersion = split[i].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    if (patchVersion.Length == 2)
                    {
                        string patchUri = patchVersion[0];
                        string versionString = patchVersion[1];
                        int version;

                        int.TryParse(versionString, out version);

                        ServerPatch patch = new ServerPatch();

                        patch.ShardId = id;
                        patch.PatchUrl = patchUri;
                        patch.Version = version;

                        if (!Program.Database.IsPatchApplied(patch))
                        {
                            patchList.Add(patch);
                        }
                    }
                }

                if (patchList.Count > 0)
                {
                    if (Program.Database.IsServerBeingPatched(id))
                    {
                        MessageBoxEx.Show(Program.MainForm, String.Format("{0} is already being patched, you cannot play until the patching process has been completed", name), "ConnectUO 2.0");
                        return;
                    }
                    else
                    {
                        frmTaskManager taskManager = new frmTaskManager(patchList, serverDirectory);

                        if (taskManager.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        {
                            return;
                        }
                    }
                }

                patchFiles = Directory.GetFiles(serverDirectory);

                ThreadPool.QueueUserWorkItem(delegate(object o)
                {
                    new ConnectUOWebService().UpdatePlayStatistics(Program.Database.Guid, id);
                });

                ClientLauncher.Launch(id, hostAddress, port,
                    Path.Combine(Program.Database.UltimaOnlineDirectory, Program.Database.UltimaOnlineExe),
                    Program.Database.RazorDirectory, razor, enc,
                    Path.Combine(Program.Database.PatchDirectory, folderName), patchFiles);
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
        }

        public static void Initialize()
        {
            RegistryKey rootKey = Registry.ClassesRoot.OpenSubKey("cuo", true);

            if (rootKey == null)
            {
                rootKey = Registry.ClassesRoot.CreateSubKey("cuo");
                rootKey.SetValue("", "URL:ConnectUO Protocol");
                rootKey.SetValue("URL Protocol", "");
            }

            RegistryKey key = rootKey.OpenSubKey("DefaultIcon", true);

            if (key == null)
            {
                key = rootKey.CreateSubKey("DefaultIcon");
            }
            
            if (key.GetValue("") == null || !key.GetValue("").ToString().Equals(
                Program.ApplicationInfo.ExePath, StringComparison.OrdinalIgnoreCase))
            {
                key.SetValue("", Program.ApplicationInfo.ExePath);
            }

            key.Close();
            key = rootKey.OpenSubKey("shell\\open\\command", true);

            if (key == null)
            {
                key = rootKey.CreateSubKey("shell\\open\\command");
            }

            if (key.GetValue("") == null || !key.GetValue("").ToString().Equals(String.Format("{0} \"%1\" -debug",
                Program.ApplicationInfo.ExePath), StringComparison.OrdinalIgnoreCase))
            {
                key.SetValue("", String.Format("{0} \"%1\" -debug", Program.ApplicationInfo.ExePath));
            }

            key.Close();
            rootKey.Close();
        }

        public static string BuildPlayString(Server server)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}={1}&", Id, server.Id);
            sb.AppendFormat("{0}={1}&", NameTolken, server.Name);
            sb.AppendFormat("{0}={1}&", HostTolken, server.HostAddress);
            sb.AppendFormat("{0}={1}&", PortTolken, server.Port);
            sb.AppendFormat("{0}={1}&", RemoveEncTolken, server.RemoveEncryption);
            sb.AppendFormat("{0}={1}", AllowRazorTolken, server.AllowRazor);

            return sb.ToString();
        }
    }
}
