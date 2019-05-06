using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class HotelReservation
    {
        [XmlAttribute(AttributeName = "ResStatus")]
        public string ResStatus { get; set; }
        [XmlAttribute(AttributeName = "CreateDateTime")]
        public string CreateDateTime { get; set; }
        [XmlAttribute(AttributeName = "LastModifyDateTime")]
        public string LastModifyDateTime { get; set; }
        [XmlElement(ElementName = "POS")]
        public POS POS { get; set; }
        [XmlElement(ElementName = "UniqueID")]
        public UniqueID UniqueID { get; set; }
        [XmlElement(ElementName = "RoomStays")]
        public RoomStays RoomStays { get; set; }
        [XmlElement(ElementName = "Services")]
        public Services Services { get; set; }
        [XmlElement(ElementName = "ResGuests")]
        public ResGuests ResGuests { get; set; }
        [XmlElement(ElementName = "ResGlobalInfo")]
        public ResGlobalInfo ResGlobalInfo { get; set; }
        [XmlElement(ElementName = "TPA_Extensions")]
        public TPA_Extensions TPA_Extensions { get; set; }
    }
}