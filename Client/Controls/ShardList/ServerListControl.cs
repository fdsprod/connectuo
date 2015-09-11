using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ConnectUO.Framework.Services;
using Ninject;
using System.Collections.Generic;
using ConnectUO.Framework.Data;
using ConnectUO.Framework;
using ConnectUO.Controls.ShardList;
using ConnectUO.Properties;

namespace ConnectUO.Controls
{
    public abstract class ServerListControl : BaseConnectUOPanel, IActivate, IDeactivate
    {
        private ServerListItem _selectedItem;
        private SimpleVScrollbar _vScrollBar;
        private ServerListItemCollection _items;
        private IStorageService _storageService;
        private ServerListOrderBy _orderBy;
        private bool _orderByReversed;
        private ServerComparer _comparer;
        private string _filterText;
        private IKernel _kernel;

        public ServerListOrderBy OrderBy
        {
            get { return _orderBy; }
            set
            {
                _orderBy = value;
                RefreshDataSource();
            }
        }

        public bool OrderByReversed
        {
            get { return _orderByReversed; }
            set
            {
                _orderByReversed = value;
                RefreshDataSource();
            }
        }

        public string FilterText
        {
            get { return _filterText; }
            set
            { 
                _filterText = value;
                RefreshDataSource();
            }
        }

        public ServerListItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != null)
                {
                    _selectedItem.IsSelected = false;
                }

                _selectedItem = value;

                if (_selectedItem != null)
                {
                    _selectedItem.IsSelected = true;
                }

