using System;
using System.Windows.Forms;
using ConnectUO.Data;
using ConnectUO.Framework.Extensions;
using ConnectUO.Framework.Data;
using ConnectUO.Framework.Windows.Forms;
using ConnectUO.Framework;
using ConnectUO.Framework.Services;
using Ninject;
using ConnectUO.Forms;

namespace ConnectUO.Controls
{
    public partial class EditLocalServerControl : BaseConnectUOPanel
    {
        private const string URL_TAG = "url";
        private const string FILE_TAG = "file";

        private Server _server;
        private IKernel _kernel;
        private IStorageService _storageService;

        public Server Server
        {
            get { return _server; }
            set
            {
                if (_server != value)
                {
                    _server = value;
                    OnShardChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> ShardEditAddComplete;

        [Inject]
        public EditLocalServerControl(IKernel kernel)
        {
            InitializeComponent();

            _kernel = kernel;
            _storageService = _kernel.Get<IStorageService>();
        }

        protected virtual void OnShardChanged(object sender, EventArgs e)
        {
            if(_server != null)
            {
                txtServerName.Text = _server.Name;
                txtDescription.Text = _server.Description;
                txtHostAddress.Text = _server.HostAddress;
                txtPort.Text = _server.Port.ToString();
                chkRemoveEnc.Checked = _server.RemoveEncryption;

                LocalPatch[] patches = _storageService.GetLocalPatches((int)_server.Id);

                for (int i = 0; i < patches.Length; i++)
                {
                    lstPatches.Items.Add(new ListViewItem(new string[] { patches[i].PatchUrl, patches[i].Version.ToString() }));
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtHostAddress.Text.Length <= 0)
            {
                MessageBoxEx.Show(this, "You must specify a host address.");
                return;
            }

            int port = 0;
            string portString = txtPort.Text;

            if (portString.Length <= 0 || !int.TryParse(portString, out port))
            {
                MessageBoxEx.Show(this, "You must specify a valid port.");
                return;
            }

            if (txtServerName.Text.Length <= 0)
            {
                MessageBoxEx.Show(this, "You must specify a server name.");
                return;
            }

            bool allPatchesAreValid = true;

            for (int i = 0; i < lstPatches.Items.Count && allPatchesAreValid; i++)
            {
                allPatchesAreValid = (lstPatches.Items[i].SubItems[0].Text.Length > 0);
            }

            if (!allPatchesAreValid)
            {
                MessageBoxEx.Show(this, "One or more pathes contain invlid paths.");
                return;
            }

            Server.Name = txtServerName.Text;
            Server.HostAddress = txtHostAddress.Text;
            Server.Port = port;
            Server.RemoveEncryption = chkRemoveEnc.Checked;
            Server.HasPatches = lstPatches.Items.Count > 0;
            Server.Description = txtDescription.Text;

            _storageService.DeleteLocalPatches((int)Server.Id);

            for (int i = 0; i < lstPatches.Items.Count; i++)
            {
                _storageService.AddLocalPatch(
                    (int)Server.Id,
                    lstPatches.Items[i].SubItems[0].Text,
                    lstPatches.Items[i].SubItems[1].Text.ConvertTo<int>());
            }

            OnShardEditAddComplete(this, EventArgs.Empty);
        }

        protected void OnShardEditAddComplete(object sender, EventArgs e)
        {
            if (ShardEditAddComplete != null)
                ShardEditAddComplete(sender, e);
        }
                        
        private void lstPatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemovePatch.Enabled = lstPatches.SelectedIndices.Count > 0;
            btnIncreaseVersion.Enabled = lstPatches.SelectedIndices.Count > 0;
        }
        
        private void lstPatches_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstPatches.SelectedItems.Count <= 0)
                return;

            ListViewItem item = lstPatches.SelectedItems[0];

            string fileOrUrl = item.SubItems[0].Text;

            if (item.Tag.ToString() == FILE_TAG)
            {
                openFileDialog.FileName = fileOrUrl;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileOrUrl = openFileDialog.FileName;
                }
            }
            else if (item.Tag.ToString() == URL_TAG)
            {                
                using (UrlDialog form = new UrlDialog())
                {
                    form.Url = fileOrUrl;

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        fileOrUrl = form.Url;
                    }
                }
            }
            else { }


            item.SubItems[0].Text = fileOrUrl;
        }
        
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem(new string[] { openFileDialog.FileName, "1" });
                item.Tag = FILE_TAG;
                lstPatches.Items.Add(item);
            } 
        }

        private void btnAddUrl_Click(object sender, EventArgs e)
        {
            using (UrlDialog form = new UrlDialog())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem item = new ListViewItem(new string[] { form.Url, "1" });
                    item.Tag = URL_TAG;
                    lstPatches.Items.Add(item);
                }
            }
        }

        private void btnRemovePatch_Click_1(object sender, EventArgs e)
        {
            if (lstPatches.SelectedItems.Count > 0)
            {
                ListViewItem item = lstPatches.SelectedItems[0];
                lstPatches.Items.Remove(item);
            }
        }

        private void btnIncreaseVersion_Click(object sender, EventArgs e)
        {
            if (lstPatches.SelectedItems.Count > 0)
            {
                ListViewItem item = lstPatches.SelectedItems[0];

                int version = int.Parse(item.SubItems[1].Text);
                version++;

                item.SubItems[1].Text = version.ToString();
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (lstPatches.Items.Count <= 0)
                return;

            if (MessageBoxEx.Show(this, "Are you sure you want to delete all the patches?", "ConnectUO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lstPatches.Items.Clear();
            }
        }
    }
}
