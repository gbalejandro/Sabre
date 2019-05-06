using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class AmountPercent
    {
        [XmlAttribute(AttributeName = "Amount")]
        public string Amount { get; set; }
        [XmlAttribute(AttributeName = "Percent")]
        public string Percent { get; set; }
        [XmlAttribute(AttributeName = "NmbrOfNights")]
        public string NmbrOfNights { get; set; }
        [XmlElement(ElementName = "Taxes")]
        public Taxes Taxes { get; set; }
    }
}