using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class VendorMessage
    {
        [XmlElement(ElementName = "SubSection")]
        public SubSection SubSection { get; set; }
        [XmlAttribute(AttributeName = "InfoType")]
        public string InfoType { get; set; }
        [XmlAttribute(AttributeName = "Language")]
        public string Language { get; set; }
        [XmlAttribute(AttributeName = "Title")]
        public string Title { get; set; }
    }
}