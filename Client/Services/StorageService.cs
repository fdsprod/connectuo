using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ConnectUO.Framework.Ultima;
using ConnectUO.Framework.Web;
using ConnectUO.Framework;
using ConnectUO.Framework.Diagnostics;
using ConnectUO.Framework.Extensions;
using Microsoft.Win32;
using System.Windows.Forms;
using ConnectUO.Framework.Data;
using System.Data.Objects;
using ConnectUO.Framework.Windows;
using ConnectUO.Framework.Services;
using Ninject;

namespace ConnectUO.Data
{
    public class StorageService :  IStorageService, IDisposable
    {
        static object _syncRoot = new object();

        private ConnectUODataContext _context;
        private ConnectUOWebService _service;
        private IApplicationService _applicationService;

        public Server[] PublicServers
        {
            get
            {
                return (from s in _context.ServerContext 
                        where s.Public == true 
                        select s).ToArray();
            }
        }
        
        public Server[] FavoriteServers
        {
            get
            {
                return (from s in _context.ServerContext
                        where s.Favorite 
                        select s).ToArray();
            }
        }

        public Server[] LocalServers
        {
            get
            {
                return (from s in _context.ServerContext 
                        where !s.Public
                        select s).ToArray();
            }
        }

        public ServerPatch[] ServerPatches
        {
            get
            {
                return (from s in _context.ServerPatchContext select s).ToArray();
            }
        }

        public event EventHandler<EventArgs> ServersUpdateComplete;
        public event EventHandler<EventArgs> ServersUpdateBegin;
        public event EventHandler<StorageSerErrorEventArgs> Error;

        [Inject]
        public StorageService(IApplicationService applicationService)
        {
            _applicationService = applicationService;

            _context = new ConnectUODataContext();
            _context.Connection.Open();

            _service = new ConnectUOWebService();
            _service.EnableDecompression = true;

            _service.GetLatestVersionCompleted += new GetLatestVersionCompletedEventHandler(OnGetLatestVersionCompleted);
            _service.GetPatchesCompleted += new GetPatchesCompletedEventHandler(OnGetPatchesCompleted);
            _service.GetPublicServersCompleted += new GetPublicServersCompletedEventHandler(OnGetPublicServersCompleted);
            _service.GetServerInformationCompleted += new GetServerInformationCompletedEventHandler(OnGetServerInformationCompleted);
            _service.TestConnectionCompleted += new TestConnectionCompletedEventHandler(OnTestConnectionCompleted);
            _service.TrackUsageCompleted += new TrackUsageCompletedEventHandler(OnTrackUsageCompleted);
            _service.UpdatePlayStatisticsCompleted += new UpdatePlayStatisticsCompletedEventHandler(OnUpdatePlayStatisticsCompleted);
            _service.UpdateVersionStatsCompleted += new UpdateVersionStatsCompletedEventHandler(OnUpdateVersionStatsCompleted);
        }

        public void Dispose()
        {
            Tracer.Verbose("Disposing Data Service...");

            _context.Connection.Close();
            _context.Dispose();
        }

        private void UpdatePublicServer(Server server, DataRow data)
        {
            server.AllowRazor = data.Field<bool>("AllowRazor");
            server.AvgOnline = data.Field<int>("AvgOnline");
            server.CurOnline = data.Field<int>("CurOnline");
            server.Data = data.Field<byte[]>("Data");
            server.Description = data.Field<string>("Description");
            server.Era = data.Field<int>("Era");
            server.HasPatches = data.Field<int>("HasPatches") == 0 ? false : true;
            server.HostAddress = data.Field<string>("HostAddress");
            server.Lang = data.Field<int>("Lang");
            server.MaxOnline = data.Field<int>("MaxOnline");
            server.Name = data.Field<string>("Name");
            server.Port = data.Field<int>("Port");
            server.RemoveEncryption = data.Field<bool>("RemoveEncryption");
            server.ShardType = data.Field<int>("ShardType");
            server.Status = data.Field<int>("Status");
            server.ServerClientVersion = data.Field<string>("ServerClientVersion");
            server.UpTime = (float)data.Field<double>("UpTime");
            server.Url = data.Field<string>("Url");
        }

