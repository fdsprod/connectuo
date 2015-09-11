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
    public partial class OptionsGroupControl : UserControl
    {
        public event EventHandler SettingsClick
        {
            add { btnSettings.Click += value; }
            remove { btnSettings.Click -= value; }
        }

        public event EventHandler AboutClick
        {
            add { btnAbout.Click += value; }
            remove { btnAbout.Click -= value; }
        }

        public OptionsGroupControl()
        {
            InitializeComponent();
        }

        public void SelectNone()
        {
            btnAbout.Selected = false;
            btnSettings.Selected = false;
            btnAbout.Invalidate();
            btnSettings.Invalidate();
        }

        public void SelectSettings()
        {
            btnSettings.Select();
        }

        public void SelectAbout()
        {
            btnAbout.Select();
        }
    }
}
