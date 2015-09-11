using System.Xml.Serialization;

namespace ConnectUO.Framework
{
    public enum Era : int
    {
        [XmlEnum("1")]
        Custom = 1,
        [XmlEnum("2")]
        PreT2A = 2,
        [XmlEnum("3")]
        T2A = 3,
        [XmlEnum("4")]
        LBR = 4,
        [XmlEnum("5")]
        UOR = 5,
        [XmlEnum("6")]
        AOS = 6,
        [XmlEnum("7")]
        SE = 7,
        [XmlEnum("8")] 
        ML = 8
    }
}
