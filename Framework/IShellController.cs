using System;

namespace ConnectUO.Framework
{
    public interface IShellController
    {
        Views CurrentView { get; set; }
        event EventHandler CurrentViewChanged;
        void Initialize();
    }
}