                Invalidate();
            }
        }

        public ServerListItemCollection Items
        {
            get { return _items; }
            set { _items = value; }
        }     

        public int SelectedIndex
        {
            get { return _items.IndexOf(_selectedItem); }
        }

        public int ScrollPosition
        {
            get { return _vScrollBar.Value; }
            set { _vScrollBar.Value = value; }
        }

        protected IStorageService StorageService
        {
            get { return _storageService; }
        }

        protected int ItemHeight
        {
            get { return Renderer.ItemHeight; }
        }

        protected int TotalItemHeight
        {
            get 
            {
                if (_items == null)
                    return 0;

                return (_items.Count) * ItemHeight + (ItemHeight * 2); 
            }
        }
                
        [Inject]
        public ServerListControl(IKernel kernel)
        {
            _kernel = kernel;
            _storageService = kernel.Get<IStorageService>();
            _items = new ServerListItemCollection();
            _comparer = new ServerComparer();

            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.ResizeRedraw, true);

            BackColor = Color.Transparent;
        }

        protected abstract void AddServerItemButtons(ServerListItem server);

        protected abstract Server[] GetServers();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RefreshDataSource();
        }
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if(Visible)
                InvalidateScrollbar();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            if (Items.Count == 0)
            {
                Image image = Resources.cuo_logo_small;

                Rectangle location =
                    new Rectangle(
                        (int)(Width / 2f - image.Width / 2f),
                        (int)(Height / 2f - image.Height / 2f),
                        image.Width,
                        image.Height);

                g.DrawImage(image, location);
            }

            int startY = _vScrollBar.Value;
            int endY = startY + Height;
            int startIndex, endIndex;

            CalculateStartEndIndex(startY, endY, out startIndex, out endIndex);

            Rectangle bounds = e.ClipRectangle;

            bounds.Width = Width - 1;
            bounds.Height -= 1;

            int width = Width - 2 - (_vScrollBar.Visible ? _vScrollBar.Width : 0);
            SizeF selectedSize = SizeF.Empty;

            if (_selectedItem != null)
            {
                selectedSize = _selectedItem.GetSize(this.CreateGraphics(), Renderer, width);
            }

            if (!DesignMode)
            {
                bounds.Y += 2;
                bounds.Height -= 2;

                int offsetY = 0;

                e.Graphics.SetClip(bounds);

                for (int i = startIndex; i < endIndex; i++)
                {
                    int height = Renderer.ItemHeight;
                    int y = offsetY + (1 + i * Renderer.ItemHeight - startY);

                    Rectangle rect = new Rectangle(0, y, width, height);

                    if (_items[i].IsSelected)
                    {
                        offsetY += (int)selectedSize.Height;
                        rect.Height += (int)selectedSize.Height;
                    }

                    _items[i].IsVisible = rect.IntersectsWith(bounds);

                    if (_items[i].IsVisible)
                    {
                        Renderer.DrawServer(g, _items[i], rect);
                    }
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            _vScrollBar.Value -= (e.Delta / 120) * (ItemHeight / 2);
            _vScrollBar.Refresh();
        }  

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            SetAllItemsMouseOverFalse();
            Cursor = Cursors.Default;

            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Point mouseLocation = new Point(e.X, e.Y);
            Point itemLocation = Point.Empty;
            Size itemSize = Size.Empty;

            ServerListItem item = GetItemAt(e.X, e.Y, out itemLocation, out itemSize);

            if (item != null)
            {
                bool clicked = false;
                int startY = 10;

                for (int j = 0; j < item.Buttons.Count && !clicked; j++)
                {
                    Rectangle buttonBounds = item.Buttons[j].GetBounds(itemLocation, itemSize);
                    buttonBounds.Y += startY;
                    clicked = buttonBounds.Contains(mouseLocation);

                    if (clicked)
                    {
                        item.Buttons[j].OnClick(_kernel);
                    }

                    startY += 15;
                }
            }

            SelectedItem = item;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point mouseLocation = PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
            
            Point itemLocation = Point.Empty;
            Size itemSize = Size.Empty;

            ServerListItem item = GetItemAt(mouseLocation.X, mouseLocation.Y, out itemLocation, out itemSize); 

            SetAllItemsMouseOverFalse();

            Cursor = Cursors.Default;

            if (item != null)
            {
                item.IsMouseOver = true;
                int startY = 10;

                for (int j = 0; j < item.Buttons.Count; j++)
                {
                    Rectangle buttonBounds = item.Buttons[j].GetBounds(itemLocation, itemSize);
                    buttonBounds.Y += startY;

                    if (buttonBounds.Contains(mouseLocation))
                    {
                        Cursor = Cursors.Hand;
                        item.Buttons[j].IsMouseOver = true;

                        if (ControlHelper.IsMouseDown(MouseButtons.Left))
                        {
                            item.Buttons[j].IsMouseDown = true;
                        }
                    }

                    startY += 15;
                }
            }

            Invalidate();
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            InvalidateScrollbar();

            base.OnInvalidated(e);
        }
        
        private void CalculateStartEndIndex(int startY, int endY, out int startIndex, out int endIndex)
        {
            int selectedOffset = 0;

            startIndex = Math.Max(startY / ItemHeight - 1, 0);
            endIndex = Math.Min(endY / ItemHeight + 1, _items.Count);

            if (SelectedItem != null)
            {
                Graphics g = CreateGraphics();
                selectedOffset = (int)Renderer.MeasureDescription(g, SelectedItem.Server.Description, Width).Height;
                g.Dispose();

                if (startIndex > SelectedIndex)
                {
                    startIndex = SelectedIndex;
                    int endOfSelected = (SelectedIndex - 1) * ItemHeight + selectedOffset;

                    if (endOfSelected < startY)
                    {
                        startIndex = (((startY - endOfSelected) / ItemHeight) - 1);
                    }
                }
            }

            startIndex--;
            endIndex++;

            startIndex = Math.Max(startIndex, 0);
            endIndex = Math.Min(endIndex, _items.Count);
        }

        private void SetAllItemsMouseOverFalse()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].IsMouseOver = false;

                for (int j = 0; j < _items[i].Buttons.Count; j++)
                {
                    _items[i].Buttons[j].IsMouseDown = false;
                    _items[i].Buttons[j].IsMouseOver = false;
                }
            }
        }

        private void OnScrollBarValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void InvalidateScrollbar()
        {
            if (_vScrollBar != null)
            {
                int totalItemHeight = TotalItemHeight;

                _vScrollBar.Maximum = Math.Max(totalItemHeight - Height, 0) + 1;
                _vScrollBar.Visible = totalItemHeight > Height;
            }
        }

        private ServerListItem CreateServerListItem(Server server)
        {
            ServerListItem item = new ServerListItem(this, server);

            AddServerItemButtons(item);

            return item;
        }

        private void InitializeComponent()
        {
            this._vScrollBar = new SimpleVScrollbar();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this._vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._vScrollBar.BackColor = System.Drawing.Color.White;
            this._vScrollBar.LargeChange = 25;
            this._vScrollBar.Location = new System.Drawing.Point(510, 3);
            this._vScrollBar.Maximum = 50;
            this._vScrollBar.Minimum = 0;
            this._vScrollBar.Name = "vScrollBar";
            this._vScrollBar.Size = new System.Drawing.Size(5, 326);
            this._vScrollBar.SmallChange = 0;
            this._vScrollBar.TabIndex = 0;
            this._vScrollBar.Value = 0;
            this._vScrollBar.Visible = false;
            this._vScrollBar.ValueChanged += new System.EventHandler<System.EventArgs>(this.OnScrollBarValueChanged);
            // 
            // ShardListControl
            // 
            this.Controls.Add(this._vScrollBar);
            this.Name = "ShardListControl";
            this.Size = new System.Drawing.Size(517, 335);
            this.ResumeLayout(false);

        }

        public ServerListItem GetItemAt(int x, int y, out Point itemLocation, out Size itemSize)
        {
            itemLocation = Point.Empty;
            itemSize = Size.Empty;

            Point pointInList = new Point(x, y + _vScrollBar.Value);

            int offsetY = 0;

            int startY = _vScrollBar.Value;
            int endY = startY + Height;
            int startIndex, endIndex;

            CalculateStartEndIndex(startY, endY, out startIndex, out endIndex);

            int selectedIndex = -1;

            int width = Width - 2 - (_vScrollBar.Visible ? _vScrollBar.Width : 0);
            SizeF selectedSize = SizeF.Empty;

            if (_selectedItem != null)
            {
                selectedIndex = _items.IndexOf(_selectedItem);
                selectedSize = _selectedItem.GetSize(this.CreateGraphics(), Renderer, width);
            }

            for (int i = startIndex; i < endIndex; i++)
            {
                int height = Renderer.ItemHeight;
                int dy = offsetY + (1 + i * Renderer.ItemHeight);

                Rectangle rect = new Rectangle(0, dy, width, height);

                if (_items[i].IsSelected)
                {
                    offsetY += (int)selectedSize.Height;
                    rect.Height += (int)selectedSize.Height;
                }

                if (rect.Contains(pointInList))
                {
                    itemLocation = new Point(rect.X, rect.Y - _vScrollBar.Value);
                    itemSize = new Size(rect.Width, rect.Height);

                    return _items[i];
                }
            }

            return null;
        }

        public ServerListItem GetItemAt(int x, int y)
        {
            Point p; Size s;
            return GetItemAt(x, y, out p, out s);
        }

        public void RefreshDataSource()
        {
            int scrollPos = _vScrollBar.Value;

            Server[] servers = GetServers();
            Server selectedServer = SelectedItem == null ? null : SelectedItem.Server;

            Items.Clear();

            _comparer.OrderBy = _orderBy;
            _comparer.Reverse = _orderByReversed;

            Array.Sort(servers, _comparer);

            for (int i = 0; i < servers.Length; i++)
            {
                Server shard = servers[i];

                if (string.IsNullOrEmpty(_filterText) ||
                    (shard.Name.ToLower().Contains(_filterText.ToLower()) || shard.Description.ToLower().Contains(_filterText.ToLower())))
                {
                    ServerListItem item = CreateServerListItem(shard);

                    Items.Add(item);

                    if (selectedServer != null && selectedServer is Server)
                    {
                        if (shard.Id == selectedServer.Id)
                        {
                            SelectedItem = item;
                        }
                    }
                }
            }

            ScrollPosition = scrollPos;
        }

        public virtual void Activate()
        {
            RefreshDataSource();
        }

        public virtual void Deactivate()
        {

        }
    }
}
