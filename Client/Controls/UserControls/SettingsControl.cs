using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ninject;
using ConnectUO.Framework.Services;
using ConnectUO.Framework.Windows.Forms;
using System.IO;

namespace ConnectUO.Controls
{
    public partial class SettingsControl : BaseConnectUOPanel
    {
        ISettingsService _settingsService;

        [Inject]
        public SettingsControl(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            chkLaunchRazor.Checked = _settingsService.LaunchRazor;
            chkRemoveEncrpyption.Checked = _settingsService.RemoveEncryption;
            chkLaunchRazor.Enabled = _settingsService.IsRazorInstalled;
            chkMinimizeOnPlay.Checked = _settingsService.MinimizeOnPlay;

            txtClientPath.Text =
                string.IsNullOrEmpty(_settingsService.UltimaOnlineDirectory)
                    ? "Not Found"
                    : _settingsService.UltimaOnlineDirectory;

            txtPatchDirectory.Text = _settingsService.PatchDirectory;
        }

        private void chkMinimizeOnPlay_CheckedChanged(object sender, EventArgs e)
        {
            _settingsService.MinimizeOnPlay = chkMinimizeOnPlay.Checked;
        }

        private void btnDefaultPatchDirectory_Click(object sender, EventArgs e)
        {
            txtPatchDirectory.Text = _settingsService.PatchDirectory = string.Empty;
        }

        private void chkRemoveEncrpyption_CheckedChanged(object sender, EventArgs e)
        {
            _settingsService.RemoveEncryption = chkRemoveEncrpyption.Checked;
        }

        private void chkLaunchRazor_CheckedChanged(object sender, EventArgs e)
        {
            _settingsService.LaunchRazor = chkLaunchRazor.Checked;
        }

        private void btnSetPatchDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = _settingsService.PatchDirectory;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtPatchDirectory.Text = _settingsService.PatchDirectory = dialog.SelectedPath;
                }
            }
        }
        
        private void btnSetManually_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Ultima Online Client|client.exe";
                dialog.Title = "Location the Ultima Online client.exe";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string found = dialog.FileName;
                    string filename = Path.GetFileName(found);
                    string directory = Path.GetDirectoryName(found);

                    _settingsService.UltimaOnlineDirectory = directory;
                    _settingsService.UltimaOnlineExe = filename;

                    txtClientPath.Text =
                        string.IsNullOrEmpty(_settingsService.UltimaOnlineDirectory)
                            ? "Not Found"
                            : _settingsService.UltimaOnlineDirectory;
                }
            }
        }

        private void btnAutoDetectClient_Click(object sender, EventArgs e)
        {
            _settingsService.AutoDetectUltimaOnlineDirectory();
            _settingsService.UltimaOnlineExe = "client.exe";

            if (string.IsNullOrEmpty(_settingsService.UltimaOnlineDirectory))
            {
                MessageBoxEx.Show(this, "ConnectUO was unable to detect a valid installation of Ultima Online.  Please re-install Ultima Online or set the path manually.");
            }

            txtClientPath.Text =
                string.IsNullOrEmpty(_settingsService.UltimaOnlineDirectory)
                    ? "Not Found"
                    : _settingsService.UltimaOnlineDirectory;
        }

        private void btnSetManually_Click_1(object sender, EventArgs e)
        {

        }
    }
}
