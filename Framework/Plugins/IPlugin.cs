using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ConnectUO.Framework.Data;
using ConnectUO.Framework;
using ConnectUO.Framework.Services;
using Ninject;

namespace ConnectUO.Framework.Plugins
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        string Author { get; }
        Version Version { get; }

        void Initialize();
    }

    public abstract class BasePlugin : IPlugin
    {
        private IStorageService _storageService;

        public IStorageService StorageService
        {
            get { return _storageService; }
        }

        [Inject]
        public BasePlugin(IKernel _kernel)
        {
            _storageService = _kernel.Get<IStorageService>();
        }

        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Author { get; }
        public abstract Version Version { get; }

        public virtual void Initialize()
        {
        }
    }

    public interface IPluginService// : IService
    {

    }

    public class PluginManager : IPluginService
    {
        private List<IPlugin> _plugins;

        public List<IPlugin> Plugins
        {
            get { return _plugins; }
        }

        public PluginManager()
        {
            _plugins = new List<IPlugin>();
        }

        public void LoadPlugins(string directory)
        {
            if (Directory.Exists(directory))
            {
                DirectoryInfo dir = new DirectoryInfo(directory);
                FileInfo[] dlls = dir.GetFiles("*.dll");

                for (int i = 0; i < dlls.Length; i++)
                {
                    Assembly assembly = Assembly.LoadFile(dlls[i].FullName);

                    Type[] types = assembly.GetTypes();
                    Type[] plugins = (from t in types
                                      where t.GetInterface("ConnectUO.Framework.Plugins.IPlugin", true) != null 
                                      select t).ToArray();

                    for (int j = 0; j < plugins.Length; j++)
                    {
                        try
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(plugins[i]);
                        }
                        catch //(Exception e)
                        {
                            
                        }
                    }
                }
            }
        }
    }
}
