using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class TimeSpan
    {
        [XmlAttribute(AttributeName = "Start")]
        public string Start { get; set; }
        [XmlAttribute(AttributeName = "End")]
        public string End { get; set; }
    }
}