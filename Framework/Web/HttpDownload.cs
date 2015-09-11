/********************************************************
 * 
 *  $Id: HttpDownload.cs 63 2009-11-02 22:14:43Z jeff $
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
using System.IO;
using System.Net;
using System.Threading;
using ConnectUO.Framework;

namespace ConnectUO.Framework.Web
{
    public sealed class HttpDownload : IProgressNotifier
    {
        public static HttpDownload CreateDownload(string url, string destination)
        {
            return new HttpDownload(url, destination);
        }
        public static HttpDownload CreateDownload(string url, Stream stream)
        {
            return new HttpDownload(url, stream);
        }

        private string url;
        private Stream stream;
        private Thread thread;

        public bool IsAlive
        {
            get { return thread.IsAlive; }
        }

        private HttpDownload(string url, string destination)
            : this(url, new FileStream(destination, FileMode.OpenOrCreate)) { }

        private HttpDownload(string url, Stream stream)
        {
            this.url = url;
            this.stream = stream;
            this.thread = new Thread(InternalBegin);
        }

        public void Begin()
        {
            if (IsAlive)
            {
                return;
            }

            if (stream == null)
            {
                throw new NullReferenceException("stream");
            }

            thread.Start();
        }
        public void End()
        {
            if (IsAlive)
            {
                thread.Abort();
            }
        }

        private void InternalBegin()
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream input = null;
            Exception exception = null;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 0x2710;
                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new WebException(String.Format("Request status code = {0}", response.StatusCode));
                }

                input = response.GetResponseStream();

                byte[] buffer = new byte[0x2000];

                long totalBytesToRecieve = response.ContentLength;
                long bytesRecieved = 0;

                BinaryReader reader = new BinaryReader(input);
                int progress = 0;

                OnStarted(this, new ProgressStartedEventArgs());

                while (bytesRecieved < totalBytesToRecieve)
                {
                    int count = (int)Math.Min((long)buffer.Length, totalBytesToRecieve - bytesRecieved);

                    buffer = reader.ReadBytes(count);
                    stream.Write(buffer, 0, count);
                    bytesRecieved += count;
                    
                    int currentProgress = (int)((100 * bytesRecieved) / totalBytesToRecieve);

                    if (currentProgress != progress)
                    {
                        progress = currentProgress;
                        OnProgressUpdate(this,
                            new ProgressUpdateEventArgs((int)bytesRecieved, (int)totalBytesToRecieve));
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
                if (stream != null)
                {
                    stream.Close();
                }

                OnCompleted(this, new ProgressCompletedEventArgs(exception, false, null, 0, 0));
            }
        }

        public event EventHandler<ProgressUpdateEventArgs> ProgressUpdate;
        public event EventHandler<ProgressStartedEventArgs> ProgressStarted;
        public event EventHandler<ProgressCompletedEventArgs> ProgressCompleted;

        private void OnStarted(object sender, ProgressStartedEventArgs e)
        {
            if (ProgressStarted != null)
            {
                ProgressStarted(sender, e);
            }
        }
        private void OnProgressUpdate(object sender, ProgressUpdateEventArgs e)
        {
            if (ProgressUpdate != null)
            {
                ProgressUpdate(sender, e);
            }
        }
        private void OnCompleted(object sender, ProgressCompletedEventArgs e)
        {
            if (ProgressCompleted != null)
            {
                ProgressCompleted(sender, e);
            }
        }
    }
}
