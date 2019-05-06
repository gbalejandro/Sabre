using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class SpecialRequest
    {
        [XmlAttribute(AttributeName = "CodeContext")]
        public string CodeContext { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "NumberOfUnits")]
        public string NumberOfUnits { get; set; }
        [XmlAttribute(AttributeName = "ParagraphNumber")]
        public string ParagraphNumber { get; set; }
        [XmlAttribute(AttributeName = "RequestCode")]
        public string RequestCode { get; set; }
        [XmlElement(ElementName = "Image")]
        public string Image { get; set; }
        [XmlElement(ElementName = "ListItem")]
        public string ListItem { get; set; }
        [XmlElement(ElementName = "Text")]
        public string Text { get; set; }
        [XmlElement(ElementName = "URL")]
        public string URL { get; set; }
    }
}