using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class RoomRate
    {
        [XmlAttribute(AttributeName = "RatePlanCode")]
        public string RatePlanCode { get; set; }
        [XmlAttribute(AttributeName = "RoomTypeCode")]
        public string RoomTypeCode { get; set; }
        [XmlAttribute(AttributeName = "NumberOfUnits")]
        public string NumberOfUnits { get; set; }
        [XmlAttribute(AttributeName = "PromotionCode")]
        public string PromotionCode { get; set; }
        [XmlAttribute(AttributeName = "GroupCode")]
        public string GroupCode { get; set; }
        [XmlElement(ElementName = "Total")]
        public Total Total { get; set; }
        [XmlElement(ElementName = "Rates")]
        public Rates Rates { get; set; }
    }
}