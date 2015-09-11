using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectUO.Framework.Services;
using System.Windows.Forms;
using ConnectUO.Framework;
using ConnectUO.Framework.Diagnostics;
using Ninject;

namespace ConnectUO.Services
{
    public sealed class ApplicationService : IApplicationService
    {
        private ApplicationInfo _applicationInfo;

        public Form MainForm
        {
            get;
            set;
        }

        public ApplicationInfo ApplicationInfo
        {
            get { return _applicationInfo; }
            set
            {
                if (_applicationInfo != value)
                {
                    _applicationInfo = value;

                    if (value != null)
                    {
                        _applicationInfo.StartupArgumentParser.AutoSetMembers(this);
                    }
                }
            }
        }
        
        public List<TraceListener> TraceListeners
        {
            get;
            private set;
        }

        [StartupArgumentMember("debug", SwitchMeansFalse=false)]
        public bool Debug
        {
            get;
            internal set;
        }

        public string FormattedVersionString
        {
            get { return string.Format("Version: {0} Beta", ApplicationInfo.Version); }
        }

        [Inject]
        public ApplicationService()
        {
            TraceListeners = new List<TraceListener>();
        }
    }
}
