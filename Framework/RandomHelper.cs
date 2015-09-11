#region File Header
/********************************************************
 * 
 *  $Id: Utility.cs 111 2010-10-12 06:58:17Z jeff $
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using ConnectUO.Framework.Windows;

namespace ConnectUO.Framework
{
    public static class RandomHelper
    {
        private static Random random = new Random();
        
        /// <summary>
        /// Returns a random number between 0.0 and 1.0
        /// </summary>
        public static double RandomDouble()
        {
            return random.NextDouble();
        }

        /// <summary>
        /// Returns a random Boolean value
        /// </summary>
        public static bool RandomBool()
        {
            return (random.Next(2) == 0);
        }

        /// <summary>
        /// Returns a random number within the specified range
        /// </summary>
        public static int RandomMinMax(int min, int max)
        {
            if (min > max)
            {
                int copy = min;
                min = max;
                max = copy;
            }
            else if (min == max)
            {
                return min;
            }

            return random.Next(min, max);
        }
        
        /// <summary>
        /// Returns a random number within the specified range, works for both negative and positive count values
        /// </summary>
        public static int Random(int from, int count)
        {
            if (count == 0)
            {
                return from;
            }
            else if (count > 0)
            {
                return from + random.Next(count);
            }
            else
            {
                return from - random.Next(-count);
            }
        }

        /// <summary>
        /// Returns a non-negative number less than the supplied count value
        /// </summary>
        public static int Random(int count)
        {
            return random.Next(count);
        }

        /// <summary>
        /// Returns a non-negative random number
        /// </summary>
        public static int Random()
        {
            return random.Next();
        }
    }
}
