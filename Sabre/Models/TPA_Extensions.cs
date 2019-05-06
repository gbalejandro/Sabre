using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class TPA_Extensions
    {
        [XmlElement(ElementName = "VATNumber")]
        public string VATNumber { get; set; }
        [XmlElement(ElementName = "PropertyExchangeRate")]
        public string PropertyExchangeRate { get; set; }
        [XmlElement(ElementName = "PropertyExchangeRateDate")]
        public string PropertyExchangeRateDate { get; set; }
    }
}