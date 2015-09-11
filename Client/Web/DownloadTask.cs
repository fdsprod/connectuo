using ConnectUO.Framework.Tasks;

namespace ConnectUO.Web
{
    public class DownloadTask : Task
    {
        private string _url;
        private string _destination;

        public string Destination
        {
            get { return _destination; }
        }

        public string Url
        {
            get { return _url; }
        }

        public DownloadTask(string url, string destination)
        {
            _url = url;
            _destination = destination;
        }
    }
}
