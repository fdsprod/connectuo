using System.Timers;
using System.Windows.Forms;
using SevenZip;
using System;
using System.IO;
using System.Data;
using ConnectUO.Framework.Web;
using ConnectUO.Framework;
using ConnectUO.Framework.Diagnostics;
using ConnectUO.Framework.IO;
using System.Diagnostics;

namespace Update
{
    public partial class frmUpdate : Form
    {

        private int _timeoutCount = 10;
        private System.Windows.Forms.Timer _closeTimer;
        private System.Windows.Forms.Timer _delayInitTimer = new System.Windows.Forms.Timer();
        private ConnectUO.Framework.Web.ConnectUOWebService _service;
        private SevenZipExtractor _extractor;
        private ConnectUO.Framework.Web.HttpDownload _update;
        private string _destinationDirectory;
        private string _destinationFile;

        public frmUpdate()
        {
            InitializeComponent();
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Status: Retrieving version information...";

            _delayInitTimer = new System.Windows.Forms.Timer();
            _delayInitTimer.Tick += new EventHandler(delayInitTimer_Tick);
            _delayInitTimer.Interval = 100;
            _delayInitTimer.Enabled = true;
            _delayInitTimer.Start();
        }

        private void delayInitTimer_Tick(object sender, EventArgs e)
        {
            _delayInitTimer.Stop();

            _destinationDirectory = Path.Combine(Program.ApplicationInfo.BaseDirectory, "update");
            _destinationFile = Path.Combine(_destinationDirectory, "update.zip");

            _closeTimer = new System.Windows.Forms.Timer();
            _closeTimer.Interval = 1000;
            _closeTimer.Tick += new EventHandler(closeTimer_Tick);

            _service = new ConnectUO.Framework.Web.ConnectUOWebService();
            _service.GetLatestVersionCompleted += new ConnectUO.Framework.Web.GetLatestVersionCompletedEventHandler(service_GetLatestVersionCompleted);

            _service.GetLatestVersionAsync(this);
        }

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            _timeoutCount--;

            if(_timeoutCount <= 0)
            {
                Close();
            }
            else
            {
                lblStatus.Text = string.Format("Status: Update Complete. (ConnectUO will restart in {0} seconds)", _timeoutCount);
            }
        }

        private void service_GetLatestVersionCompleted(object sender, ConnectUO.Framework.Web.GetLatestVersionCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;

            Invoke((MethodInvoker)delegate
            {
                if (e.Error != null)
                {
                    Program.Status = UpdateStatus.Failure;
                    txtChangeLog.Text = string.Format("The following error occured while trying to get the lastest ConnectUO version information: \n {0}", e.Error.Message);
                    Complete();
                    return;
                }

                string url = e.Result.Tables[0].Rows[0].Field<string>("UpdateUrl");
                string changeLog = e.Result.Tables[0].Rows[0].Field<string>("ChangeLog");

                txtChangeLog.Text = changeLog;
                txtChangeLog.Focus();
                txtChangeLog.Select(0, 0);

                FileSystemHelper.EnsureDirectoryExists(_destinationDirectory);

                DirectoryInfo dir = new DirectoryInfo(_destinationDirectory);
                FileInfo[] files = dir.GetFiles();

                for (int i = 0; i < files.Length; i++)
                {
                    Tracer.Verbose("Removing old file {0}", files[i].Name);

                    try
                    {
                        files[i].Delete();
                    }
                    catch
                    {
                    }
                }

                _update = HttpDownload.CreateDownload(url, _destinationFile);

                _update.ProgressStarted += new EventHandler<ProgressStartedEventArgs>(update_ProgressStarted);
                _update.ProgressUpdate += new EventHandler<ProgressUpdateEventArgs>(update_ProgressUpdate);
                _update.ProgressCompleted += new EventHandler<ProgressCompletedEventArgs>(update_ProgressCompleted);

                _update.Begin();
            });
        }

