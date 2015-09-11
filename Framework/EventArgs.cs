/********************************************************
 * 
 *  $Id: EventArgs.cs 63 2009-11-02 22:14:43Z jeff $
 *  
 *  $Author: jeff $
 *  $Date: 2009-11-02 14:14:43 -0800 (Mon, 02 Nov 2009) $
 *  $Revision: 63 $
 *  
 *  $LastChangedBy: jeff $
 *  $LastChangedDate: 2009-11-02 14:14:43 -0800 (Mon, 02 Nov 2009) $
 *  $LastChangedRevision: 63 $
 *  
 *  (C) Copyright 2009 Jeff Boulanger
 *  All rights reserved. 
 *  
 ********************************************************/

using System;
using System.Collections;
using System.Globalization;

namespace ConnectUO.Framework
{
    public class ProgressUpdateEventArgs : EventArgs
    {
        readonly object _state;
        readonly int _current;
        readonly int _max;
        readonly int _progressPercentage;

        public object State
        {
            get { return _state; }
        } 

        public int ProgressPercentage
        {
            get { return _progressPercentage; }
        } 

        public int Current
        {
            get { return _current; }
        }

        public int Max
        {
            get { return _max; }
        }

        public ProgressUpdateEventArgs(int current, int max)
            : this(null, current, max) { }

        public ProgressUpdateEventArgs(object state, int current, int max)
        {
            _state = state;
            _current = current;
            _max = max;
            _progressPercentage = (int)(100 * ((double)current / max));
        }
    }

    public class StatusUpdateEventArgs : EventArgs
    {
        readonly object _state;
        readonly string _status;
        readonly int _statusLevel;

        public object State
        {
            get { return _state; }
        } 

        public string Status
        {
            get { return _status; }
        }

        public int StatusLevel
        {
            get { return _statusLevel; }
        }

        public StatusUpdateEventArgs(string status, params object[] objects)
            : this((object)null, ConnectUO.Framework.StatusLevel.Debug, status, objects)
        {
        }

        public StatusUpdateEventArgs(string status)
            : this(ConnectUO.Framework.StatusLevel.Debug, status)
        {
        }

        public StatusUpdateEventArgs(object state, int statusLevel, string status, params object[] objects)
        {
            _state = state;
            _statusLevel = statusLevel;
            _status = String.Format(CultureInfo.CurrentCulture, status, objects);
        }

        public StatusUpdateEventArgs(int statusLevel, string status)
        {
            _statusLevel = statusLevel;
            _status = status;
        }
    }

    public class ProgressStartedEventArgs : EventArgs
    {
        readonly object _state;

        public object State
        {
            get { return _state; }
        }

        public ProgressStartedEventArgs() { }

        public ProgressStartedEventArgs(object state)
        {
            _state = state;
        }
    }

    public class ProgressCompletedEventArgs : ProgressUpdateEventArgs
    {
        readonly object _exceptionObject;
        readonly bool _cancelled;

        public bool Cancelled
        {
            get { return _cancelled; }
        } 
        
        public object ExceptionObject
        {
            get { return _exceptionObject; }
        } 
        
        public ProgressCompletedEventArgs(bool canceled, int current, int max)
            : this(null, canceled, null, current, max) { }

        public ProgressCompletedEventArgs(Exception exception, bool canceled, object state, int current, int max)
            : base(state, current, max)
        {
            _exceptionObject = exception;
            _cancelled = canceled;
        }
    }

    public class PropertyChangedEventArgs : EventArgs
    {
        private readonly string _propertyName;

        public virtual string PropertyName
        {
            get { return _propertyName; }
        }

        public PropertyChangedEventArgs(string propertyName)
        {
            _propertyName = propertyName;
        }
    }

    public class PropertyChangingEventArgs : EventArgs
    {
        private readonly string _propertyName;

        public virtual string PropertyName
        {
            get { return _propertyName; }
        }

        public PropertyChangingEventArgs(string propertyName)
        {
            _propertyName = propertyName;
        }
    }

    public enum CollectionChangedNotifierAction
    {
        Add,
        Remove,
        Replace,
        Move,
        Reset
    }

    public class NotifyCollectionChangedEventArgs : EventArgs
    {
        private CollectionChangedNotifierAction _action;
        private IList _newItems;
        private int _newStartingIndex;
        private IList _oldItems;
        private int _oldStartingIndex;

        public CollectionChangedNotifierAction Action
        {
            get
            {
                return _action;
            }
        }

        public IList NewItems
        {
            get
            {
                return _newItems;
            }
        }

        public int NewStartingIndex
        {
            get
            {
                return _newStartingIndex;
            }
        }

        public IList OldItems
        {
            get
            {
                return _oldItems;
            }
        }

        public int OldStartingIndex
        {
            get
            {
                return _oldStartingIndex;
            }
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;

            if (action != CollectionChangedNotifierAction.Reset)
            {
                throw new ArgumentException("WrongActionForCtor", "action");
            }

            InitializeAdd(action, null, -1);
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action, IList changedItems)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;
            if (((action != CollectionChangedNotifierAction.Add) && (action != CollectionChangedNotifierAction.Remove)) && (action != CollectionChangedNotifierAction.Reset))
            {
                throw new ArgumentException("Must be Reset, Add, or Remove Action for ctor", "action");
            }
            if (action == CollectionChangedNotifierAction.Reset)
            {
                if (changedItems != null)
                {
                    throw new ArgumentException("Reset Action Requires Null Item", "action");
                }
                InitializeAdd(action, null, -1);
            }
            else
            {
                if (changedItems == null)
                {
                    throw new ArgumentNullException("changedItems");
                }
                InitializeAddOrRemove(action, changedItems, -1);
            }
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action, object changedItem)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;

