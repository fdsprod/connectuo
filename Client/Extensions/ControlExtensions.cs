using System.Windows.Forms;

namespace ConnectUO.Controls
{
    public static class ControlHelper
    {
        public static bool IsMouseDown(MouseButtons button)
        {
            return (((int)Control.MouseButtons & (int)button) == (int)button);
        }

        public static bool IsMouseUp(MouseButtons button)
        {
            return !IsMouseDown(button);
        }
    }
}
