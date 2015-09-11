using System;
using System.Diagnostics;
using System.IO;
using ConnectUO.Forms;
using Fds.Core;
using Fds.Core.Diagnostics;
using Fds.Core.Net;
using SevenZip;
using System.Windows.Forms;

namespace ConnectUO
{
    public sealed class ConnectUOVersion
    {
        private static bool _recentlyUpdated;
        private static Logger _log = new Logger(typeof(ConnectUOVersion));

        private string _version;
        private string _updateUrl;
        private string _updateDir = Path.Combine(Program.ApplicationInfo.BaseDirectory, "update");

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public string UpdateUrl
        {
            get { return _updateUrl; }
            set { _updateUrl = value; }
        }

        public bool Match(Version current)
        {
            Version latest = new Version(_version);

            _log.Debug("Version Check returned {0}", latest.CompareTo(current));

            return (latest.CompareTo(current) != 1);        
        }
    }
}
