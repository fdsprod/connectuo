using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectUO.Framework.Windows
{
    public class DesktopWindowsManagerCompositionException : Exception
    {
        public DesktopWindowsManagerCompositionException(string message)
            : base(message) { }
    }
}
