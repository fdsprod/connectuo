using System;
using System.IO;
using System.Net;
using ConnectUO.Framework.Tasks;
using ConnectUO.Framework;
using ConnectUO.Framework.IO;

namespace ConnectUO.Web
{
    public class DownloadManager : TaskManager
    {
        private static object _syncRoot = new object();

        public DownloadManager()
            : base(2) { }

        public DownloadManager(int maxDownloads)
            : base(maxDownloads) { }

        protected override void BeginTask(object state)
        {
            DownloadTask task = (DownloadTask)state;
            Exception exception = null;

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            string directory = Path.GetDirectoryName(task.Destination);
            FileSystemHelper.EnsureDirectoryExists(directory);

            Stream input = null;
            Stream writer = new FileStream(task.Destination, FileMode.OpenOrCreate);

            long totalBytesToRecieve = 0;
            long bytesRecieved = 0;

            try
            {
                if (File.Exists(task.Url))
                {
                    input = new FileStream(task.Url, FileMode.Open);
                    totalBytesToRecieve = input.Length;
                }
                else
                {
                    request = (HttpWebRequest)WebRequest.Create(task.Url);
                    request.Timeout = 60000;
                    response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new WebException(String.Format("Error retrieving download.  WebRequest status code = {0}", response.StatusCode));
                    }

                    input = response.GetResponseStream();
                    totalBytesToRecieve = response.ContentLength;
                }

                byte[] buffer = new byte[0x2000];

                bytesRecieved = 0;

                BinaryReader reader = new BinaryReader(input);
                int progress = 0;

                if (!CancelRequested)
                {
                    task.OnProgressStarted(this, new ProgressStartedEventArgs(task));
                }

                while (bytesRecieved < totalBytesToRecieve)
                {
                    if (CancelRequested)
                    {
                        break;
                    }

                    int count = (int)Math.Min((long)buffer.Length, totalBytesToRecieve - bytesRecieved);

                    buffer = reader.ReadBytes(count);
                    writer.Write(buffer, 0, count);
                    bytesRecieved += count;

                    int currentProgress = (int)((100 * bytesRecieved) / totalBytesToRecieve);

                    if (currentProgress != progress)
                    {
                        progress = currentProgress;

                        if (!CancelRequested)
                        {
                            task.OnProgressUpdate(this, new ProgressUpdateEventArgs(task, (int)bytesRecieved, (int)totalBytesToRecieve));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                if (input != null)
                {
                    input.Close();
                }

                if (writer != null)
                {
                    writer.Close();
                }

                if (!CancelRequested)
                {
                    task.OnProgressCompleted(this, new ProgressCompletedEventArgs(exception, CancelRequested, task, (int)bytesRecieved, (int)totalBytesToRecieve));
                }
            }

            base.BeginTask(state);
        }
    }
}
