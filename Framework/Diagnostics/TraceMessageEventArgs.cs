using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectUO.Framework.Debug;

namespace ConnectUO.Framework.Diagnostics
{
    public sealed class TraceMessageEventArgs : EventArgs
    {
        public TraceMessage TraceMessage { get; private set; }

        public TraceMessageEventArgs(TraceMessage traceMessage)
        {
            Asserter.AssertIsNotNull(traceMessage, "traceMessage");

            TraceMessage = traceMessage;
        }
    }
}
