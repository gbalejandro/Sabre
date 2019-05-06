using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Discount
    {
        [XmlElement(ElementName = "DiscountReason")]
        public string DiscountReason { get; set; }
        [XmlElement(ElementName = "Taxes")]
        public List<Taxes> Taxes { get; set; }
        [XmlElement(ElementName = "TPA_Extensions")]
        public TPA_Extensions TPA_Extensions { get; set; }
        [XmlAttribute(AttributeName = "")]
        public string AdditionalFeesExcludedIndicator { get; set; }
        [XmlAttribute(AttributeName = "AmountAfterTax")]
        public string AmountAfterTax { get; set; }
        [XmlAttribute(AttributeName = "AmountBeforeTax")]
        public string AmountBeforeTax { get; set; }
        [XmlAttribute(AttributeName = "AmountIncludingMarkup")]
        public string AmountIncludingMarkup { get; set; }
        [XmlAttribute(AttributeName = "DiscountCode")]
        public string DiscountCode { get; set; }
        [XmlAttribute(AttributeName = "Percent")]
        public string Percent { get; set; }
        [XmlAttribute(AttributeName = "RateOverrideIndicator")]
        public string RateOverrideIndicator { get; set; }
        [XmlAttribute(AttributeName = "RestrictedDisplayIndicator")]
        public string RestrictedDisplayIndicator { get; set; }
        [XmlAttribute(AttributeName = "ServiceOverrideIndicator")]
        public string ServiceOverrideIndicator { get; set; }
        [XmlAttribute(AttributeName = "TaxInclusive")]
        public string TaxInclusive { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
    }
}