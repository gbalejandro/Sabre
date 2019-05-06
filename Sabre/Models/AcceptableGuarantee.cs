using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class AcceptableGuarantee
    {
        [XmlAttribute(AttributeName = "GuaranteeCode")]
        public string GuaranteeCode { get; set; }
        [XmlAttribute(AttributeName = "GuaranteePolicyType")]
        public string GuaranteePolicyType { get; set; }
        [XmlAttribute(AttributeName = "GuaranteeType")]
        public string GuaranteeType { get; set; }
        [XmlAttribute(AttributeName = "HoldTime")]
        public string HoldTime { get; set; }
        [XmlAttribute(AttributeName = "PaymentType")]
        public string PaymentType { get; set; }
        [XmlAttribute(AttributeName = "RetributionType")]
        public string RetributionType { get; set; }
        [XmlAttribute(AttributeName = "UnacceptablePaymentType")]
        public string UnacceptablePaymentType { get; set; }
        [XmlElement(ElementName = "Comments")]
        public List<Comments> Comments { get; set; }
        [XmlElement(ElementName = "Deadline")]
        public Deadline Deadline { get; set; }
        [XmlElement(ElementName = "GuaranteeDescription")]
        public GuaranteeDescription GuaranteeDescription { get; set; }
        [XmlElement(ElementName = "GuaranteesAccepted")]
        public GuaranteesAccepted GuaranteesAccepted { get; set; }
    }
}