using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace ConnectUO.Framework.Diagnostics
{
    public sealed class FileTraceListener : TraceListener
    {
        private static Dictionary<string, object> _lockTable = new Dictionary<string, object>();
        private string _filename;

        public FileTraceListener(string filename)
        {
            _filename = filename;

            object syncRoot;

            if (!_lockTable.TryGetValue(filename, out syncRoot))
            {
                syncRoot = new object();
                _lockTable.Add(filename, syncRoot);
                OnTraceReceived(new TraceMessage(TraceLevels.Verbose, DateTime.UtcNow, "Logging Started", 
                    string.IsNullOrEmpty(Thread.CurrentThread.Name) ? Thread.CurrentThread.ManagedThreadId.ToString() : Thread.CurrentThread.Name));
            }
        }

        protected override void OnTraceReceived(TraceMessage message)
        {
            try
            {
                object syncRoot = _lockTable[_filename];

                lock (syncRoot)
                {
                    using (FileStream fileStream = new FileStream(_filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                    {
                        fileStream.Seek(fileStream.Length, SeekOrigin.Begin);

                        using(StreamWriter writer = new StreamWriter(fileStream))
                        {
                            writer.WriteLine(message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Dispose(true);
                Tracer.Error(e);
            }
        }
    }
}
