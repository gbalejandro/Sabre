using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Reference
    {
        [XmlElement(ElementName = "CompanyName")]
        public CompanyName CompanyName { get; set; }
        [XmlElement(ElementName = "TPA_Extensions")]
        public TPA_Extensions TPA_Extensions { get; set; }
        [XmlAttribute(AttributeName = "ID")]
        public string ID { get; set; }
        [XmlAttribute(AttributeName = "ID_Context")]
        public string ID_Context { get; set; }
        [XmlAttribute(AttributeName = "Instance")]
        public string Instance { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "URL")]
        public string URL { get; set; }
    }
}