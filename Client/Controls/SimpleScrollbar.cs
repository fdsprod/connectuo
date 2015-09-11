using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ConnectUO.Framework;

namespace ConnectUO.Controls
{
    public sealed class SimpleVScrollbar : Control
    {
        private int max;
        private int min;
        private int value;
        private int largeChange;
        private int smallChange;

        bool mouseDown;

        public int LargeChange
        {
            get { return largeChange; }
            set { largeChange = value; }
        }

        public int SmallChange
        {
            get { return smallChange; }
            set { smallChange = value; }
        }

        public int Maximum
        {
            get { return max; }
            set 
            {
                max = value;
                MathHelper.Clamp(this.value, min, max);
                Invalidate();
            }
        }

        public int Minimum
        {
            get { return min; }
            set
            {
                min = value;
                MathHelper.Clamp(this.value, min, max);
                Invalidate();
            }
        }

        public int Value
        {
            get { return this.value; }
            set
            {
                    this.value = value;
                    OnValueChanged(this, EventArgs.Empty);
                    this.value = MathHelper.Clamp(this.value, min, max);
                    Invalidate();
            }
        }

        public event EventHandler<EventArgs> ValueChanged;

        public SimpleVScrollbar()
            : base()
        {            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw | 
                     ControlStyles.UserPaint |
                     ControlStyles.UserMouse, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            int arcSize = 4;
            GraphicsPath path = new GraphicsPath();

            float rectScale = 0.1f;

            Rectangle bounds = new Rectangle(0, 0, Width - 1, (int)(Height * rectScale));

            int minY = bounds.Height / 2;
            int maxY = Height - bounds.Height / 2;

            bounds.Y = (int)((Height - (minY * 2) - 1) * ((float)value / (float)max));
            
            path.AddArc(new Rectangle(bounds.X, bounds.Y, arcSize, arcSize), 180, 90);
            path.AddArc(new Rectangle(bounds.X + bounds.Width - arcSize, bounds.Y, arcSize, arcSize), 270, 90);
            path.AddArc(new Rectangle(bounds.X + bounds.Width - arcSize, bounds.Y + bounds.Height - arcSize, arcSize, arcSize), 0, 90);
            path.AddArc(new Rectangle(bounds.X, bounds.Y + bounds.Height - arcSize, arcSize, arcSize), 90, 90);

            path.CloseFigure();

            g.FillPath(Brushes.Gray, path);
        }
        private void OnValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(sender, e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (mouseDown)
            {
                Value = (int)((max * e.Y / (Height)) - (Height * 0.1f));
            }

            Refresh();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                Value = (int)(max * e.Y / (Height - (Height * 0.1f)));
                Invalidate();
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            mouseDown = false;
        }
    }
}
