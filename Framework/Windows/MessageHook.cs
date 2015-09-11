#region File Header
/********************************************************
 * 
 *  $Id: MessageHook.cs 123 2010-12-02 02:14:35Z jeff $
 *  
 *  $Author: jeff $
 *  $Date: 2010-12-01 18:14:35 -0800 (Wed, 01 Dec 2010) $
 *  $Revision: 123 $
 *  
 *  $LastChangedBy: jeff $
 *  $LastChangedDate: 2010-12-01 18:14:35 -0800 (Wed, 01 Dec 2010) $
 *  $LastChangedRevision: 123 $
 *  
 *  (C) Copyright 2009 Jeff Boulanger
 *  All rights reserved. 
 *  
 ********************************************************/
#endregion

using System;

namespace ConnectUO.Framework.Windows
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nCode"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    public delegate int WndProcHandler(int nCode, IntPtr wParam, IntPtr lParam);
    
    /// <summary>
    /// 
    /// </summary>
    public abstract class MessageHook : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract int HookType { get; }

        private uint _threadId;
        private IntPtr hHook;
        private IntPtr hWnd;
        private WndProcHandler cachedHook;

        /// <summary>
        /// 
        /// </summary>
        public IntPtr HHook
        {
            get { return hHook; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IntPtr HWnd
        {
            get { return hWnd; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        public MessageHook(IntPtr hWnd)
            : this(hWnd, NativeMethods.GetWindowThreadProcessId(hWnd, IntPtr.Zero)) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        public MessageHook(IntPtr hWnd, uint threadId)
        {
            this.hWnd = hWnd;

            _threadId = threadId;
            cachedHook = WndProcHook;
            CreateHook();
        }

        /// <summary>
        /// 
        /// </summary>
        ~MessageHook()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateHook()
        {
            hHook = NativeMethods.SetWindowsHookEx(HookType, cachedHook, IntPtr.Zero, _threadId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        protected virtual int WndProcHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            if (hHook != IntPtr.Zero)
            {
                NativeMethods.UnhookWindowsHookEx(hHook);
            }
        }
    }
}
