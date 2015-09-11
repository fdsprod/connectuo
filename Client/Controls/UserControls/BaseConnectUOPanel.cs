using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ConnectUO.Framework.Extensions;

namespace ConnectUO.Controls
{
    public partial class BaseConnectUOPanel : UserControl
    {
        private static ConnectUORenderer _renderer = new DefaultRenderer();
        protected ConnectUORenderer Renderer { get { return _renderer; } }

        public BaseConnectUOPanel()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            Rectangle bounds = new Rectangle(0, 0, Width, Height);

            bounds.Width = Width - 1;
            bounds.Height -= 1;

            int arcSize = 10;

            GraphicsPath path = new GraphicsPath();

            AddRoundedRectangle(path, bounds, arcSize);

            using (SolidBrush brush = new SolidBrush(Renderer.BackColor))
            {
                g.FillPath(brush, path);
            }

            using (Pen pen = new Pen(Renderer.BorderPenColor))
            {
                g.DrawPath(pen, path);
            }

            path.Dispose();
        }

        public static void AddRoundedRectangle(GraphicsPath path, Rectangle bounds, int arcSize)
        {
            path.AddArc(new Rectangle(bounds.X, bounds.Y, arcSize, arcSize), 180, 90);
            path.AddArc(new Rectangle(bounds.X + bounds.Width - arcSize, bounds.Y, arcSize, arcSize), 270, 90);
            path.AddArc(new Rectangle(bounds.X + bounds.Width - arcSize, bounds.Y + bounds.Height - arcSize, arcSize, arcSize), 0, 90);
            path.AddArc(new Rectangle(bounds.X, bounds.Y + bounds.Height - arcSize, arcSize, arcSize), 90, 90);

            path.CloseFigure();
        }
    }
}