        private void ValidateServers(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                Server[] servers = PublicServers;
                bool save = false;

                foreach (DataRow data in dataTable.Rows)
                {
                    Server server = (from s in servers
                                     where data.Field<int>("Id") == s.Id
                                     select s).FirstOrDefault();

                    bool createNew = (server == null);

                    if (createNew)
                    {
                        save = true;
                        server = Server.CreateServer((long)data.Field<int>("Id"),
                                                     data.Field<string>("Name"),
                                                     data.Field<string>("Url"),
                                                     data.Field<int>("Era"),
                                                     data.Field<int>("ShardType"),
                                                     data.Field<int>("Lang"),
                                                     data.Field<bool>("RemoveEncryption"),
                                                     data.Field<bool>("AllowRazor"),
                                                     data.Field<string>("HostAddress"),
                                                     data.Field<int>("Port"),
                                                     data.Field<int>("Status"),
                                                     data.Field<int>("AvgOnline"),
                                                     data.Field<int>("MaxOnline"),
                                                     data.Field<int>("CurOnline"),
                                                     data.Field<double>("UpTime"),
                                                     data.Field<int>("HasPatches") == 0 ? false : true,
                                                     true,
                                                     false);
                        server.Data = data.Field<byte[]>("Data");
                        server.Description = data.Field<string>("Description");
                        server.Status = data.Field<int>("Status");
                        server.ServerClientVersion = data.Field<string>("ServerClientVersion");

                        save = true;
                        Tracer.Verbose("Adding server id:{0}, name:{1}", server.Id, server.Name);
                        _context.AddToServerContext(server);
                    }
                    else
                    {
                        server.PropertyChanged += (sender, e) => { save = true; };
                        Tracer.Verbose("Updated server id:{0}, name:{1}", server.Id, server.Name);
                        UpdatePublicServer(server, data);
                    }
                }

                if (save)
                {
                    try
                    {
                        _context.SaveChanges(true);
                    }
                    catch (Exception e)
                    {
                        Tracer.Fatal(e);
                    }
                }
            }
        }

        public void SetConfigValue<T>(string key, T value)
        {            
           Config config = (from c in _context.ConfigContext where c.Key == key select c).FirstOrDefault();

           if (config == null)
           {
               config = Config.CreateConfig(key);
               _context.AddToConfigContext(config);
           }

            config.Value = value.ToString();
            _context.SaveChanges();
        }

        public T GetConfigValue<T>(string key)
        {
            Config config = (from c in _context.ConfigContext where c.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase) select c).FirstOrDefault();

            if (config == null)
            {
                return default(T);
            }

