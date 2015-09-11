using System;
using ConnectUO.Framework;
using ConnectUO.Framework.Data;

namespace ConnectUO.Framework.Services
{
    public interface IStorageService : IDisposable
    {
        Server[] PublicServers { get; }
        Server[] FavoriteServers { get; }
        Server[] LocalServers { get; }
        ServerPatch[] ServerPatches { get; }

        void SetConfigValue<T>(string key, T value);
        T GetConfigValue<T>(string key);

        event EventHandler<EventArgs> ServersUpdateBegin;
        event EventHandler<EventArgs> ServersUpdateComplete;
        event EventHandler<StorageSerErrorEventArgs> Error;

        void CheckForUpdates(CallbackState state);
        void CancelServiceCall(object state);

        void AddLocalPatch(int id, string url, int version);
        void AddServer(Server server);

        void DeleteLocalPatches(int id);
        void DeleteServer(Server server);

        LocalPatch[] GetLocalPatches(int id);
        ServerPatch[] GetPatches(int id);

        void LogProcess(int id, uint pid);

        bool IsServerBeingPatched(int id);
        bool IsPatchApplied(ServerPatch patch);

        void ResetPatches(int id);

        void SaveChanges();
        bool ServerIsCurrentlyBeingPlayed(Server server);
        void SetPatchApplied(ServerPatch patch);
        void SetServerPatchState(Server server);

        void UpdateServers();
        void UpdateServers(CallbackState state);
        void UpdatePlayStatistics(Guid guid, int id);
    }
}
