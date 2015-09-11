using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConnectUO.Framework
{
    public abstract class OperationBase
    {
        private Thread _thread;

        public event EventHandler<ProgressEventArgs> ProgressChanged;
        public event EventHandler Complete;
        public event EventHandler<ExceptionEventArgs> Error;

        protected OperationBase()
        {
            _thread = new Thread(BeginOverride);
            _thread.IsBackground = true;
        }
        
        public void Begin()
        {
            if (_thread.ThreadState != ThreadState.Running)
            {
                _thread.Start();
            }
        }

        public void End()
        {
            if (_thread.ThreadState == ThreadState.Running ||
                _thread.ThreadState == ThreadState.WaitSleepJoin)
            {
                _thread.Abort();
            }
        }

        protected abstract void BeginOverride();

        protected void OnError(Exception e)
        {
            if (Error != null)
                Error(this, new ExceptionEventArgs(e));
        }

        protected void OnComplete()
        {
            if (Complete != null)
                Complete(this, EventArgs.Empty);
        }

        protected void OnProgressChanged(int current, int length)
        {
            if (ProgressChanged != null)
                ProgressChanged(this, new ProgressEventArgs(current, length));
        }
    }
}
