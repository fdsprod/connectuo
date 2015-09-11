using System;
using System.Xml.Serialization;

namespace ConnectUO.Framework.Patching
{
    [Serializable]
    public class ShardPatch
    {
        private string patchUrl;
        private int version;

        [XmlElement(ElementName = "Version", IsNullable = false)]
        public int Version
        {
            get { return version; }
            set { version = value; }
        }

        [XmlElement(ElementName = "PatchUrl", IsNullable = false)]
        public string PatchUrl
        {
            get { return patchUrl; }
            set { patchUrl = value; }
        }

        public ShardPatch() { }

        public override bool Equals(object obj)
        {
            if (obj is ShardPatch)
            {
                ShardPatch p = (ShardPatch)obj;

                return (p.patchUrl == patchUrl && p.version == version);
            }

            return base.Equals(obj);
        }

        public static bool operator ==(ShardPatch a, ShardPatch b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(ShardPatch a, ShardPatch b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
