using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ConnectUO.Data;
using ConnectUO.Framework.Patching;
using ConnectUO.Framework;
using ConnectUO.Framework.Data;
using ConnectUO.Framework.Windows.Forms;
using ConnectUO.Web;
using ConnectUO.IO.Compression;
using ConnectUO.Framework.Services;
using Ninject;

namespace ConnectUO.Forms
{
    public partial class PatcherForm : Form
    {
        IStorageService _storageService;

        ServerPatch[] _patches;
        string _serverDirectory;

        DownloadManager _downloadManager;
        ExtractionManager _extractionManager;
        PatchManager _patchManager;

        Dictionary<DownloadTask, ListViewItem> _downloadTable;
        Dictionary<ExtractionTask, ListViewItem> _extractionTable;
        Dictionary<PatchingTask, ListViewItem> _patchTable;

        List<string> _failedPatchFiles = new List<string>();
        List<string> _errorMessages = new List<string>();

        int _extractionsComplete = 0;
        int _patchesComplete = 0;
        int _patchCount = 0;

        public ServerPatch[] Patches
        {
            get { return _patches; }
            set { _patches = value; }
        }

        public string ServerDirectory
        {
            get { return _serverDirectory; }
            set { _serverDirectory = value; }
        }

        [Inject]
        public PatcherForm(IStorageService storageService)
        {
            _storageService = storageService;

            _downloadManager = new DownloadManager();
            _extractionManager = new ExtractionManager();
            _patchManager = new PatchManager();

            _downloadTable = new Dictionary<DownloadTask, ListViewItem>();
            _extractionTable = new Dictionary<ExtractionTask, ListViewItem>();
            _patchTable = new Dictionary<PatchingTask, ListViewItem>();

            InitializeComponent();
        }

        ~PatcherForm()
        {
            foreach (DownloadTask task in _downloadTable.Keys)
            {
                task.ProgressUpdate -= new EventHandler<ProgressUpdateEventArgs>(OnDownloadStateProgressUpdate);
                task.ProgressCompleted -= new EventHandler<ProgressCompletedEventArgs>(OnDownloadStateProgressCompleted);
            }

            foreach (ExtractionTask task in _extractionTable.Keys)
            {
                task.ProgressUpdate -= new EventHandler<ProgressUpdateEventArgs>(OnExtractionProgressUpdate);
                task.ProgressCompleted -= new EventHandler<ProgressCompletedEventArgs>(OnExtractionProgressCompleted);
            }

            foreach (PatchingTask task in _patchTable.Keys)
            {
                task.ProgressUpdate -= new EventHandler<ProgressUpdateEventArgs>(OnPatchingProgressUpdate);
                task.ProgressCompleted -= new EventHandler<ProgressCompletedEventArgs>(OnPatchingProgressCompleted);
            }
        }

        private void frmDownloads_Load(object sender, EventArgs e)
        {
            InitializeDownloadTasks();

            _downloadManager.Begin();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            _downloadManager.CancleAll();
            _extractionManager.CancleAll();
            _patchManager.CancleAll();
        }

        private void InitializeDownloadTasks()
        {
            for (int i = 0; i < _patches.Length; i++)
            {
                ServerPatch patch = _patches[i];

                string url = patch.PatchUrl;
                int version = patch.Version;

                string filename = Path.GetFileName(url);
                string destination = Path.Combine(_serverDirectory, filename);

                DownloadTask task = new DownloadTask(_patches[i].PatchUrl, destination);
                ListViewItem item = new ListViewItem(new string[5]);

                task.ProgressUpdate += new EventHandler<ProgressUpdateEventArgs>(OnDownloadStateProgressUpdate);
                task.ProgressCompleted += new EventHandler<ProgressCompletedEventArgs>(OnDownloadStateProgressCompleted);
                
                _downloadManager.Queue(task);
                _downloadTable.Add(task, item);

                lvDownloads.Items.Add(item);

                UpdateDownloadStatus(task, item, 0, -1, 0, "Download Queued");
            }
        }
        
        private void InitializeExtractionTask(DownloadTask state)
        {
            string filename = Path.GetFileName(state.Url);
            string archive = state.Destination;

            ListViewItem item = _downloadTable[state];
            ExtractionTask task = new ExtractionTask(state.Destination, Path.GetDirectoryName(state.Destination));

            task.ProgressUpdate += new EventHandler<ProgressUpdateEventArgs>(OnExtractionProgressUpdate);
            task.ProgressCompleted += new EventHandler<ProgressCompletedEventArgs>(OnExtractionProgressCompleted);

            Invoke((MethodInvoker)delegate()
            {
                UpdateExtractionStatus(task, item, 0, 100, 0, "Extraction Queued");
            });

            _extractionTable.Add(task, item);
            _extractionManager.Queue(task);

            if (!_extractionManager.HasBegun)
            {
                _extractionManager.Begin();
            }
        }

