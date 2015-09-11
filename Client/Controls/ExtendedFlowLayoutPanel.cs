using System.ComponentModel;
using System.Windows.Forms;

namespace ConnectUO.Controls
{
    public class ExtendedFlowLayoutPanel : FlowLayoutPanel
    {
        public ExtendedFlowLayoutPanel()
            : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (Parent is GroupBox)
            {
                GroupBox gb = Parent as GroupBox;
                gb.Height += e.Control.Height + 6;
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            if (Parent is GroupBox)
            {
                GroupBox gb = Parent as GroupBox;
                gb.Height -= e.Control.Height + 6;
            }
        }
    }
}
