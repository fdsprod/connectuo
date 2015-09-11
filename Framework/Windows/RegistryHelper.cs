/********************************************************
 * 
 *  $Id: RegistryHelper.cs 29 2009-09-11 22:19:25Z jeff $
 *  
 *  $Author: jeff $
 *  $Date: 2009-09-11 15:19:25 -0700 (Fri, 11 Sep 2009) $
 *  $Revision: 29 $
 *  
 *  $LastChangedBy: jeff $
 *  $LastChangedDate: 2009-09-11 15:19:25 -0700 (Fri, 11 Sep 2009) $
 *  $LastChangedRevision: 29 $
 *  
 *  (C) Copyright 2009 Jeff Boulanger
 *  All rights reserved. 
 *  
 ********************************************************/

using System;
using Microsoft.Win32;
using ConnectUO.Framework.Diagnostics;

namespace ConnectUO.Framework.Windows
{
    public enum RegistryError
    {
        None, 
        Error
    }

    public static class RegistryHelper
    {
        public static RegistryError Get(RegistryKey regKey, string location, ref string value)
        {
            RegistryError rerr = RegistryError.None;

            try
            {
                regKey = regKey.OpenSubKey(location);
                value = regKey.GetValue(value).ToString();
            }
            catch (Exception e)
            {
                rerr = RegistryError.Error;
                Tracer.Error(e);
            }
            finally
            {
                if(regKey != null)
                    regKey.Close();
            }

            return rerr;
        }
        public static RegistryError Set(RegistryKey regKey, string location, string key, string value, RegistryValueKind kind)
        {
            RegistryError rerr = RegistryError.None;

            try
            {
                regKey = regKey.OpenSubKey(location, true);
                regKey.SetValue(key, value, kind);
            }
            catch (Exception e)
            {
                rerr = RegistryError.Error;
                Tracer.Error(e);
            }
            finally
            {
                regKey.Close();
            }

            return rerr;
        }
        public static RegistryError Delete(RegistryKey regKey, string location, string value)
        {
            RegistryError rerr = RegistryError.None;

            try
            {
                regKey = regKey.OpenSubKey(location, true);
                regKey.DeleteValue(value, false);
            }
            catch (Exception e)
            {
                rerr = RegistryError.Error;
                Tracer.Error(e);
            }
            finally
            {
                regKey.Close();
            }

            return rerr;
        }
    }
}
