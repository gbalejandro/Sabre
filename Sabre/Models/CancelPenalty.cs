using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class CancelPenalty
    {
        [XmlAttribute(AttributeName = "NonRefundable")]
        public string NonRefundable { get; set; }
        [XmlElement(ElementName = "Deadline")]
        public Deadline Deadline { get; set; }
        [XmlElement(ElementName = "AmountPercent")]
        public AmountPercent AmountPercent { get; set; }
        [XmlElement(ElementName = "PenaltyDescription")]
        public PenaltyDescription PenaltyDescription { get; set; }
    }
}