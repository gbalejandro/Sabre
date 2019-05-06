using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class LengthOfStay
    {
        [XmlElement(ElementName = "LOS_Pattern")]
        public string LOS_Pattern { get; set; }
        [XmlAttribute(AttributeName = "MinMaxMessageType")]
        public string MinMaxMessageType { get; set; }
        [XmlAttribute(AttributeName = "OpenStatusIndicator")]
        public string OpenStatusIndicator { get; set; }
        [XmlAttribute(AttributeName = "Time")]
        public string Time { get; set; }
        [XmlAttribute(AttributeName = "TimeUnit")]
        public string TimeUnit { get; set; }
    }
}