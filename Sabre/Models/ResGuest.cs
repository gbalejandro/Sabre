using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class ResGuest
    {
        [XmlAttribute(AttributeName = "ResGuestRPH")]
        public string ResGuestRPH { get; set; }
        [XmlAttribute(AttributeName = "PrimaryIndicator")]
        public string PrimaryIndicator { get; set; }
        [XmlAttribute(AttributeName = "ArrivalTime")]
        public string ArrivalTime { get; set; }
        [XmlElement(ElementName = "Profiles")]
        public Profiles Profiles { get; set; }
        [XmlElement(ElementName = "SpecialRequest")]
        public SpecialRequest SpecialRequest { get; set; }
        [XmlElement(ElementName = "ServiceRPHs")]
        public ServiceRPHs ServiceRPHs { get; set; }
        [XmlElement(ElementName = "ProfileRPHs")]
        public List<ProfileRPHs> ProfileRPHs { get; set; }
        [XmlElement(ElementName = "ArrivalTransport")]
        public ArrivalTransport ArrivalTransport { get; set; }
        [XmlElement(ElementName = "DepartureTransport")]
        public DepartureTransport DepartureTransport { get; set; }
        [XmlElement(ElementName = "Comments")]
        public Comments Comments { get; set; }
    }
}