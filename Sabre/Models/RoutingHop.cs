using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class RoutingHop
    {
        [XmlAttribute(AttributeName = "Comment")]
        public string Comment { get; set; }
        [XmlAttribute(AttributeName = "Data")]
        public string Data { get; set; }
        [XmlAttribute(AttributeName = "LocalRefID")]
        public string LocalRefID { get; set; }
        [XmlAttribute(AttributeName = "SequenceNmbr")]
        public string SequenceNmbr { get; set; }
        [XmlAttribute(AttributeName = "SystemCode")]
        public string SystemCode { get; set; }
        [XmlAttribute(AttributeName = "TimeStamp")]
        public string TimeStamp { get; set; }
    }
}