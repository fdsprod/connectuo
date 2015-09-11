using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConnectUO.Framework.Services
{
    public interface ISiteService
    {
        void Register<T>(string siteName, T control) where T : Control;
        void Remove(string siteName);

        T Get<T>(string siteName) where T : Control;
    }
}
