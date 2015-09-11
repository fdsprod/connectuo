using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectUO.Framework.Diagnostics
{
    public sealed class DebugTraceListener : TraceListener
    {
        protected override void OnTraceReceived(TraceMessage message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