            if (((action != CollectionChangedNotifierAction.Add) && (action != CollectionChangedNotifierAction.Remove)) && (action != CollectionChangedNotifierAction.Reset))
            {
                throw new ArgumentException("Must be Reset, Add, or Remove Action for ctor", "action");
            }
            if (action == CollectionChangedNotifierAction.Reset)
            {
                if (changedItem != null)
                {
                    throw new ArgumentException("Reset Action Requires Null Item", "action");
                }
                InitializeAdd(action, null, -1);
            }
            else
            {
                InitializeAddOrRemove(action, new object[] { changedItem }, -1);
            }
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action, IList newItems, IList oldItems)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;

            if (action != CollectionChangedNotifierAction.Replace)
            {
                throw new ArgumentException("Wrong Action for ctor", "action");
            }

            if (newItems == null)
            {
                throw new ArgumentNullException("newItems");
            }

            if (oldItems == null)
            {
                throw new ArgumentNullException("oldItems");
            }

            InitializeMoveOrReplace(action, newItems, oldItems, -1, -1);
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action, IList changedItems, int startingIndex)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;
            if (((action != CollectionChangedNotifierAction.Add) && (action != CollectionChangedNotifierAction.Remove)) && (action != CollectionChangedNotifierAction.Reset))
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor", "action");
            }
            if (action == CollectionChangedNotifierAction.Reset)
            {
                if (changedItems != null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem", "action");
                }
                if (startingIndex != -1)
                {
                    throw new ArgumentException("ResetActionRequiresIndexMinus1", "action");
                }
                InitializeAdd(action, null, -1);
            }
            else
            {
                if (changedItems == null)
                {
                    throw new ArgumentNullException("changedItems");
                }
                if (startingIndex < -1)
                {
                    throw new ArgumentException("IndexCannotBeNegative", "startingIndex");
                }
                InitializeAddOrRemove(action, changedItems, startingIndex);
            }
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action, object changedItem, int index)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;
            if (((action != CollectionChangedNotifierAction.Add) && (action != CollectionChangedNotifierAction.Remove)) && (action != CollectionChangedNotifierAction.Reset))
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor", "action");
            }
            if (action == CollectionChangedNotifierAction.Reset)
            {
                if (changedItem != null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem", "action");
                }
                if (index != -1)
                {
                    throw new ArgumentException("ResetActionRequiresIndexMinus1", "action");
                }
                InitializeAdd(action, null, -1);
            }
            else
            {
                InitializeAddOrRemove(action, new object[] { changedItem }, index);
            }
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action, object newItem, object oldItem)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;
            if (action != CollectionChangedNotifierAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor", "action");
            }
            InitializeMoveOrReplace(action, new object[] { newItem }, new object[] { oldItem }, -1, -1);
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action, IList newItems, IList oldItems, int startingIndex)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;
            if (action != CollectionChangedNotifierAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor", "action");
            }
            if (newItems == null)
            {
                throw new ArgumentNullException("newItems");
            }
            if (oldItems == null)
            {
                throw new ArgumentNullException("oldItems");
            }
            InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action, IList changedItems, int index, int oldIndex)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;
            if (action != CollectionChangedNotifierAction.Move)
            {
                throw new ArgumentException("WrongActionForCtor", "action");
            }
            if (index < 0)
            {
                throw new ArgumentException("IndexCannotBeNegative", "index");
            }
            InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex);
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action, object changedItem, int index, int oldIndex)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;
            if (action != CollectionChangedNotifierAction.Move)
            {
                throw new ArgumentException("WrongActionForCtor", "action");
            }
            if (index < 0)
            {
                throw new ArgumentException("IndexCannotBeNegative", "index");
            }
            object[] newItems = new object[] { changedItem };

            InitializeMoveOrReplace(action, newItems, newItems, index, oldIndex);
        }

        public NotifyCollectionChangedEventArgs(CollectionChangedNotifierAction action, object newItem, object oldItem, int index)
        {
            _newStartingIndex = -1;
            _oldStartingIndex = -1;
            if (action != CollectionChangedNotifierAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor", "action");
            }
            InitializeMoveOrReplace(action, new object[] { newItem }, new object[] { oldItem }, index, index);
        }

        private void InitializeAdd(CollectionChangedNotifierAction action, IList newItems, int newStartingIndex)
        {
            _action = action;
            _newItems = (newItems == null) ? null : ArrayList.ReadOnly(newItems);
            _newStartingIndex = newStartingIndex;
        }

        private void InitializeAddOrRemove(CollectionChangedNotifierAction action, IList changedItems, int startingIndex)
        {
            if (action == CollectionChangedNotifierAction.Add)
            {
                InitializeAdd(action, changedItems, startingIndex);
            }
            else if (action == CollectionChangedNotifierAction.Remove)
            {
                InitializeRemove(action, changedItems, startingIndex);
            }
            else
            {
                throw new ArgumentException("Unsupported action: {0}", action.ToString());
            }
        }

        private void InitializeMoveOrReplace(CollectionChangedNotifierAction action, IList newItems, IList oldItems, int startingIndex, int oldStartingIndex)
        {
            InitializeAdd(action, newItems, startingIndex);
            InitializeRemove(action, oldItems, oldStartingIndex);
        }

        private void InitializeRemove(CollectionChangedNotifierAction action, IList oldItems, int oldStartingIndex)
        {
            _action = action;
            _oldItems = (oldItems == null) ? null : ArrayList.ReadOnly(oldItems);
            _oldStartingIndex = oldStartingIndex;
        }
    }
}
