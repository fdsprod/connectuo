using System;
using ConnectUO.Framework;
using ConnectUO.Framework.Tasks;
using Ninject;

namespace ConnectUO.Framework.Patching
{
    public class PatchManager : TaskManager
    {
        private static object _syncRoot = new object();

        private IKernel _kernel;

        public PatchManager()
            : base(5) { }

        public PatchManager(IKernel kernel)
        {
            _kernel = kernel;
        }
        
        protected override void BeginTask(object state)
        {
            PatchingTask task = (PatchingTask)state;
            Exception exception = null;

            try
            {
                if (!CancelRequested)
                {
                    task.OnProgressStarted(this, new ProgressStartedEventArgs(task));
                }

                Patcher patcher = _kernel.Get<Patcher>();

                patcher.Patches = task.Patches;
                patcher.ServerFolder = task.ServerDirectory;

                patcher.ProgressChanged +=
                    new EventHandler<PatcherProgressChangeEventArgs>(delegate(object sender, PatcherProgressChangeEventArgs e)
                    {
                        if (!CancelRequested)
                        {
                            task.OnProgressUpdate(this, new ProgressUpdateEventArgs(task, e.PercentComplete, 100));
                        }
                    });


                patcher.WritePatches();
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                if (!CancelRequested)
                {
                    task.OnProgressCompleted(this, new ProgressCompletedEventArgs(exception, CancelRequested, task, 100, 100));
                }
            }

            base.BeginTask(state);
        }
    }
}
