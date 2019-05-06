using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Deadline
    {
        [XmlAttribute(AttributeName = "OffsetTimeUnit")]
        public string OffsetTimeUnit { get; set; }
        [XmlAttribute(AttributeName = "OffsetUnitMultiplier")]
        public string OffsetUnitMultiplier { get; set; }
        [XmlAttribute(AttributeName = "OffsetDropTime")]
        public string OffsetDropTime { get; set; }
    }
}