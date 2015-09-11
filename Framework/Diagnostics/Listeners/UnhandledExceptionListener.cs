using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectUO.Framework.Debug;
using System.IO;
using System.Reflection;

namespace ConnectUO.Framework.Diagnostics
{
    public sealed class UnhandledExceptionListener : TraceListener
    {
        private static object _syncLock = new object();

        private static UnhandledExceptionListener _instance;

        public UnhandledExceptionListener()
        {
            Asserter.AssertIsNull(_instance, "UnhandledExceptionListener can only be created 1 time per application domain.");

            _instance = this;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Tracer.Fatal(e.ExceptionObject);

            try
            {
                string timeStamp = GetTimeStamp();
                string fileName = String.Format("Crash {0}.log", timeStamp);

                string root = GetRoot();
                string filePath = Combine(root, fileName);

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    Version version = Assembly.GetEntryAssembly().GetName().Version;

                    writer.WriteLine("Crash Report");
                    writer.WriteLine("===================");
                    writer.WriteLine();
                    writer.WriteLine("Version {0}.{1}, Build {2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
                    writer.WriteLine("Operating System: {0}", Environment.OSVersion);
                    writer.WriteLine(".NET Framework: {0}", Environment.Version);
                    writer.WriteLine("Time: {0}", DateTime.Now);
                    writer.WriteLine();
                    writer.WriteLine("Trace Message:");
                    writer.WriteLine(e.ExceptionObject);
                    writer.WriteLine();
                }
            }
            catch { } //Swallow it.
        }

		private static string Combine( string path1, string path2 )
		{
			if ( path1.Length == 0 )
				return path2;

			return Path.Combine( path1, path2 );
		}

        private static string GetRoot()
        {
            try
            {
                return Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            }
            catch
            {
                return "";
            }
        }

        private static string GetTimeStamp()
        {
            DateTime now = DateTime.Now;

            return String.Format("{0}-{1}-{2}-{3}-{4}-{5}",
                    now.Day,
                    now.Month,
                    now.Year,
                    now.Hour,
                    now.Minute,
                    now.Second
                );
        }

        protected override void OnTraceReceived(TraceMessage message)
        {

        }
    }
}
