
using System;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.opentravel.org/OTA/2003/05")]
    public class OTA_HotelResNotifRQ
    {
        [XmlAttribute(AttributeName = "TimeStamp")]
        public string TimeStamp { get; set; }
        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "ResStatus")]
        public string ResStatus { get; set; }
        [XmlAttribute(AttributeName = "EchoToken")]
        public string EchoToken { get; set; }
        [XmlElement(ElementName = "HotelReservations")]
        public HotelReservations HotelReservations { get; set; }
        [XmlElement(ElementName = "POS")]
        public POS POS { get; set; }
        [XmlElement(ElementName = "MessageID")]
        public MessageID MessageID { get; set; }
    }
}