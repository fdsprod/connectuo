using System.Drawing;

namespace ConnectUO.Controls
{
    public sealed class DarkConnectUORenderer : ConnectUORenderer
    {
        public override string Name { get { return "Dark"; } }
        public override int ItemHeight { get { return 90; } }
        public override int ArcSize { get { return 10; } }
        public override Color ForeColor { get { return Color.FromArgb(0x88, 0x88, 0x88); } }
        public override Color BackColor { get { return Color.FromArgb(0x1d, 0x1d, 0x1d); } }
        public override Color BorderPenColor { get { return Color.FromArgb(0x2e, 0x2e, 0x2e); } }
        public override Color FillColorTop { get { return Color.FromArgb(0x40, 0x40, 0x40); } }
        public override Color FillColorBottom { get { return Color.FromArgb(0x1b, 0x1b, 0x1b); } }
        public override Color FillColorTopMouseOver { get { return Color.FromArgb(0x1b, 0x1b, 0x1b); } }
        public override Color FillColorBottomMouseOver { get { return Color.FromArgb(0x40, 0x40, 0x40); } }
        public override Color FillColorTopSelected { get { return Color.FromArgb(0x5e, 0x5e, 0x5e); } }
        public override Color FillColorBottomSelected { get { return Color.FromArgb(0x34, 0x34, 0x34); } }
        public override Color FillColorTopMouseOverSelected { get { return Color.FromArgb(0x34, 0x34, 0x34); } }
        public override Color FillColorBottomMouseOverSelected { get { return Color.FromArgb(0x5e, 0x5e, 0x5e); } }
        public override Color DefaultTextColor { get { return Color.FromArgb(0x88, 0x88, 0x88); } }
        public override Color DefaultTextColorMouseOver { get { return Color.FromArgb(0x88, 0x88, 0x88); } }
        public override Color DefaultTextColorSelected { get { return Color.FromArgb(0x88, 0x88, 0x88); } }
        public override Color DefaultTextColorMouseOverSelected { get { return Color.FromArgb(0x88, 0x88, 0x88); } }
        public override Color TextNameColor { get { return Color.FromArgb(0xc2, 0xbf, 0xae); } }
        public override Color TextNameColorMouseOver { get { return Color.FromArgb(0xc2, 0xbf, 0xae); } }
        public override Color TextNameColorSelected { get { return Color.FromArgb(0xc1, 0xc1, 0xb0); } }
        public override Color TextNameColorMouseOverSelected { get { return Color.FromArgb(0xc1, 0xc1, 0xb0); } }
        public override Color TextStrongColor { get { return Color.White; } }
        public override Color TextStrongColorMouseOver { get { return Color.White; } }
        public override Color TextStrongColorSelected { get { return Color.White; } }
        public override Color TextStrongColorMouseOverSelected { get { return Color.White; } }
        public override Bitmap DefaultShardIcon { get { return Properties.Resources.RunUOGearShadow; } }
        public override Bitmap DefaultUpIcon { get { return Properties.Resources.Up; } }
        public override Bitmap DefaultDownIcon { get { return Properties.Resources.Down; } }
        public override Bitmap DefaultUnknownIcon { get { return Properties.Resources.Unknown; } }
        public override float FontSizeName { get { return 14; } }
        public override float FontSizeDescription { get { return 12; } }
    }
}
