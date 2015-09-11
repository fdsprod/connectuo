using System.Xml.Serialization;

namespace ConnectUO.Framework
{
    public enum Lang : int
    {
        [XmlEnum("1")]
        ENG = 1,
        [XmlEnum("2")]
        CHI = 2,
        [XmlEnum("3")] 
        JAP = 3,
        [XmlEnum("4")]
        TUR = 4,
        [XmlEnum("5")]
        KOR = 5,
        [XmlEnum("6")] 
        GER = 6,
        [XmlEnum("7")]
        SPN = 7,
        [XmlEnum("8")]
        POR = 8,
        [XmlEnum("9")] 
        MULTI = 9,
    }
}
