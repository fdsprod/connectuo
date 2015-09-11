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
    public partial class ConnectionGroupControl : UserControl
    {
        public event EventHandler UpdateServersClick
        {
            add { btnUpdateServerlists.Click += value; }
            remove { btnUpdateServerlists.Click -= value; }
        }

        public ConnectionGroupControl()
        {
            InitializeComponent();
        }
    }
}
