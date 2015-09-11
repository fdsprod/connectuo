using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectUO.Framework.Services;
using Ninject;
using ConnectUO.Framework.Debug;
using System.IO;
using ConnectUO.Framework.Windows;
using Microsoft.Win32;
using ConnectUO.Framework.Ultima;

namespace ConnectUO.Services
{
    public class SettingsService : ISettingsService
    {
        private IStorageService _storageService;
        private IApplicationService _applicationService;

        [Inject]
        public SettingsService(
            IStorageService storageService,
            IApplicationService applicationService)
        {
            Asserter.AssertIsNotNull(storageService, "storageService");
            Asserter.AssertIsNotNull(applicationService, "applicationService");

            _storageService = storageService;
            _applicationService = applicationService;
        }

        public Guid Guid
        {
            get
            {
                Guid? value = _storageService.GetConfigValue<Guid?>("Guid");

                if (!value.HasValue)
                {
                    value = Guid.NewGuid();
                    _storageService.SetConfigValue<Guid>("Guid", value.Value);
                }

                return value.Value;
            }
            set
            {
                _storageService.SetConfigValue<Guid>("Guid", value);
                _storageService.SaveChanges();
            }
        }

        public string StorageDirectory
        {
            get
            {
                string value = _storageService.GetConfigValue<string>("WorkState");

                if (string.IsNullOrEmpty(value))
                {
                    value = Path.Combine(UserDocumentsDirectory, "ConnectUO");
                    _storageService.SetConfigValue<string>("WorkState", value);
                }

                return value;
            }
            set
            {
                _storageService.SetConfigValue<string>("WorkState", value);
                _storageService.SaveChanges();
            }
        }

        public string UltimaOnlineDirectory
        {
            get
            {
                string value = _storageService.GetConfigValue<string>("UltimaOnlineDirectory");

                if (string.IsNullOrEmpty(value))
                {
                    value = LocateClientDirectory();
                    _storageService.SetConfigValue<string>("UltimaOnlineDirectory", value);
                }

                return value;
            }
            set
            {
                _storageService.SetConfigValue<string>("UltimaOnlineDirectory", value);
                _storageService.SaveChanges();
            }
        }

        public string UltimaOnlineExe
        {
            get
            {
                string value = _storageService.GetConfigValue<string>("UltimaOnlineExe");

                if (string.IsNullOrEmpty(value))
                {
                    value = LocateClientExe();
                    _storageService.SetConfigValue<string>("UltimaOnlineExe", value);
                }

                return value;
            }
            set
            {
                _storageService.SetConfigValue<string>("UltimaOnlineExe", value);
                _storageService.SaveChanges();
            }
        }

        public string PatchDirectory
        {
            get
            {
                string value = _storageService.GetConfigValue<string>("PatchDirectory");

                if (string.IsNullOrEmpty(value))
                {
                    value = Path.Combine(StorageDirectory, "Servers");
                    _storageService.SetConfigValue<string>("PatchDirectory", value);
                }

                return value;
            }
            set
            {
                _storageService.SetConfigValue<string>("PatchDirectory", value);
                _storageService.SaveChanges();
            }
        }

        public bool MinimizeOnPlay
        {
            get
            {
                bool? value = _storageService.GetConfigValue<bool?>("MinimizeOnPlay");

                if (!value.HasValue)
                {
                    value = true;
                    _storageService.SetConfigValue<bool>("MinimizeOnPlay", value.Value);
                }

                return value.Value;
            }
            set
            {
                _storageService.SetConfigValue<bool>("MinimizeOnPlay", value);
                _storageService.SaveChanges();
            }
        }

        public bool LaunchRazor
        {
            get
            {
                bool? value = _storageService.GetConfigValue<bool?>("LaunchRazor");

                if (!value.HasValue)
                {
                    value = IsRazorInstalled;
                    _storageService.SetConfigValue<bool>("LaunchRazor", value.Value);
                }

                return value.Value;
            }
            set
            {
                _storageService.SetConfigValue<bool>("LaunchRazor", value);
                _storageService.SaveChanges();
            }
        }

        public bool RemoveEncryption
        {
            get
            {
                bool? value = _storageService.GetConfigValue<bool?>("RemoveEncryption");

                if (!value.HasValue)
                {
                    value = true;
                    _storageService.SetConfigValue<bool>("RemoveEncryption", value.Value);
                }

                return value.Value;
            }
            set
            {
                _storageService.SetConfigValue<bool>("RemoveEncryption", value);
                _storageService.SaveChanges();
            }
        }

        public int MinutesBetweenUpdates
        {
            get
            {
                int? value = _storageService.GetConfigValue<int?>("MinutesBetweenUpdates");

                if (!value.HasValue)
                {
                    value = 10;
                    _storageService.SetConfigValue<int>("MinutesBetweenUpdates", value.Value);
                }

                return value.Value;
            }
            set
            {
                _storageService.SetConfigValue<int>("MinutesBetweenUpdates", value);
                _storageService.SaveChanges();
            }
        }

        public string UserDocumentsDirectory
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); }
        }

        public string RazorDirectory
        {
            get
            {
                string val = "InstallDir";
                string keyPath = "SOFTWARE\\Razor";

                try
                {
                    if (_applicationService.ApplicationInfo.Is64Bit)
                    {
                        keyPath = @"SOFTWARE\Wow6432Node\Razor";
                    }

                    if (RegistryHelper.Get(Registry.LocalMachine, keyPath, ref val) == RegistryError.Error)
                    {
                        val = string.Empty;
                    }
                }
                catch
                {
                    return String.Empty;
                }

                return Path.Combine(val, "razor.exe");
            }
        }

        public bool IsRazorInstalled
        {
            get
            {
                RegistryKey key = null;

                try
                {
                    if (_applicationService.ApplicationInfo.Is64Bit)
                    {
                        key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Razor");
                    }
                    else
                    {
                        key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Razor");
                    }

                    if (key == null)
                    {
                        return false;
                    }

                    string path = key.GetValue("InstallDir") as string;

                    if (((path == null) || (path.Length <= 0)) ||
                        (!Directory.Exists(path)))
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        public string LocateClientDirectory()
        {
            string firstPath = "";

            if (Client.ValidClientPaths.Count > 0)
                firstPath = Client.ValidClientPaths[0];

            return firstPath;
        }

        public string LocateClientExe()
        {
            string clientExe = "client.exe";

            if (!string.IsNullOrEmpty(UltimaOnlineDirectory) &&
                !string.IsNullOrEmpty(LocateClientDirectory()))
            {
                string clientExePath = Path.Combine(UltimaOnlineDirectory, clientExe);
            }

            return clientExe;
        }

        public void AutoDetectUltimaOnlineDirectory()
        {
            UltimaOnlineDirectory = LocateClientDirectory();
        }

        public void ResetDefaults()
        {
            Guid = System.Guid.NewGuid();
            StorageDirectory = Path.Combine(UserDocumentsDirectory, "ConnectUO");
            UltimaOnlineDirectory = LocateClientDirectory();
            UltimaOnlineExe = "client.exe";
            PatchDirectory = Path.Combine(StorageDirectory, "Servers");
            LaunchRazor = IsRazorInstalled;
            RemoveEncryption = true;
            MinutesBetweenUpdates = 10;
            MinimizeOnPlay = true;
        }
    }
}
