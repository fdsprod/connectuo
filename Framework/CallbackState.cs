using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectUO.Framework
{
    public class CallbackState
    {
        private EventHandler _callback;
        private object _tag;

        public EventHandler Callback
        {
            get { return _callback; }
        }

        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }


        public CallbackState(EventHandler callback)
        {
            _callback = callback;
        }
    }
}
