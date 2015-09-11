#region File Header
/********************************************************
 * 
 *  $Id: ApplicationInfo.cs 120 2010-11-23 16:44:35Z jeff $
 *  
 *  $Author: jeff $
 *  $Date: 2010-11-23 08:44:35 -0800 (Tue, 23 Nov 2010) $
 *  $Revision: 120 $
 *  
 *  $LastChangedBy: jeff $
 *  $LastChangedDate: 2010-11-23 08:44:35 -0800 (Tue, 23 Nov 2010) $
 *  $LastChangedRevision: 120 $
 *  
 *  (C) Copyright 2009 Jeff Boulanger
 *  All rights reserved. 
 *  
 ********************************************************/
#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using ConnectUO.Framework.Diagnostics;
using System.Text;
using ConnectUO.Framework.Debug;

namespace ConnectUO.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationInfo 
    {
        private Assembly _assembly;
        private string[] _startupArgs;
        private StartupArgumentParser _startupArgumentParser;

        public StartupArgumentParser StartupArgumentParser
        {
            get { return _startupArgumentParser; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] StartupArgs
        {
            get { return _startupArgs; }
        }

        /// <summary>
        /// Gets the assembly used to create the ApplicationInfo
        /// </summary>
        public Assembly Assembly
        {
            get { return _assembly; }
        }

        /// <summary>
        /// Gets the current running Process
        /// </summary>
        public Process Process
        {
            get { return Process.GetCurrentProcess(); }
        }

        /// <summary>
        /// Gets the version of the Application Assembly
        /// </summary>
        public Version Version
        {
            get { return _assembly.GetName().Version; }
        }

        /// <summary>
        /// Gets the base directory of the Application
        /// </summary>
        public string BaseDirectory
        {
            get { return Path.GetDirectoryName(ExePath); }
        }

        /// <summary>
        /// Gets the exe path to Application
        /// </summary>
        public string ExePath
        {
            get { return _assembly.Location; }
        }

        /// <summary>
        /// Gets a boolean value indicating wether we are in 64bit mode or not.
        /// </summary>
        public bool Is64Bit
        {
            get { return IntPtr.Size == 8; }
        }

        /// <summary>
        /// Creates a new instance of the ApplicationInfo object, the default Assembly used will be Assembly.GetEntryAssembly();
        /// </summary>
        public ApplicationInfo()
            : this(System.Reflection.Assembly.GetEntryAssembly()) { }

        /// <summary>
        /// Creates a new instance of ApplicationInfo.
        /// </summary>
        public ApplicationInfo(Assembly assembly)
            : this(assembly, null, new StartupArgumentFlagCollection()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startupArgs"></param>
        public ApplicationInfo(string[] startupArgs, StartupArgumentFlagCollection flags)
            : this(System.Reflection.Assembly.GetEntryAssembly(), startupArgs, flags) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="startupArgs"></param>
        public ApplicationInfo(Assembly assembly, string[] startupArgs)
            : this(assembly, startupArgs, new StartupArgumentFlagCollection()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="startupArgs"></param>
        public ApplicationInfo(Assembly assembly, string[] startupArgs, StartupArgumentFlagCollection flags)
        {
            if (startupArgs == null)
                startupArgs = new string[0];

            if(startupArgs.Length > 0)
            {
                StringBuilder sb = new StringBuilder();

                for(int i = 0; i < startupArgs.Length; i++)
                    sb.AppendFormat("{0}{1}", startupArgs[i], i + 1 == startupArgs.Length ? "" : ",");

                Tracer.Verbose("Application started with the following startup arguments: {0}", sb.ToString());
            }

            Asserter.AssertIsNotNull(assembly, "assembly");

            _assembly = assembly;
            _startupArgs = startupArgs;
            _startupArgumentParser = new StartupArgumentParser(StartupArgumentFormats.All, false, flags);
            _startupArgumentParser.Parse(startupArgs);
        }
    }
}
