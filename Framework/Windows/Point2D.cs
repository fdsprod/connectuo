#region File Header
/********************************************************
 * 
 *  $Id: Point2D.cs 111 2010-10-12 06:58:17Z jeff $
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
    public struct Point2D
    {
        public Int32 x;
        public Int32 y;

        public override bool Equals(object obj)
        {
            if (!(obj is Point2D))
            {
                return false;
            }

            Point2D pt = (Point2D)obj;

            return pt.x == x && pt.y == y;
        }

        public static bool operator ==(Point2D p1, Point2D p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point2D p1, Point2D p2)
        {
            return !p1.Equals(p2);
        }
    }
}
