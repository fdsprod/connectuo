using System;
using SevenZip;
using ConnectUO.Framework.Tasks;
using ConnectUO.Framework;

namespace ConnectUO.IO.Compression
{
    public class ExtractionManager : TaskManager
    {
        private static object _syncRoot = new object();

        public ExtractionManager()
            : base(5) { }

        public ExtractionManager(int maxExtactions)
            : base(maxExtactions)
        {

        }

        protected override void BeginTask(object state)
        {
            ExtractionTask task = (ExtractionTask)state;
            Exception exception = null;

            try
            {
                if (!CancelRequested)
                {
                    task.OnProgressStarted(this, new ProgressStartedEventArgs(task));
                }

                SevenZipExtractor extractor = new SevenZipExtractor(task.Archive);

                extractor.Extracting += new EventHandler<SevenZip.ProgressEventArgs>(delegate(object sender, SevenZip.ProgressEventArgs e)
                {
                    if (!CancelRequested)
                    {
                        task.OnProgressUpdate(this, new ProgressUpdateEventArgs(task, e.PercentDone, 100));
                    }
                });

                extractor.ExtractArchive(task.Destination);
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
