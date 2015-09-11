using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConnectUO.Framework;
using ConnectUO.Framework.Diagnostics;
using Ninject;

namespace ConnectUO.Framework.Services
{
    public interface IApplicationService
    {
        bool Debug { get; }
        Form MainForm { get; set; }
        string FormattedVersionString { get; }
        ApplicationInfo ApplicationInfo { get; set; }
        List<TraceListener> TraceListeners { get; }
    }
}
