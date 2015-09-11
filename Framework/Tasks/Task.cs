using System;

namespace ConnectUO.Framework.Tasks
{
    public abstract class Task : ITask, IProgressNotifier, IStatusNotifier
    {
        public event EventHandler<ProgressUpdateEventArgs> ProgressUpdate;
        public event EventHandler<ProgressStartedEventArgs> ProgressStarted;
        public event EventHandler<ProgressCompletedEventArgs> ProgressCompleted;
        public event EventHandler<StatusUpdateEventArgs> StatusUpdate;

        public void OnProgressStarted(object sender, ProgressStartedEventArgs e)
        {
            if (ProgressStarted != null)
            {
                ProgressStarted(sender, e);
            }
        }

        public void OnProgressUpdate(object sender, ProgressUpdateEventArgs e)
        {
            if (ProgressUpdate != null)
            {
                ProgressUpdate(sender, e);
            }
        }

        public void OnProgressCompleted(object sender, ProgressCompletedEventArgs e)
        {
            if (ProgressCompleted != null)
            {
                ProgressCompleted(sender, e);
            }
        }
    }
}