            return config.Value.ConvertTo<T>();
        }

        public void UpdateServers()
        {
            UpdateServers(null);
        }

        public void UpdateServers(CallbackState state)
        {
            if (state == null)
            {
                state = new CallbackState(null);
            }

            CallbackState testState = new CallbackState(InternalUpdateServers);
            testState.Tag = state;

            _service.TestConnectionAsync(testState);
        }

        public ServerPatch[] GetPatches(int id)
        {
            DataSet data = _service.GetPatches(id);
            ServerPatch[] patches = null;

            if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
            {
                DataTable dt = data.Tables[0];
                patches = new ServerPatch[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    patches[i] = new ServerPatch();

                    patches[i].ShardId = id;
                    patches[i].PatchUrl = dt.Rows[i].Field<string>("PatchUrl");
                    patches[i].Version = dt.Rows[i].Field<int>("Version");
                }
            }

            return patches;
        }

        protected virtual void OnUpdateVersionStatsCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(this, new StorageSerErrorEventArgs(e.Error));
            }
        }

        protected virtual void OnUpdatePlayStatisticsCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(this, new StorageSerErrorEventArgs(e.Error));
            }
        }

        protected virtual void OnTrackUsageCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(this, new StorageSerErrorEventArgs(e.Error));
            }
        }

        protected virtual void OnTestConnectionCompleted(object sender, TestConnectionCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(this, new StorageSerErrorEventArgs(e.Error));
            }
            if (!e.Cancelled)
            {
                CallbackState state = e.UserState as CallbackState;

                if (state != null && state.Callback != null)
                {
                    state.Callback(state, EventArgs.Empty);
                }
            }
        }

        protected virtual void OnGetServerInformationCompleted(object sender, GetServerInformationCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(this, new StorageSerErrorEventArgs(e.Error));
            }
            else if (!e.Cancelled)
            {
                if (e.Result.Tables.Count > 0 && e.Result.Tables[0].Rows.Count > 0)
                {
                    DataRow data = e.Result.Tables[0].Rows[0];

                    Server server = (from s in PublicServers
                                     where data.Field<int>("Id") == s.Id
                                     select s).FirstOrDefault();

                    if (server != null)
                    {
                        UpdatePublicServer(server, data);
                        _context.SaveChanges();
                    }
                }
            }

            CallbackState state = e.UserState as CallbackState;

            if (state != null && state.Callback != null)
            {
                state.Callback(this, EventArgs.Empty);
            }
        }

        protected virtual void OnGetPublicServersCompleted(object sender, GetPublicServersCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(this, new StorageSerErrorEventArgs(e.Error));
            }
            else if (!e.Cancelled)
            {
                if (e.Result.Tables.Count > 0)
                {
                    int[] ids = (from d in e.Result.Tables[0].Select() select d.Field<int>("Id")).ToArray();

                    DropPublicServers(ids);
                    DropFavoriteServers(ids);

                    ValidateServers(e.Result.Tables[0]);
                }

                OnServerUpdateComplete(this, EventArgs.Empty);
            }

            CallbackState state = e.UserState as CallbackState;

            if (state != null && state.Callback != null)
            {
                state.Callback(this, EventArgs.Empty);
            }
        }

        protected virtual void OnError(object sender, StorageSerErrorEventArgs e)
        {
            if (Error != null)
            {
                Error(sender, e);
            }
        }

        protected virtual void OnServerUpdateBegin(object sender, EventArgs e)
        {
            if (ServersUpdateBegin != null)
            {
                ServersUpdateBegin(sender, e);
            }
        }

        protected virtual void OnServerUpdateComplete(object sender, EventArgs e)
        {
            if (ServersUpdateComplete != null)
            {
                ServersUpdateComplete(sender, e);
            }
        }

        public void UpdatePlayStatistics(Guid guid, int id)
        {
            _service.UpdatePlayStatistics(guid, id);
        }

        protected virtual void OnGetPatchesCompleted(object sender, GetPatchesCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(this, new StorageSerErrorEventArgs(e.Error));
            }

            CallbackState state = e.UserState as CallbackState;

            if (state != null && state.Callback != null)
            {
                state.Callback(this, EventArgs.Empty);
            }
        }

        protected virtual void OnGetLatestVersionCompleted(object sender, GetLatestVersionCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(this, new StorageSerErrorEventArgs(e.Error));
            }
            else if (!e.Cancelled)
            {
                if(e.Result.Tables.Count > 0 && e.Result.Tables[0].Rows.Count > 0)
                {
                    Version version = new Version(e.Result.Tables[0].Rows[0].Field<string>("Version"));

                    if (_applicationService.ApplicationInfo.Version.CompareTo(version) < 0)
                    {
                        Process.Start(Path.Combine(_applicationService.ApplicationInfo.BaseDirectory, "update.exe"));
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }

            CallbackState state = e.UserState as CallbackState;

            if (state != null && state.Callback != null)
            {
                state.Callback(this, EventArgs.Empty);
            }
        }

        private void InternalUpdateServers(object sender, EventArgs e)
        {
            //Sender should be the callback state being invoked
            CallbackState testState = sender as CallbackState;

            if (testState != null)
            {
                //Tag should be the original callback state.
                CallbackState state = testState.Tag as CallbackState;

                if (state != null)
                {
                    OnServerUpdateBegin(this, EventArgs.Empty);
                    _service.GetPublicServersAsync(state);
                }
            }
        }

        private void DropFavoriteServers(int[] validIds)
        {
            if(validIds.Length > 0)
            {
                Server[] remove =
                    (from f in FavoriteServers
                     where !(from id in validIds select id).Contains((int)f.Id)
                     select f).ToArray();

                for (int i = 0; i < remove.Length; i++)
                {
                    _context.DeleteObject(remove[i]);
                }

                _context.SaveChanges();
            }
        }

        private void DropPublicServers(int[] validIds)
        {
            if (validIds.Length > 0)
            {
                Server[] remove =
                    (from p in PublicServers
                     where !(from id in validIds select id).Contains((int)p.Id)
                     select p).ToArray();

                for (int i = 0; i < remove.Length; i++)
                {
                    _context.DeleteObject(remove[i]);
                }

                _context.SaveChanges();
            }
        }

        public LocalPatch[] GetLocalPatches(int id)
        {
            return (from p in _context.LocalPatchContext where p.ShardId == id select p).ToArray();
        }

        public void DeleteLocalPatches(int id)
        {
            LocalPatch[] patches = GetLocalPatches(id);
            ServerPatch[] applied = (from s in _context.ServerPatchContext where s.ShardId == id select s).ToArray();

            if (patches.Length > 0 || applied.Length > 0)
            {
                for (int i = 0; i < patches.Length; i++)
                {
                    _context.DeleteObject(patches[i]);
                }

                for (int i = 0; i < applied.Length; i++)
                {
                    _context.DeleteObject(applied[i]);
                }

                _context.SaveChanges();
            }
        }

        public void AddLocalPatch(int id, string url, int version)
        {
            LocalPatch patch = LocalPatch.CreateLocalPatch(id, url, version);

            _context.AddToLocalPatchContext(patch);
            _context.SaveChanges();
        }

        public void AddServer(Server server)
        {
            _context.AddToServerContext(server);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void DeleteServer(Server server)
        {
            _context.DeleteObject(server);
            _context.SaveChanges();
        }

        public void CheckForUpdates(CallbackState state)
        {
            if (state == null)
            {
                state = new CallbackState(null);
            }

            _service.GetLatestVersionAsync(state);
        }

        public void SetServerPatchState(Server server)
        {
            ServerPatchState state = (from p in _context.ServerPatchStateContext where p.ServerId == server.Id select p).FirstOrDefault();

            if(state == null)
            {
                state = ServerPatchState.CreateServerPatchState(server.Id, true);
                _context.AddToServerPatchStateContext(state);
            }

            state.Patching = true;
            _context.SaveChanges();
        }

        public bool IsServerBeingPatched(int id)
        {            
            ServerPatchState state = (from p in _context.ServerPatchStateContext where p.ServerId == id select p).FirstOrDefault();

            if(state == null)
            {
                return false;
            }

            return state.Patching;
        }

        public void SetPatchApplied(ServerPatch patch)
        {
            _context.AddToServerPatchContext(patch);
            _context.SaveChanges();
        }

        public bool IsPatchApplied(ServerPatch patch)
        {
            return (from p in _context.ServerPatchContext 
                    where p.ShardId == patch.ShardId
                    && p.PatchUrl == patch.PatchUrl 
                    && p.Version == patch.Version
                    select p).Count() > 0;
        }

        public void ResetPatches(int id)
        {
            ServerPatch[] patches = (from p in _context.ServerPatchContext where p.ShardId == id select p).ToArray();
            
            if (patches.Length > 0)
            {
                for (int i = 0; i < patches.Length; i++)
                {
                    _context.DeleteObject(patches[i]);
                }

                _context.SaveChanges();
            }
        }

        public bool ServerIsCurrentlyBeingPlayed(Server server)
        {
            ClientProcess[] clientProcesses = (from p in _context.ClientProcessContext where p.ServerId == server.Id select p).ToArray();

            bool isBeingPlayed = false;

            for (int i = 0; i < clientProcesses.Length && !isBeingPlayed; i++)
            {
                Process process = null;

                try
                {
                    process = Process.GetProcessById((int)clientProcesses[i].Pid);

                }
                catch (Exception ex)
                {
                    _context.DeleteObject(clientProcesses[i]);
                    Tracer.Info(ex);
                }

                if (process != null)
                {
                    isBeingPlayed = true;
                }
            }

            _context.SaveChanges();

            return isBeingPlayed;
        }

        public void LogProcess(int id, uint pid)
        {
            ClientProcess process = ClientProcess.CreateClientProcess(id, pid);

            _context.AddToClientProcessContext(process);
            _context.SaveChanges();
        }

        public void CancelServiceCall(object state)
        {
            _service.CancelAsync(state);
        }
    }
}
