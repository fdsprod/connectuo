using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ConnectUO.Forms;
using ConnectUO.Framework.Data;
using ConnectUO.Framework.Ultima;
using ConnectUO.Framework;
using ConnectUO.Framework.Windows.Forms;
using Microsoft.Win32;
using ConnectUO.Framework.Services;
using ConnectUO.Framework.Diagnostics;
using Ninject;
using ConnectUO.Framework.Debug;
using ConnectUO.Framework.IO;

namespace ConnectUO.Framework.Web
{
    public class CuoUri
    {
        public const string Id = "id";
        public const string NameTolken = "n";
        public const string AllowRazorTolken = "rzr";
        public const string PatchesTolken = "pch";
        public const string PortTolken = "p";
        public const string HostTolken = "ip";
        public const string RemoveEncTolken = "renc";

        private readonly string[] _requiredFields = new string[] { Id, HostTolken, PortTolken, NameTolken };

        private IKernel _kernel;
        private IStorageService _storageService;
        private ISettingsService _settingsService;
        private IApplicationService _applicationService;

        [Inject]
        public CuoUri(
            IKernel kernel,
            IStorageService storageService,
            IApplicationService applicationService,
            ISettingsService settingsService)
        {
            _kernel = kernel;
            _storageService = storageService;
            _settingsService = settingsService;
            _applicationService = applicationService;
        }

        public void Play(string url)
        {
            try
            {
                string[] split = null;

                if (url.Contains("//"))
                {
                    split = url.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);

                    if (split.Length >= 2)
                    {
                        url = split[1];
                    }
                }

                url = url.Replace("/", "");

                Dictionary<string, string> table = new Dictionary<string, string>();
                string dataString = string.Empty;

                dataString = url;
                dataString = Uri.UnescapeDataString(dataString);

                bool encoded = Regex.IsMatch(dataString, "^[A-Za-z0-9+/=]+$", RegexOptions.IgnoreCase);

                if (encoded)
                {
                    dataString = Encoding.UTF8.GetString(Convert.FromBase64String(dataString));
                }

                Tracer.Verbose("dataString = {0}", dataString);

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
                        MessageBoxEx.Show(_applicationService.MainForm, string.Format("One or more of the following fields was missing from the ConnectUO Url Protocol string: {0}", string.Join(" ", _requiredFields)));
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

                bool razor = _settingsService.LaunchRazor;
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
                    MessageBoxEx.Show(_applicationService.MainForm, "Invalid server id.");
                    return;
                }

                if (!int.TryParse(portString, out port) || (port > 65536 || port < 0))
                {
                    MessageBoxEx.Show(_applicationService.MainForm, "Invalid port number, please use a valid port number between 0 and 65535");
                    return;
                }

                bool shardAllowsRazor;
                bool.TryParse(allowRazor, out shardAllowsRazor);
                bool.TryParse(removeEnc, out enc);

                //Make sure the user wants razor, and that shard allows it.
                razor = razor && shardAllowsRazor;

                if (string.IsNullOrEmpty(_settingsService.UltimaOnlineDirectory))
                {
                    MessageBoxEx.Show(_applicationService.MainForm, "ConnectUO was unable to find the directory that Ultima Online is installed to.  This must be set in order to launch the client.");
                    return;
                }

                if (string.IsNullOrEmpty(_settingsService.UltimaOnlineExe))
                {
                    MessageBoxEx.Show(_applicationService.MainForm, "ConnectUO was unable to find the client executable.  This must be set in order to launch the client.");
                    return;
                }
                
                string folderName = name;
                EnsureValidFolderName(ref folderName);

                string serverDirectory = Path.Combine(_settingsService.PatchDirectory, folderName);
                string[] patchFiles = null;

                FileSystemHelper.EnsureDirectoryExists(serverDirectory);

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

                        patchList.Add(patch);

                        //if (!_storageService.IsPatchApplied(patch))
                        //{
                        //    //_storageService.ResetPatches(id);                        
                        //    patchList.Add(patch);
                        //    //i = 0;
                        //    //continue;
                        //}

                    }
                }

                ServerPatch[] notPatched = (from p in patchList where !_storageService.IsPatchApplied(p) select p).ToArray();

                if (notPatched.Length > 0)
                {
                    if (_storageService.IsServerBeingPatched(id))
                    {
                        MessageBoxEx.Show(_applicationService.MainForm, String.Format("{0} is already being patched, you cannot play until the patching process has been completed", name), "ConnectUO 2.0");
                        return;
                    }
                    else
                    {
                        //_storageService.ResetPatches(id);

                        PatcherForm taskManager = _kernel.Get<PatcherForm>();

                        taskManager.Patches = notPatched;
                        taskManager.ServerDirectory = serverDirectory;

                        if (taskManager.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        {
                            return;
                        }
                    }
                }

                patchFiles = Directory.GetFiles(serverDirectory);

                _storageService.UpdatePlayStatistics(_settingsService.Guid, id);

                ClientLauncher.Launch(
                    _kernel,
                    id, 
                    hostAddress,
                    port,
                    Path.Combine(_settingsService.UltimaOnlineDirectory, _settingsService.UltimaOnlineExe),
                    _settingsService.RazorDirectory, 
                    razor, 
                    enc,
                    Path.Combine(_settingsService.PatchDirectory, folderName), 
                    patchFiles);
            }
            catch (Exception e)
            {
                Tracer.Error(e);
            }
        }
        
        public static void EnsureValidFolderName(ref string folderName)
        {
            char[] invalidFileChars = Path.GetInvalidFileNameChars();

            for (int i = 0; i < invalidFileChars.Length; i++)
            {
                folderName = folderName.Replace(invalidFileChars[i], '_');
            }
        }

        public static void Initialize(IApplicationService applicationService)
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
                applicationService.ApplicationInfo.ExePath, StringComparison.OrdinalIgnoreCase))
            {
                key.SetValue("", applicationService.ApplicationInfo.ExePath);
            }

            key.Close();
            key = rootKey.OpenSubKey("shell\\open\\command", true);

            if (key == null)
            {
                key = rootKey.CreateSubKey("shell\\open\\command");
            }

            if (key.GetValue("") == null || !key.GetValue("").ToString().Equals(String.Format("{0} \"%1\" -debug",
                applicationService.ApplicationInfo.ExePath), StringComparison.OrdinalIgnoreCase))
            {
                key.SetValue("", String.Format("{0} \"%1\" -debug", applicationService.ApplicationInfo.ExePath));
            }

            key.Close();
            rootKey.Close();
        }

        public static string BuildPlayString(Server server)
        {
            Asserter.AssertIsNotNull(server, "server");

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
