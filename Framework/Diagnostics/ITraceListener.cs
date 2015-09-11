using System;

namespace ConnectUO.Framework.Diagnostics
{
    public interface ITraceListener : IDisposable
    {
        TraceLevels? TraceLevel { get; set; }
    }
}
