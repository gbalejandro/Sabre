
using System;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    [XmlRoot(ElementName = "POS")]
    public class Source
    {
        [XmlAttribute(AttributeName = "PseudoCityCode")]
        public string PseudoCityCode { get; set; }
        [XmlElement(ElementName = "BookingChannel")]
        public BookingChannel BookingChannel { get; set; }
    }
}