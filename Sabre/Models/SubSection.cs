using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class SubSection
    {
        [XmlElement(ElementName = "Paragraph")]
        public string Paragraph { get; set; }
        [XmlAttribute(AttributeName = "SubCode")]
        public string SubCode { get; set; }
        [XmlAttribute(AttributeName = "SubSectionNumber")]
        public string SubSectionNumber { get; set; }
        [XmlAttribute(AttributeName = "SubTitle")]
        public string SubTitle { get; set; }
    }
}