#region File Header
/********************************************************
 * 
 *  $Id: SingleExtensions.cs 111 2010-10-12 06:58:17Z jeff $
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

namespace ConnectUO.Framework.Extensions
{
    public static class SingleExtensions
    {
        public static float Epsilon = 0.0001f;

        public static bool NearlyZero(this float val)
        {
            return val < float.Epsilon &&
                   val > -float.Epsilon;
        }

        public static float Cos(this float angle)
        {
            return (float)Math.Cos(angle);
        }

        public static float CosABS(this float angle)
        {
            return (float)Math.Abs(Math.Cos(angle));
        }

        public static float Sin(this float angle)
        {
            return (float)Math.Sin(angle);
        }
    }
}
