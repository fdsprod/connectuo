using System;

namespace ConnectUO.Controls
{
    public sealed class ShardListItemButtonClickedEventArgs : EventArgs
    {
        private ServerListItem item;
        private ServerListItemButton button;

        public ServerListItem Item
        {
            get { return item; }
            set { item = value; }
        }
        
        public ServerListItemButton Button
        {
            get { return button; }
            set { button = value; }
        }

        public ShardListItemButtonClickedEventArgs(ServerListItem item, ServerListItemButton button)
        {
            this.item = item;
            this.button = button;
        }
    }
}
