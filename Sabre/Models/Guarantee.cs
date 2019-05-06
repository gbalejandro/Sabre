using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Guarantee
    {
        [XmlAttribute(AttributeName = "GuaranteeType")]
        public string GuaranteeType { get; set; }
        [XmlElement(ElementName = "GuaranteesAccepted")]
        public GuaranteesAccepted GuaranteesAccepted { get; set; }
        [XmlElement(ElementName = "Deadline")]
        public Deadline Deadline { get; set; }
        [XmlElement(ElementName = "GuaranteeDescription")]
        public GuaranteeDescription GuaranteeDescription { get; set; }
        [XmlElement(ElementName = "AmountPercent")]
        public AmountPercent AmountPercent { get; set; }
    }
}