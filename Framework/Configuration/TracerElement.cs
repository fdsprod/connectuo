using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ConnectUO.Framework.Configuration
{
    public sealed class TracerElement : ConfigurationElement
    {
        private static ConfigurationProperty _typeProperty;
        private static ConfigurationProperty _traceLevelProperty;
        private static ConfigurationPropertyCollection _properties;

        static TracerElement()
        {
            _typeProperty = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
            _traceLevelProperty = new ConfigurationProperty("traceLevel", typeof(string), "Verbose", ConfigurationPropertyOptions.None);

            _properties = new ConfigurationPropertyCollection();

            _properties.Add(_typeProperty);
            _properties.Add(_traceLevelProperty);
        }

        [ConfigurationProperty("type")]
        public string Type
        {
            get { return (string)base[_typeProperty]; }
        }

        [ConfigurationProperty("traceLevel")]
        public string TraceLevel
        {
            get { return (string)base[_traceLevelProperty]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _properties; }
        }
    }
}
