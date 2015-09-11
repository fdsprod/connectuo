using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectUO.Framework
{
    public class ProgressEventArgs : EventArgs
    {
        readonly object _state;
        readonly int _current;
        readonly int _max;
        readonly int _percentComplete;

        public object State
        {
            get { return _state; }
        }

        public int PercentComplete
        {
            get { return _percentComplete; }
        }

        public int Current
        {
            get { return _current; }
        }

        public int Max
        {
            get { return _max; }
        }

        public ProgressEventArgs(int current, int max)
            : this(null, current, max) { }

        public ProgressEventArgs(object state, int current, int max)
        {
            _state = state;
            _current = current;
            _max = max;
            _percentComplete = (int)(100 * ((double)current / max));
        }
    }
}
