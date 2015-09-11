using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectUO.Framework
{
    public class StorageSerErrorEventArgs : EventArgs
    {
        private readonly Exception _exception;

        public Exception Exception
        {
            get { return _exception; }
        }

        public StorageSerErrorEventArgs(Exception exception)
        {
            _exception = exception;
        }
    }
}
