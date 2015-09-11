using System.Xml.Serialization;

namespace ConnectUO.Framework
{
    public enum ShardType : int
    {
        [XmlEnum("1")] 
        RPPVP = 1,
        [XmlEnum("2")]
        RP = 2,
        [XmlEnum("3")] 
        PVP = 3,
    }
}
