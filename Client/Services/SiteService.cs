using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectUO.Framework.Services;
using System.Windows.Forms;

namespace ConnectUO.Services
{
    public class SiteService : ISiteService
    {
        private Dictionary<string, Control> _sites;

        public SiteService()
        {
            _sites = new Dictionary<string, Control>();
        }

        public void Register<T>(string siteName, T control) where T : System.Windows.Forms.Control
        {
            if (_sites.ContainsKey(siteName))
                throw new ArgumentException(string.Format("The site name '{0}' already exists."), siteName);

            _sites.Add(siteName, control);
        }

        public T Get<T>(string siteName) where T : System.Windows.Forms.Control
        {
            return (T)_sites[siteName];
        }

        public void Remove(string siteName)
        {
            if (_sites.ContainsKey(siteName))
            {
                _sites.Remove(siteName);
            }            
        }
    }
}
