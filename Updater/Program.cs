using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using ConnectUO.Framework;
using System.IO;
using ConnectUO.Framework.Diagnostics;

namespace Update
{
    public enum UpdateStatus
    {
        Incomplete,
        Success,
        Failure
    }


    static class Program
    {
        const string MutexName = "ConnectUO 2.0 Updater";

        static ApplicationInfo _applicationInfo;
        static UpdateStatus _status;

        public static UpdateStatus Status
        {
            get { return Program._status; }
            set { Program._status = value; }
        }

        public static ApplicationInfo ApplicationInfo
        {
            get { return Program._applicationInfo; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //new CrashLogTraceListener();

            using (Mutex mutex = new Mutex(false, MutexName))
            {
                if (mutex.WaitOne(1, true) == false)
                {
                    MessageBox.Show("Another instance of ConnectUO 2.0 Updater is running.  Please wait for that instance to close before running the Updater again.", "Already Running");
                    return;
                }

                for (Process[] processArray = Process.GetProcessesByName("ConnectUO.exe"); processArray.Length > 0; processArray = Process.GetProcessesByName("ConnectUO.exe"))
                {
                    Thread.Sleep(50);
                }

                _applicationInfo = new ApplicationInfo();

                Directory.SetCurrentDirectory(_applicationInfo.BaseDirectory);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmUpdate());

                if (_status == UpdateStatus.Success)
                {
                    Process.Start(Path.Combine(_applicationInfo.BaseDirectory, "ConnectUO.exe"));
                }

                _applicationInfo.Process.Kill();
            }
        }
    }
}
