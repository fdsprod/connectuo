using System.Drawing;
using Ninject;

namespace ConnectUO.Controls
{
    public abstract class ServerListItemButton
    {
        private string text;
        private ServerListItem item;
        private Point locationFromRight;
        private Size size;
        private bool isMouseOver;
        private bool isMouseDown;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public ServerListItem Item
        {
            get { return item; }
            set { item = value; }
        }

        public int Top
        {
            get { return locationFromRight.Y; }
            set { locationFromRight.Y = value; }
        }

        public Point LocationFromRight
        {
            get { return locationFromRight; }
            set { locationFromRight = value; }
        }


        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        public bool IsMouseOver
        {
            get { return isMouseOver; }
            set { isMouseOver = value; }
        }

        public bool IsMouseDown
        {
            get { return isMouseDown; }
            set { isMouseDown = value; }
        }

        public ServerListItemButton(
            string text, 
            ServerListItem item,
            int pixelsFromLeft,
            int pixelsFromTop, 
            Size size)
        {
            this.text = text;
            this.item = item;
            this.locationFromRight = new Point(pixelsFromLeft, pixelsFromTop);
            this.size = size;
        }

        public Rectangle GetBounds(Point itemLocation, Size itemSize)
        {
            return new Rectangle((itemLocation.X + itemSize.Width) - locationFromRight.X,
                                 itemLocation.Y,
                                 size.Width, 
                                 size.Height);
        }

        public abstract void OnClick(IKernel kernel);
    }
}
