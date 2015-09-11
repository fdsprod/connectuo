using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ConnectUO.Framework.Configuration
{
    public sealed class TracerConfigurationSection : ConfigurationSection
    {
        private static ConfigurationProperty _tracerCollectionProperty;
        private static ConfigurationPropertyCollection _properties;

        static TracerConfigurationSection()
        {
            _tracerCollectionProperty = 
                new ConfigurationProperty(
                    string.Empty, 
                    typeof(TracerElementCollection),
                    null ,
                    ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsDefaultCollection);

            _properties = new ConfigurationPropertyCollection();

            _properties.Add(_tracerCollectionProperty);
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _properties; }
        }

        public TracerElementCollection Tracers
        {
            get { return (TracerElementCollection)base[_tracerCollectionProperty]; }
        }

    }
}
