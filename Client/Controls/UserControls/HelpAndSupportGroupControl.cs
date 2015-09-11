using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConnectUO.Controls
{
    public partial class HelpAndSupportGroupControl : UserControl
    {
        public event EventHandler ClientHelpClick
        {
            add { btnClientHelp.Click += value; }
            remove { btnClientHelp.Click -= value; }
        }

        public event EventHandler FAQClick
        {
            add { btnFAQ.Click += value; }
            remove { btnFAQ.Click -= value; }
        }

        public event EventHandler ReportBugClick
        {
            add { btnReportABug.Click += value; }
            remove { btnReportABug.Click -= value; }
        }

        public event EventHandler ServerHelpClick
        {
            add { btnServerHelp.Click += value; }
            remove { btnServerHelp.Click -= value; }
        }

        public HelpAndSupportGroupControl()
        {
            InitializeComponent();
        }
    }
}
