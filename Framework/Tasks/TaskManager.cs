using System;
using System.Collections.Generic;
using System.Threading;

namespace ConnectUO.Framework.Tasks
{
    public abstract class TaskManager : IProgressNotifier, IStatusNotifier
    {
        private int _maxTasks;
        private int _tasksInProgress;
        private int _tasksComplete;
        private int _totaltasks;

        protected bool CancelRequested;

        private Queue<ITask> _taskQueue;

        public bool HasBegun
        {
            get { return _tasksComplete > 0 || _tasksInProgress > 0; }
        }

        public event EventHandler<ProgressUpdateEventArgs> ProgressUpdate;
        public event EventHandler<ProgressStartedEventArgs> ProgressStarted;
        public event EventHandler<ProgressCompletedEventArgs> ProgressCompleted;
        public event EventHandler<StatusUpdateEventArgs> StatusUpdate;

        public TaskManager()
            : this(5) { }

        public TaskManager(int maxTasks)
        {
            maxTasks = Math.Max(maxTasks, 1);

            _maxTasks = maxTasks;
            _taskQueue = new Queue<ITask>();
        }

        public void Queue(ITask task)
        {
            _taskQueue.Enqueue(task);
            _totaltasks++;

            if (HasBegun)
            {
                ProcessNext();
            }
        }

        public void Begin()
        {
            if (!HasBegun)
            {
                OnProgressStarted(this, new ProgressStartedEventArgs());
            }

            ProcessNext();
        }

        private void ProcessNext()
        {
            //Nothing queued, nothing in progress
            if (CancelRequested || _taskQueue.Count == 0 && _tasksInProgress == 0)
            {
                OnProgressCompleted(this, new ProgressCompletedEventArgs(CancelRequested, _tasksComplete, _totaltasks));
            }
            //We arent maxed, and there are tasks still queued
            else if (_tasksInProgress < _maxTasks && _taskQueue.Count > 0)
            {
                _tasksInProgress++;

                ITask state = _taskQueue.Dequeue();
                ThreadPool.QueueUserWorkItem(BeginTask, state);

                ProcessNext();
            }
            //Do nothing, once one of the current task 
            //is compelte, the next will begin
            else
            {

            }
        }

        protected virtual void BeginTask(object state)
        {
            _tasksInProgress--;
            _tasksComplete++;

            OnProgressUpdate(this, new ProgressUpdateEventArgs(_tasksComplete, _totaltasks));
            ProcessNext();
        }

        public void CancleAll()
        {
            CancelRequested = true;
        }

        protected virtual void OnProgressStarted(object sender, ProgressStartedEventArgs e)
        {
            if (ProgressStarted != null)
            {
                ProgressStarted(sender, e);
            }
        }

        protected virtual void OnProgressUpdate(object sender, ProgressUpdateEventArgs e)
        {
            if (ProgressUpdate != null)
            {
                ProgressUpdate(sender, e);
            }
        }

        protected virtual void OnProgressCompleted(object sender, ProgressCompletedEventArgs e)
        {
            if (ProgressCompleted != null)
            {
                ProgressCompleted(sender, e);
            }
        }
    }
}