        private void update_ProgressCompleted(object sender, ProgressCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;

            Invoke((MethodInvoker)delegate
            {
                if (e.ExceptionObject != null)
                {
                    Program.Status = UpdateStatus.Failure;
                    txtChangeLog.Text = string.Format("The following error occured while trying to get the lastest ConnectUO version information: \n {0}", ((Exception)e.ExceptionObject).Message);
                    Complete();
                    return;
                }

                pbProgress.Value = 100;
                lblStatus.Text = string.Format("Status: Extracting...");

                System.Threading.ThreadPool.QueueUserWorkItem(delegate(object o)
                {
                    _extractor = new SevenZipExtractor(_destinationFile);

                    _extractor.Extracting += new EventHandler<SevenZip.ProgressEventArgs>(extractor_Extracting);
                    _extractor.ExtractionFinished += new EventHandler(extractor_ExtractionFinished);
                    _extractor.ExtractArchive(_destinationDirectory);
                    _extractor.Dispose();
                });
            });

        }

        private void extractor_ExtractionFinished(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                System.Threading.ThreadPool.QueueUserWorkItem(delegate(object o)
                {
                    try
                    {
                        if (Directory.Exists(_destinationDirectory))
                        {
                            File.Delete(_destinationFile);

                            DirectoryInfo dir = new DirectoryInfo(_destinationDirectory);
                            FileInfo[] files = dir.GetFiles();

                            if (files.Length > 0)
                            {
                                for (int i = 0; i < files.Length; i++)
                                {
                                    string filename = files[i].Name;

                                    string copyToPath = Path.Combine(Program.ApplicationInfo.BaseDirectory, filename);

                                    if (File.Exists(copyToPath) && !(filename.ToLower() == "data.jab"))
                                    {
                                        FileInfo existingFile = new FileInfo(copyToPath);
                                        Tracer.Verbose("Backing up file {0}", existingFile.Name);

                                        string bak = copyToPath + ".bak";

                                        if (File.Exists(bak))
                                        {
                                            File.Delete(bak);
                                        }

                                        existingFile.MoveTo(bak);
                                    }

                                    if (!File.Exists(copyToPath))
                                    {
                                        Tracer.Verbose("Copying updated file to {0}", copyToPath);
                                        files[i].CopyTo(copyToPath);

                                        Tracer.Verbose("Deleting {0}", files[i].Name);
                                        files[i].Delete();
                                    }
                                }
                            }
                        }

                        Program.Status = UpdateStatus.Success;
                    }
                    catch (Exception ex)
                    {
                        Program.Status = UpdateStatus.Failure;
                        Tracer.Fatal(ex.ToString());
                    }

                    Complete();
                });
            });
        }

        private void extractor_Extracting(object sender, SevenZip.ProgressEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                pbProgress.Value = e.PercentDone;
                lblStatus.Text = "Status: Extracting...";
            });
        }

        private void update_ProgressUpdate(object sender, ProgressUpdateEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                pbProgress.Value = e.ProgressPercentage;
                lblStatus.Text = string.Format("Status: Downloading {0} / {1}...", e.Current, e.Max);
            });
        }

        private void update_ProgressStarted(object sender, ProgressStartedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                pbProgress.Value = 0;
            });
        }

        private void frmUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((Program.Status == UpdateStatus.Incomplete) &&
                (MessageBox.Show(this, "The update has not completed!\nAre you sure you want to cancel the update?", "Cancel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
            {
                e.Cancel = true;
                return;
            }

            _update.End();
            _closeTimer.Stop();
            _service.CancelAsync(this);
        }

        private void Complete()
        {
            Invoke((MethodInvoker)delegate
            {
                _closeTimer.Start();

                lblStatus.Text = "Update complete.";
                btnClose.Text = "&Close";
                btnClose.Enabled = true;
            });
        }

        private void txtChangeLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
