using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class BookingRule
    {
        [XmlElement(ElementName = "AcceptableGuarantees")]
        public List<AcceptableGuarantees> AcceptableGuarantees { get; set; }
        [XmlElement(ElementName = "AddtionalRules")]
        public List<AddtionalRules> AddtionalRules { get; set; }
        [XmlElement(ElementName = "CancelPenalties")]
        public List<CancelPenalties> CancelPenalties { get; set; }
        [XmlElement(ElementName = "CheckoutCharge")]
        public string CheckoutCharge { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "DOW_Restrictions")]
        public List<DOW_Restrictions> DOW_Restrictions { get; set; }
        [XmlElement(ElementName = "LengthsOfStay")]
        public LengthsOfStay LengthsOfStay { get; set; }
        [XmlElement(ElementName = "RequiredPaymts")]
        public RequiredPaymts RequiredPaymts { get; set; }
        [XmlElement(ElementName = "RestrictionStatus")]
        public string RestrictionStatus { get; set; }
        [XmlElement(ElementName = "UniqueID")]
        public UniqueID UniqueID { get; set; }
        [XmlElement(ElementName = "Viewerships")]
        public List<Viewerships> Viewerships { get; set; }
        [XmlAttribute(AttributeName = "AbsoluteDropTime")]
        public string AbsoluteDropTime { get; set; }
        [XmlAttribute(AttributeName = "AddressRequired")]
        public string AddressRequired { get; set; }
        [XmlAttribute(AttributeName = "DepositWaiverOffset")]
        public string DepositWaiverOffset { get; set; }
        [XmlAttribute(AttributeName = "ForceGuaranteeOffset")]
        public string ForceGuaranteeOffset { get; set; }
        [XmlAttribute(AttributeName = "GenerallyBookable")]
        public string GenerallyBookable { get; set; }
        [XmlAttribute(AttributeName = "MaxAdvancedBookingOffset")]
        public string MaxAdvancedBookingOffset { get; set; }
        [XmlAttribute(AttributeName = "MaxContiguousBookings")]
        public string MaxContiguousBookings { get; set; }
        [XmlAttribute(AttributeName = "MaxTotalOccupancy")]
        public string MaxTotalOccupancy { get; set; }
        [XmlAttribute(AttributeName = "MinAdvancedBookingOffset")]
        public string MinAdvancedBookingOffset { get; set; }
        [XmlAttribute(AttributeName = "MinTotalOccupancy")]
        public string MinTotalOccupancy { get; set; }
        [XmlAttribute(AttributeName = "PriceViewable")]
        public string PriceViewable { get; set; }
        [XmlAttribute(AttributeName = "QualifiedRateYN")]
        public string QualifiedRateYN { get; set; }
    }
}