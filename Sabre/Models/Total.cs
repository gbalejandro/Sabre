using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Total
    {
        [XmlAttribute(AttributeName = "AmountBeforeTax")]
        public double AmountBeforeTax { get; set; }
        [XmlAttribute(AttributeName = "AmountAfterTax")]
        public double AmountAfterTax { get; set; }
        [XmlAttribute(AttributeName = "CurrencyCode")]
        public string CurrencyCode { get; set; }
        [XmlElement(ElementName = "Taxes")]
        public Taxes Taxes { get; set; }
    }
}