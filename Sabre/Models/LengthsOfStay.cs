using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class LengthsOfStay
    {
        [XmlElement(ElementName = "LengthOfStay")]
        public LengthOfStay LengthOfStay { get; set; }
        [XmlAttribute(AttributeName = "ArrivalDateBased")]
        public string ArrivalDateBased { get; set; }
        [XmlAttribute(AttributeName = "FixedPatternLengthMyProperty")]
        public string FixedPatternLengthMyProperty { get; set; }
    }
}