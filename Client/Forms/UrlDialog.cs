using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConnectUO.Framework;
using ConnectUO.Framework.Windows.Forms;
using System.Text.RegularExpressions;

namespace ConnectUO.Forms
{
    public partial class UrlDialog : Form
    {
        public string Url
        {
            get { return txtUrl.Text; }
            set { txtUrl.Text = value; }
        }

        public UrlDialog()
        {
            InitializeComponent();
        }

        private void frmUrl_Load(object sender, EventArgs e)
        {
            txtUrl.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsValidUrl(Url))
            {
                MessageBoxEx.Show(this, "That is not a valid url.", "ConnectUO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        public static bool IsValidUrl(string url)
        {
            string pattern = @"^(http)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }

    }
}
