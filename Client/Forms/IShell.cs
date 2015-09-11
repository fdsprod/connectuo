using System;
namespace ConnectUO.Forms
{
    interface IShell
    {
        string Text { get; set; }
        void SetStatus(string status, params object[] args);
    }
}
