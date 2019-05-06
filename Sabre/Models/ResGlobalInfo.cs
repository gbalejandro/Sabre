using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class ResGlobalInfo
    {
        [XmlElement(ElementName = "BasicPropertyInfo")]
        public BasicPropertyInfo BasicPropertyInfo { get; set; }
        [XmlElement(ElementName = "HotelReservationIDs")]
        public HotelReservationIDs HotelReservationIDs { get; set; }
        [XmlElement(ElementName = "GuestCounts")]
        public GuestCounts GuestCounts { get; set; }
        [XmlElement(ElementName = "Comments")]
        public Comments Comments { get; set; }
        [XmlElement(ElementName = "Total")]
        public Total Total { get; set; }
        [XmlElement(ElementName = "Profiles")]
        public Profiles Profiles { get; set; }
        [XmlElement(ElementName = "DepositPayments")]
        public DepositPayments DepositPayments { get; set; }
        [XmlElement(ElementName = "Guarantee")]
        public Guarantee Guarantee { get; set; }
        [XmlElement(ElementName = "SpecialRequests")]
        public List<SpecialRequests> SpecialRequests { get; set; }
        [XmlElement(ElementName = "Fees")]
        public List<Fees> Fees { get; set; }
        [XmlElement(ElementName = "RoutingHops")]
        public RoutingHops RoutingHops { get; set; }
        [XmlElement(ElementName = "BookingRules")]
        public BookingRules BookingRules { get; set; }
        [XmlElement(ElementName = "Memberships")]
        public Memberships Memberships { get; set; }
    }
}