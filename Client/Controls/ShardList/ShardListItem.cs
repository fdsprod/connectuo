using System;
using System.Collections.Generic;
using ConnectUO.Data;
using ConnectUO.Framework.Data;
using System.Drawing;

namespace ConnectUO.Controls
{
    [Serializable]
    public sealed class ServerListItem
    {
        private Server _server;
        private bool _selected;
        private bool _isMouseOver;
        private bool _isVisible;
        private ServerListControl _parent;
        private List<ServerListItemButton> _buttons;

        public ServerListControl Parent
        {
            get { return _parent; }
        }

        public List<ServerListItemButton> Buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }
        
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        public bool IsMouseOver
        {
            get 
            { 
                return _isMouseOver; 
            }
            set 
            { 
                _isMouseOver = value;
            }
        }

        public bool IsSelected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public Server Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public ServerListItem(ServerListControl parent)
            : this(parent, null) { }

        public ServerListItem(ServerListControl parent, Server server)
        {
            _parent = parent;
            _server = server;
            _buttons = new List<ServerListItemButton>();
        }

        public SizeF GetSize(Graphics graphics, ConnectUORenderer renderer, int width)
        {
            return renderer.MeasureDescription(graphics, _server.Description, width);
        }
    }
}