        private void InitializePatchingProcess()
        {
            Invoke((MethodInvoker)delegate()
            {
                lblStatus.Text = "Status: Gathering patching information...";
            });

            DirectoryInfo directory = new DirectoryInfo(_serverDirectory);
            List<FileInfo> files = new List<FileInfo>();

            FileInfo[] muoFiles = directory.GetFiles("*.muo");
            FileInfo[] uopFiles = directory.GetFiles("*.uop");
            FileInfo[] mulFiles = directory.GetFiles("verdata.mul");

            List<FileInfo> patchFiles = new List<FileInfo>();

            patchFiles.AddRange(muoFiles);
            patchFiles.AddRange(uopFiles);
            patchFiles.AddRange(mulFiles);

            List<Patch> patches = new List<Patch>();

            for (int i = 0; i < patchFiles.Count; i++)
            {
                PatchReader reader = new PatchReader(
                    File.OpenRead(patchFiles[i].FullName), PatchReader.ExtensionToPatchFileType(patchFiles[i].FullName));

                patches.AddRange(reader.ReadPatches());
                reader.Close();
            }

            for (int i = 0; i < muoFiles.Length; i++)
                muoFiles[i].Delete();
            for (int i = 0; i < uopFiles.Length; i++)
                uopFiles[i].Delete();
            for (int i = 0; i < mulFiles.Length; i++)
                mulFiles[i].Delete();

            if (patches.Count <= 0)
            {
                Complete();
                return;
            }
            
            Dictionary<int, List<Patch>> typedPatchTable = new Dictionary<int, List<Patch>>();

            for (int i = 0; i < patches.Count; i++)
            {
                if (!typedPatchTable.ContainsKey(patches[i].FileId))
                    typedPatchTable.Add(patches[i].FileId, new List<Patch>());

                typedPatchTable[patches[i].FileId].Add(patches[i]);
            }

            Invoke((MethodInvoker)delegate()
            {
                lvDownloads.Items.Clear();

                columnHeader1.Text = "Mul File";
                columnHeader2.Text = "Total";
                columnHeader3.Text = "Completed";
                columnHeader4.Text = "Progress";
                columnHeader7.Text = "Status";
            });

            List<int> keys = new List<int>(typedPatchTable.Keys);

            _patchCount = keys.Count;

            for (int i = 0; i < keys.Count; i++)
            {
                int key = keys[i];
                patches = typedPatchTable[keys[i]];

                if(patches.Count > 0)
                {
                    FileId id = (FileId)i;

                    PatchingTask task = new PatchingTask(patches.ToArray(), _serverDirectory, id.ToString().Replace('_', '.'));

                    task.ProgressUpdate += new EventHandler<ProgressUpdateEventArgs>(OnPatchingProgressUpdate);
                    task.ProgressCompleted += new EventHandler<ProgressCompletedEventArgs>(OnPatchingProgressCompleted);

                    ListViewItem item = new ListViewItem(new string[5]);

                    Invoke((MethodInvoker)delegate()
                    {
                        lvDownloads.Items.Add(item);
                        UpdatePatchingStatus(task, item, 0, 100, 0, "Patching Queued");
                    });

                    _patchTable.Add(task, item);
                    _patchManager.Queue(task);

                }
            }

            _patchManager.Begin();
        }

