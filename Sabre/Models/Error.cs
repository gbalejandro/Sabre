using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Error
    {
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "ShortText")]
        public string ShortText { get; set; }
        [XmlAttribute(AttributeName = "RecordID")]
        public string RecordID { get; set; }
        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }
    }
}