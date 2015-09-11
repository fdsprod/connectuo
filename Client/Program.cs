using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ConnectUO.Data;
using ConnectUO.Forms;
using ConnectUO.Framework.Web;
using ConnectUO.Framework;
using ConnectUO.Framework.Diagnostics;
using System.IO;
using Microsoft.Win32;
using ConnectUO.Framework.Data;
using ConnectUO.Framework.Windows.Forms;
using ConnectUO.Framework.Services;
using ConnectUO.Services;
using Ninject;
using ConnectUO.Framework.Debug;
using ConnectUO.Controls;

namespace ConnectUO
{
    static class Program
    {
        const string MutexName = "ConnectUO 2.0";

        static IKernel _kernel = new StandardKernel();
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _kernel.Load(AppDomain.CurrentDomain.GetAssemblies());

            StartupArgumentFlagCollection startupFlags = new StartupArgumentFlagCollection();
            IApplicationService applicationService = _kernel.Get<IApplicationService>();
            ApplicationInfo applicationInfo = new ApplicationInfo(Assembly.GetEntryAssembly(), args, startupFlags);

            Directory.SetCurrentDirectory(applicationInfo.BaseDirectory);

            applicationService.ApplicationInfo = applicationInfo;
            //applicationService.TraceListeners.Add(new CrashLogTraceListener());

            if (applicationService.Debug)
            {
                applicationService.TraceListeners.Add(new FileTraceListener(Path.Combine(applicationInfo.BaseDirectory, "debug.log")));
            }

            EnsureDotNet35SP1();

            CuoUri.Initialize(applicationService);

            if (ContainsCuoProtocol(args))
            {
                return;
            }

            using (Mutex mutex = new Mutex(false, MutexName))
            {
                Tracer.Verbose("Entering Mutex {0}", MutexName);

                if (!mutex.WaitOne(1, true))
                {
                    MessageBoxEx.Show(null, "ConnectUO is already running.");
                    return;
                }

                SplashScreen splashScreen = _kernel.Get<SplashScreen>();
                ShellForm mainForm = _kernel.Get<IShell>() as ShellForm;

                applicationService.MainForm = mainForm;

                Application.Run(splashScreen);
                Application.Run(mainForm);
            }
        }

        private static void EnsureDotNet35SP1()
        {
            string Fx35RegistryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5";
            object Fx35ServicePack = Registry.GetValue(Fx35RegistryKey, "SP", null);

            if (Fx35ServicePack == null || (int)Fx35ServicePack < 1)
            {
                Tracer.Warn(".Net 3.5 SP1 was not found and is required for ConnectUO to run.");

                if (MessageBox.Show("It appears you are missing .Net 3.5 SP1.  ConnectUO cannot continue until this is installed.  Would you like to goto the download page now?", "ConnectUO 2.0", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start("http://www.microsoft.com/downloads/details.aspx?FamilyID=AB99342F-5D1A-413D-8319-81DA479AB0D7&displaylang=en");
                }

                return;
            }
        }
        
        /// <summary>
        /// Processes the args passed to the application and returns true if the 
        /// args contained the proper string to launch the client
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static bool ContainsCuoProtocol(string[] args)
        {
            Tracer.Verbose("Checking args...");

            for (int i = 0; i < args.Length; i++)
            {
                Tracer.Verbose("args[{0}] = {1}", i, args[i]);

                if (args[i].Contains("cuo://"))
                {
                    string url = args[i].Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries)[1];

                    Tracer.Verbose("Found it = {0}", url);

                    _kernel.Get<CuoUri>().Play(args[i]);

                    return true;                       
                }
            }

            return false;
        }
    }
}
