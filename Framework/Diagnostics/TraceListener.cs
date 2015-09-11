using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectUO.Framework.Diagnostics
{
    public abstract class TraceListener : IDisposable, ITraceListener
    {
        public virtual TraceLevels? TraceLevel { get; set; }

        protected TraceListener()
        {
            Tracer.TraceReceived += OnTraceReceived;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void OnTraceReceived(TraceMessageEventArgs e)
        {
            TraceMessage message = e.TraceMessage;

            if (TraceLevel.HasValue && Tracer.TraceLevel < message.Type)
                return;

            if (TraceLevel.HasValue && message.Type < TraceLevel)
                return;

            OnTraceReceived(message);
        }

        protected abstract void OnTraceReceived(TraceMessage message);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Tracer.TraceReceived -= OnTraceReceived;
            }
        }
    }
}
