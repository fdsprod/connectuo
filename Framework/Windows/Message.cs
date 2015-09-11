#region File Header
/********************************************************
 * 
 *  $Id: Message.cs 111 2010-10-12 06:58:17Z jeff $
 *  
 *  $Author: jeff $
 *  $Date: 2010-10-11 23:58:17 -0700 (Mon, 11 Oct 2010) $
 *  $Revision: 111 $
 *  
 *  $LastChangedBy: jeff $
 *  $LastChangedDate: 2010-10-11 23:58:17 -0700 (Mon, 11 Oct 2010) $
 *  $LastChangedRevision: 111 $
 *  
 *  (C) Copyright 2009 Jeff Boulanger
 *  All rights reserved. 
 *  
 ********************************************************/
#endregion

using System;
using System.Runtime.InteropServices;

namespace ConnectUO.Framework.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MESSAGE
    {
        public IntPtr WindowHandle;
        public UInt32 Id;
        public IntPtr WParam;
        public IntPtr LParam;
        public Int32 Time;
        public Point2D Point;

        public override bool Equals(object obj)
        {
            if (!(obj is MESSAGE))
            {
                return false;
            }

            MESSAGE msg = (MESSAGE)obj;

            return msg.Id == Id && msg.LParam == LParam && msg.Point == Point && msg.Time == Time && msg.WindowHandle == WindowHandle && msg.WParam == WParam; 
        }

        public static bool operator ==(MESSAGE m1, MESSAGE m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(MESSAGE m1, MESSAGE m2)
        {
            return !m1.Equals(m2);
        }
    }
}
