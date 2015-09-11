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
    public partial class ServerListsGroupControl : UserControl
    {
        public event EventHandler PublicServersClick
        {
            add { btnPublicServers.Click += value; }
            remove { btnPublicServers.Click -= value; }
        }

        public event EventHandler FavoriteServersClick
        {
            add { btnFavoriteServers.Click += value; }
            remove { btnFavoriteServers.Click -= value; }
        }

        public event EventHandler LocalServersClick
        {
            add { btnLocalServer.Click += value; }
            remove { btnLocalServer.Click -= value; }
        }

        public event EventHandler ManageMyShardsClick
        {
            add { btnManageMyShards.Click += value; }
            remove { btnManageMyShards.Click -= value; }
        }
        
        public ServerListsGroupControl()
        {
            InitializeComponent();
        }

        public void SelectNone()
        {
            btnFavoriteServers.Selected = false;
            btnLocalServer.Selected = false;
            btnPublicServers.Selected = false;

            btnFavoriteServers.Invalidate();
            btnLocalServer.Invalidate();
            btnPublicServers.Invalidate();
        }

        public void SelectFavoriteServers()
        {
            btnFavoriteServers.Select();
        }

        public void SelectLocalServer()
        {
            btnLocalServer.Select();
        }

        public void SelectPublicServers()
        {
            btnPublicServers.Select();
        }
    }
}