        private void Complete()
        {
            if (_errorMessages.Count <= 0)
            {
                Invoke((MethodInvoker)delegate()
                {
                    lblStatus.Text = "Patching completed.";

                    for (int i = 0; i < _patches.Length; i++)
                    {
                        _storageService.SetPatchApplied(_patches[i]);
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate()
                {
                    btnViewErrors.Enabled = true;
                    lblStatus.Text = "Patching completed with errors.  Play at your own risk...";

                    for (int i = 0; i < _patches.Length; i++)
                    {
                        string filename = Path.GetFileName(_patches[i].PatchUrl);

                        if (!_failedPatchFiles.Contains(filename))
                        {
                            _storageService.SetPatchApplied(_patches[i]);
                        }
                    }

                    btnOK.Enabled = true;
                });
            }
        }

        private void UpdateDownloadStatus(DownloadTask task, ListViewItem item, int current, int total, int completed, string status)
        {
            item.SubItems[0].Text = task.Url;
            item.SubItems[1].Text = total == -1 ? "Unknown" : string.Format("{0:0.00} KB", (float)total / 1024f);
            item.SubItems[2].Text = string.Format("{0:0.00} KB", (float)current / 1024f);
            item.SubItems[3].Text = string.Format("{0:00}%", completed);
            item.SubItems[4].Text = status;
        }

        private void UpdateExtractionStatus(ExtractionTask task, ListViewItem item, int current, int total, int completed, string status)
        {
            item.SubItems[0].Text = task.Archive;
            item.SubItems[1].Text = "100";
            item.SubItems[2].Text = string.Format("{0}", current);
            item.SubItems[3].Text = string.Format("{0:00}%", completed);
            item.SubItems[4].Text = status;
        }

        private void UpdatePatchingStatus(PatchingTask task, ListViewItem item, int current, int total, int completed, string status)
        {
            item.SubItems[0].Text = task.PatchType;
            item.SubItems[1].Text = "100";
            item.SubItems[2].Text = string.Format("{0}", current);
            item.SubItems[3].Text = string.Format("{0:00}%", completed);
            item.SubItems[4].Text = status;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnPatchingProgressCompleted(object sender, ProgressCompletedEventArgs e)
        {
            PatchingTask task = e.State as PatchingTask;

            if (task != null)
            {
                string status = "Patching Complete";

                if (e.ExceptionObject != null)
                {
                    _errorMessages.Add(string.Format("Patching Manager reporting the following error: {0} - {1}", task.PatchType,
                        ((Exception)e.ExceptionObject).Message));

                    status = String.Format("Failed: {0}", ((Exception)e.ExceptionObject).Message);
                }

                Invoke((MethodInvoker)delegate()
                {
                    ListViewItem item = _patchTable[task];
                    UpdatePatchingStatus(task, item, e.Current, e.Max, e.ProgressPercentage, status);
                });

                _patchesComplete++;

                if (_patchesComplete == _patchCount)
                {
                    Complete();
                }
            }
        }

        private void OnPatchingProgressUpdate(object sender, ProgressUpdateEventArgs e)
        {
            PatchingTask task = e.State as PatchingTask;

            if (task != null)
            {
                Invoke((MethodInvoker)delegate()
                {
                    ListViewItem item = _patchTable[task];
                    UpdatePatchingStatus(task, item, e.Current, e.Max, e.ProgressPercentage, "Patching...");
                });

            }
        }

        private void OnExtractionProgressCompleted(object sender, ProgressCompletedEventArgs e)
        {
            ExtractionTask task = e.State as ExtractionTask;

            if (task != null)
            {
                string status = "Extraction Complete";

                if (e.ExceptionObject != null)
                {
                    _errorMessages.Add(string.Format("Extraction Manager reporting the following error: {0} - {1}", task.Archive,
                        ((Exception)e.ExceptionObject).Message));

                    _failedPatchFiles.Add(Path.GetFileName(task.Archive));

                    status = String.Format("Failed: {0}", ((Exception)e.ExceptionObject).Message);
                }

                Invoke((MethodInvoker)delegate()
                {
                    ListViewItem item = _extractionTable[task];
                    UpdateExtractionStatus(task, item, e.Current, e.Max, e.ProgressPercentage, status);
                });

                _extractionsComplete++;

                if (_extractionsComplete == _patches.Length)
                {
                    InitializePatchingProcess();
                }
            }
        }

        private void OnExtractionProgressUpdate(object sender, ProgressUpdateEventArgs e)
        {
            ExtractionTask task = e.State as ExtractionTask;

            if (task != null)
            {
                Invoke((MethodInvoker)delegate()
                {
                    ListViewItem item = _extractionTable[task];
                    UpdateExtractionStatus(task, item, e.Current, e.Max, e.ProgressPercentage, "Extracting...");
                });
            }
        }

        private void OnDownloadStateProgressCompleted(object sender, ProgressCompletedEventArgs e)
        {
            DownloadTask task = e.State as DownloadTask;

            if (task != null)
            {
                string status = "Download Complete";
                bool extract = true;

                if(e.ExceptionObject != null)
                {
                    _errorMessages.Add(string.Format("Download Manager reporting the following error: {0} - {1}", task.Url,
                        ((Exception)e.ExceptionObject).Message));

                    _failedPatchFiles.Add(Path.GetFileName(task.Url));

                    extract = false;
                    status = String.Format("Failed: {0}", ((Exception)e.ExceptionObject).Message);
                }
                
                Invoke((MethodInvoker)delegate()
                {
                    ListViewItem item = _downloadTable[task];
                    UpdateDownloadStatus(task, item, e.Current, e.Max, e.ProgressPercentage, status);
                });

                if (extract)
                {
                    InitializeExtractionTask(task);
                }
            }
        }

        private void OnDownloadStateProgressUpdate(object sender, ProgressUpdateEventArgs e)
        {
            DownloadTask state = e.State as DownloadTask;

            if (state != null)
            {
                Invoke((MethodInvoker)delegate()
                {
                    ListViewItem item = _downloadTable[state];
                    UpdateDownloadStatus(state, item, e.Current, e.Max, e.ProgressPercentage, "Downloading...");
                });

            }
        }

        private void btnViewErrors_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            for(int i = 0 ; i < _errorMessages.Count; i++)
            {
                builder.AppendLine(_errorMessages[i]);
            }

            MessageBoxEx.Show(this, builder.ToString(), "ConnectUO 2.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
