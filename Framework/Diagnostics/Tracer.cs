using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConnectUO.Framework.Configuration;
using System.Configuration;
using ConnectUO.Framework.Extensions;

namespace ConnectUO.Framework.Diagnostics
{
    public delegate void TraceReveivedHandler(TraceMessageEventArgs e);

    public static class Tracer
    {
        private static object _syncRoot = new object();
        private static TraceLevels _traceLevel;

        public static TraceLevels TraceLevel
        {
            get { return Tracer._traceLevel; }
            set { Tracer._traceLevel = value; }
        }

        public static TraceReveivedHandler TraceReceived;

        static Tracer()
        {
            TracerConfigurationSection config = ConfigurationManager.GetSection("traceListeners") as TracerConfigurationSection;

            if (config == null)
                return;

            foreach (TracerElement tracer in config.Tracers)
            {
                Type type = Type.GetType(tracer.Type);

                if (type == null)
                {
                    Tracer.Warn("Unable to resolve type \"{0}\"", tracer.Type);
                    continue;
                }

                TraceLevels traceLevel = TraceLevels.Verbose;

                if(!string.IsNullOrEmpty(tracer.TraceLevel))
                {
                    if (!Enum.IsDefined(typeof(TraceLevels), tracer.TraceLevel))
                        Tracer.Warn("Unable to resolve Trace Level \"{0}\"", tracer.TraceLevel);
                    else
                        traceLevel = (TraceLevels)Enum.Parse(typeof(TraceLevels), tracer.TraceLevel);
                }

                ITraceListener listener = Activator.CreateInstance(type) as ITraceListener;

                if (listener != null)
                {
                    listener.TraceLevel = traceLevel;
                }
            }
        }

        public static void Verbose(object obj)
        {
            Trace(TraceLevels.Verbose, obj.ToString());
        }

        public static void Verbose(string message, params object[] args)
        {
            Trace(TraceLevels.Verbose, message, args);
        }

        public static void Info(object obj)
        {
            Trace(TraceLevels.Info, obj.ToString());
        }

        public static void Info(string message, params object[] args)
        {
            Trace(TraceLevels.Info, message, args);
        }

        public static void Warn(object obj)
        {
            Trace(TraceLevels.Warning, obj.ToString());
        }

        public static void Warn(string message, params object[] args)
        {
            Trace(TraceLevels.Warning, message, args);
        }

        public static void Error(object obj)
        {
            Trace(TraceLevels.Error, obj.ToString());
        }

        public static void Error(string message, params object[] args)
        {
            Trace(TraceLevels.Error, message, args);
        }

        public static void Fatal(object obj)
        {
            Trace(TraceLevels.Fatal, obj.ToString());
        }

        public static void Fatal(string message, params object[] args)
        {
            Trace(TraceLevels.Fatal, message, args);
        }

        public static void Trace(TraceLevels type, string message, params object[] args)
        {
            lock (_syncRoot)
            {
                if (TraceReceived != null)
                {
                    if(args.Length > 0)
                    {
                        message = string.Format(message, args);
                    }

                    TraceMessage traceMessage =
                        new TraceMessage(type, DateTime.UtcNow, message, 
                            string.IsNullOrEmpty(Thread.CurrentThread.Name) ? Thread.CurrentThread.ManagedThreadId.ToString() : Thread.CurrentThread.Name);

                    TraceReceived(new TraceMessageEventArgs(traceMessage));
                }
            }
        }
    }
}
