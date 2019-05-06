using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class PaymentCard
    {
        [XmlAttribute(AttributeName = "CardCode")]
        public string CardCode { get; set; }
        [XmlAttribute(AttributeName = "ExpireDate")]
        public string ExpireDate { get; set; }
        [XmlElement(ElementName = "CardHolderName")]
        public string CardHolderName { get; set; }
        [XmlElement(ElementName = "CardNumber")]
        public CardNumber CardNumber { get; set; }
        [XmlElement(ElementName = "SeriesCode")]
        public SeriesCode SeriesCode { get; set; }
        [XmlElement(ElementName = "PaymentCardTypeThreeDomainSecurity")]
        public PaymentCardTypeThreeDomainSecurity PaymentCardTypeThreeDomainSecurity { get; set; }
    }
}