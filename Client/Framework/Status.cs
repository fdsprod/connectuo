using System.Xml.Serialization;

namespace ConnectUO.Framework
{
    public enum Status : int
    {
        [XmlEnum("1")]
        Active = 1,
        [XmlEnum("3")]
        NeedsApproval = 3,
        [XmlEnum("4")]
        Suspended = 4,
        [XmlEnum("5")]
        Down = 5,
        [XmlEnum("6")]
        Inactive = 6,
        [XmlEnum("7")] 
        InvalidWebsite = 7,
        [XmlEnum("8")] 
        NeedsWebsiteApproval = 4,
    }
}
