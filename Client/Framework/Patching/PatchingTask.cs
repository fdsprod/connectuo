using Fds.Core.TaskFramework;

namespace ConnectUO.Framework.Patching
{
    public class PatchingTask : Task
    {
        private Patch[] _patches;
        private string _serverDirectory;
        private string _patchType;

        public string PatchType
        {
            get { return _patchType; }
            set { _patchType = value; }
        }

        public string ServerDirectory
        {
            get { return _serverDirectory; }
        }

        public Patch[] Patches
        {
            get { return _patches; }
        }

        public PatchingTask(Patch[] patches, string serverDirectory, string patchType)
        {
            _patches = patches;
            _serverDirectory = serverDirectory;
            _patchType = patchType;
        }
    }
}
