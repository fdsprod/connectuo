using ConnectUO.Framework.Tasks;

namespace ConnectUO.IO.Compression
{
    public class ExtractionTask : Task
    {
        private string _archive;
        private string _destination;

        public string Destination
        {
            get { return _destination; }
        }

        public string Archive
        {
            get { return _archive; }
        }

        public ExtractionTask(string archive, string destination)
        {
            _archive = archive;
            _destination = destination;
        }
    }
}
