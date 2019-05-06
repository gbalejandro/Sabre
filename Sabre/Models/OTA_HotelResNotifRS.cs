using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    [XmlRoot(ElementName = "OTA_HotelResNotifRS", Namespace = "http://www.opentravel.org/OTA/2003/05")]
    public class OTA_HotelResNotifRS
    {
        [XmlAttribute(AttributeName = "EchoToken")]
        public string EchoToken { get; set; }
        [XmlAttribute(AttributeName = "TimeStamp")]
        public string TimeStamp { get; set; }
        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
        public HotelReservations HotelReservations { get; set; }
        [XmlElement(ElementName = "Success")]
        public Success Success { get; set; }
        [XmlElement(ElementName = "Errors")]
        public string Errors { get; set; }
        [XmlElement(ElementName = "Warnings")]
        public Warnings Warnings { get; set; }
    }
}