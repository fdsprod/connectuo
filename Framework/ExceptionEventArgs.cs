using System;

namespace ConnectUO.Framework
{
    public class ExceptionEventArgs : EventArgs
    {
        private readonly Exception _exception;

        public Exception Exception
        {
            get { return _exception; }
        }

        public ExceptionEventArgs(Exception exception)
        {
            _exception = exception;
        }
    }
}
