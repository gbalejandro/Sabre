using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class RoomStay
    {
        [XmlAttribute(AttributeName = "IndexNumber")]
        public string IndexNumber { get; set; }
        [XmlAttribute(AttributeName = "PromotionCode")]
        public string PromotionCode { get; set; }
        [XmlAttribute(AttributeName = "PromotionVendorCode")]
        public string PromotionVendorCode { get; set; }
        [XmlAttribute(AttributeName = "WarningRPH")]
        public string WarningRPH { get; set; }
        [XmlElement(ElementName = "RoomTypes")]
        public RoomTypes RoomTypes { get; set; }
        [XmlElement(ElementName = "RatePlans")]
        public RatePlans RatePlans { get; set; }
        [XmlElement(ElementName = "RoomRates")]
        public RoomRates RoomRates { get; set; }
        [XmlElement(ElementName = "GuestCounts")]
        public GuestCounts GuestCounts { get; set; }
        [XmlElement(ElementName = "TimeSpan")]
        public TimeSpan TimeSpan { get; set; }
        [XmlElement(ElementName = "CancelPenalties")]
        public CancelPenalties CancelPenalties { get; set; }
        [XmlElement(ElementName = "Total")]
        public Total Total { get; set; }
        [XmlElement(ElementName = "ResGuestRPHs")]
        public string ResGuestRPHs { get; set; }
        [XmlElement(ElementName = "ServiceRPHs")]
        public ServiceRPHs ServiceRPHs { get; set; }
        [XmlElement(ElementName = "DepositPayments")]
        public List<DepositPayments> DepositPayments { get; set; }
        [XmlElement(ElementName = "Memberships")]
        public List<MemberShip> MemberShips { get; set; }
        [XmlElement(ElementName = "Comments")]
        public List<Comment> Comments { get; set; }
        [XmlElement(ElementName = "SpecialRequests")]
        public SpecialRequests SpecialRequests { get; set; }
        [XmlElement(ElementName = "Discount")]
        public Discount Discount { get; set; }
        [XmlElement(ElementName = "Guarantee")]
        public Guarantee Guarantee { get; set; }
        [XmlElement(ElementName = "MapURL")]
        public string MapURL { get; set; }
        [XmlElement(ElementName = "Reference")]
        public Reference Reference { get; set; }
        [XmlElement(ElementName = "BookingRules")]
        public List<BookingRules> BookingRules { get; set; }
    }
}