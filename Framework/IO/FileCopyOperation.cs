using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using ConnectUO.Framework.Diagnostics;

namespace ConnectUO.Framework.IO
{
    public sealed class FileCopyOperation : OperationBase
    {
        private readonly int _bufferSize = 1024 * 128;

        private string _source;
        private string _destination;

        public FileCopyOperation(string source, string destination)
        {
            _source = source;
            _destination = destination;
        }

        protected override void BeginOverride()
        {
            try
            {
                if (!File.Exists(_source))
                    throw new FileNotFoundException(_source);

                FileSystemHelper.EnsureDirectoryExists(_destination);

                byte[] buffer = new byte[_bufferSize];

                using (FileStream sourceStream = new FileStream(_source, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (FileStream destStream = new FileStream(_destination, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                    {
                        OnProgressChanged(0, (int)sourceStream.Length);

                        while (sourceStream.Position < sourceStream.Length)
                        {
                            int remainingLength = (int)(sourceStream.Length - sourceStream.Position);
                            int bytesToRead = Math.Min(buffer.Length, remainingLength);

                            sourceStream.Read(buffer, 0, bytesToRead);
                            destStream.Write(buffer, 0, bytesToRead);

                            OnProgressChanged((int)sourceStream.Position, (int)sourceStream.Length);
                        }

                        OnProgressChanged((int)sourceStream.Length, (int)sourceStream.Length);
                    }
                }

                OnComplete();
            }
            catch (Exception e)
            {
                Tracer.Error(e);
                OnError(e);
            }

            End();
        }
    }
}
