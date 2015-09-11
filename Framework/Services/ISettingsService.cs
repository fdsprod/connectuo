using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectUO.Framework.Services
{
    public interface ISettingsService
    {
        Guid Guid { get; set; }

        bool IsRazorInstalled { get; }
        bool LaunchRazor { get; set; }
        bool MinimizeOnPlay { get; set; }
        bool RemoveEncryption { get; set; }

        string PatchDirectory { get; set; }
        string StorageDirectory { get; set; }
        string UltimaOnlineDirectory { get; set; }
        string UltimaOnlineExe { get; set; }
        string RazorDirectory { get; }
        string UserDocumentsDirectory { get; }

        int MinutesBetweenUpdates { get; set; }

        void ResetDefaults();
        void AutoDetectUltimaOnlineDirectory();
    }
}
