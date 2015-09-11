using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ConnectUO.Framework.Configuration
{
    [ConfigurationCollection(typeof(TracerElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public sealed class TracerElementCollection : ConfigurationElementCollection
    {
        private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _properties; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        public TracerElement this[int index]
        {
            get { return (TracerElement)base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                    base.BaseRemoveAt(index);

                base.BaseAdd(index, value);
            }
        }

        public TracerElement this[string key]
        {
            get { return (TracerElement)base.BaseGet(key); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TracerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as TracerElement).Type;
        }
    }
}
