using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ConnectUO.Controls
{
    public partial class ArrowButton : UserControl
    {
        private bool selected = false;
        private bool selectable = true;

        [Browsable(false)]
        public bool Selected
        {
            get { return selected; }
            set 
            {
                selected = value;

                if (selectable)
                {
                    pbArrow.BackgroundImage =
                        (selected ? Properties.Resources.ArrowSelected : Properties.Resources.Arrow);
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Selectable
        {
            get { return selectable; }
            set { selectable = value; }
        }
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Refresh();
            }
        }

        public event EventHandler<EventArgs> ControlSelected;

        public ArrowButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserMouse, true);
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            EnsureSize();
        }

        public new void Select()
        {
            OnControlSelected(this, EventArgs.Empty);
        }

        private void OnControlSelected(object sender, EventArgs e)
        {
            if (ControlSelected != null)
                ControlSelected(sender, e);

            if (Parent != null)
            {
                for (int i = 0; i < Parent.Controls.Count; i++)
                    if (Parent.Controls[i] is ArrowButton)
                        ((ArrowButton)Parent.Controls[i]).Selected = (Parent.Controls[i] == this);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, Text, Font, new Point(5, 0), ForeColor);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            //EnsureSize();
        }

        private void EnsureSize()
        {
            if (IsHandleCreated)
            {
                Graphics g = CreateGraphics();
                Width = (Padding.Left + 5 + (int)g.MeasureString(Text, Font).Width);
                g.Dispose();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            Font = new Font(Font, FontStyle.Underline);
            Cursor = Cursors.Hand;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            Font = new Font(Font, FontStyle.Regular);
            Cursor = Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            
            if (selectable)
                OnControlSelected(this, EventArgs.Empty);
        }
    }
}
