using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ConnectUO.Framework
{
    public enum Views
    {
        [Description("Public Servers")]
        PublicServers = 0,
        [Description("Favorites")]
        FavoritesServers,
        [Description("Local Servers")]
        LocalServers,
        [Description("Edit Server")]
        EditServer,
        [Description("Settings")]
        Settings,
        [Description("About")]
        About
    }
}
