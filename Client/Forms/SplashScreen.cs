using System;
using System.IO;
using System.Windows.Forms;
using ConnectUO.Data;
using ConnectUO.Framework.Data;
using ConnectUO.Framework.Web;
using ConnectUO.Framework;
using ConnectUO.Framework.Diagnostics;
using ConnectUO.Framework.Services;
using Ninject;

namespace ConnectUO.Forms
{
    public sealed partial class SplashScreen : Form
    {
        private Timer _closeTimer = new Timer();
        private Timer _delayInitTimer = new Timer();
        private bool _finishedCheckForUpdate;
        private CallbackState state;
        private IApplicationService _applicationService;
        private IStorageService _storageService;
        private IKernel _kernel;

        [Inject]
        public SplashScreen(IKernel kernel)
        {
            InitializeComponent();

            _kernel = kernel;
            _applicationService = kernel.Get<IApplicationService>();
            _storageService = kernel.Get<IStorageService>();
        }

        void Database_Error(object sender, StorageSerErrorEventArgs e)
        {
            Tracer.Fatal(e.Exception);
        }

        void OnCheckForUpdatesComplete(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate()
            {
                lblStatus.Text = "ConnectUO is up to date.";
            });

            _finishedCheckForUpdate = true;
        }

        void t_Tick(object sender, EventArgs e)
        {
            if (_finishedCheckForUpdate)
            {
                _closeTimer.Stop();
                Close();
            }

            _closeTimer.Interval = 1000;
        }

        private void donationButton1_Click(object sender, EventArgs e)
        {
            _closeTimer.Stop();
        }

        private void donationButton1_DonationMessageBoxClosed(object sender, EventArgs e)
        {
            _storageService.CancelServiceCall(state);
            _closeTimer.Stop();
            Close();
        }

        private void frmSplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            _storageService.Error -= new EventHandler<StorageSerErrorEventArgs>(Database_Error);
        }

        private void frmSplashScreen_Load(object sender, EventArgs e)
        {
            lblVersion.Text = _applicationService.FormattedVersionString;
            lblStatus.Text = "Checking for ConnectUO updates...";

            _delayInitTimer = new Timer();
            _delayInitTimer.Tick += new EventHandler(delayInitTimer_Tick);
            _delayInitTimer.Interval = 100;
            _delayInitTimer.Enabled = true;
            _delayInitTimer.Start();
        }

        void delayInitTimer_Tick(object sender, EventArgs e)
        {
            _delayInitTimer.Stop();

            try
            {
                DirectoryInfo dir = new DirectoryInfo(_applicationService.ApplicationInfo.BaseDirectory);
                FileInfo[] files = dir.GetFiles("*.bak");

                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        Tracer.Verbose("Deleting backed up file {0}", files[i].Name);
                        files[i].Delete();
                    }
                    catch
                    {

                    }
                }

                state = new CallbackState(OnCheckForUpdatesComplete);
                _storageService.Error += new EventHandler<StorageSerErrorEventArgs>(Database_Error);

                try
                {
#if !DEV
                    new ConnectUOWebService().UpdateVersionStatsAsync(_storageService.Guid, _applicationService.ApplicationInfo.Version.ToString());
#endif
                }
                catch (Exception ex)
                {
                    Tracer.Error(ex);
                }
                try
                {
                    _storageService.CheckForUpdates(state);
                }
                catch (Exception ex)
                {
                    Invoke((MethodInvoker)delegate()
                    {
                        lblStatus.Text = "Unable to contact webservice...";
                    });

                    Tracer.Error(ex);
                    _finishedCheckForUpdate = true;
                }

                _closeTimer.Interval = 3000;
                _closeTimer.Tick += new EventHandler(t_Tick);
                _closeTimer.Start();
            }
            catch (Exception ex)
            {
                Tracer.Fatal(ex);
            }
        }
    }
}
