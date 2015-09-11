using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ConnectUO.Framework.Diagnostics;

namespace ConnectUO.Framework.Windows
{
    public static class DesktopWindowsManager
    {
        public static bool IsCompositionSupported
        {
            get
            {
                return (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6);
            }
        }

        public static bool IsCompositionEnabled
        {
            get { return NativeMethods.DwmIsCompositionEnabled(); }
            set
            {
                try
                {
                    if (!IsCompositionSupported)
                        return;

                    NativeMethods.DwmEnableComposition(value);
                }
                catch(Exception e)
                {
                    Tracer.Error("An error occurred while trying to set Desktop Composition to {0}: {1}", value, e);
                }
            }
        }

        /// <summary>Enable the Aero "Blur Behind" effect on the whole client area. Background must be black.</summary>
        public static void EnableBlurBehind(IntPtr hWnd)
        {
            BlurBehind bb = new BlurBehind();
            bb.dwFlags = BlurBehindFlags.Enable;
            bb.fEnable = true;
            bb.hRgnBlur = (IntPtr)0;

            NativeMethods.DwmEnableBlurBehindWindow(hWnd, ref bb);
        }

        /// <summary>Enable the Aero "Blur Behind" effect on the whole client area. Background must be black.</summary>
        public static void EnableBlurBehind(IntPtr hWnd, IntPtr regionHandle)
        {
            BlurBehind bb = new BlurBehind();
            bb.dwFlags = BlurBehindFlags.Enable | BlurBehindFlags.BlurRegion;
            bb.fEnable = true;
            bb.hRgnBlur = regionHandle;

            NativeMethods.DwmEnableBlurBehindWindow(hWnd, ref bb);
        }

        public static void DisableBlurBehind(IntPtr hWnd)
        {
            BlurBehind bb = new BlurBehind();
            bb.dwFlags = BlurBehindFlags.Enable;
            bb.fEnable = false;

            NativeMethods.DwmEnableBlurBehindWindow(hWnd, ref bb);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="margin"></param>
        public static void EnableGlass(IntPtr hWnd, ref Margins margin)
        {
            InternalGlassFrame(hWnd, ref margin);
        }

        /// <summary>
        /// Extends the Aero "Glass Frame" to the whole client area ("Glass Sheet" effect). Background must be black.
        /// </summary>
        public static void EnableGlassSheet(IntPtr hWnd)
        {
            Margins margin = new Margins(-1);
            InternalGlassFrame(hWnd, ref margin);
        }

        /// <summary>
        /// Disables the Aero "Glass Frame".
        /// </summary>
        public static void DisableGlassFrame(IntPtr hWnd)
        {
            Margins margin = new Margins(0);
            InternalGlassFrame(hWnd, ref margin);
        }

        private static void InternalGlassFrame(IntPtr hWnd, ref Margins margins)
        {
            if (NativeMethods.DwmExtendFrameIntoClientArea(hWnd, ref margins) != 0)
                throw new DesktopWindowsManagerCompositionException("Unable to set glass frame.");
        }
    }
}
