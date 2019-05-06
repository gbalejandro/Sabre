using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class SystemCodes
    {
        [XmlElement(ElementName = "SystemCode")]
        public string SystemCode { get; set; }
        [XmlAttribute(AttributeName = "SystemCodesInclusive")]
        public string SystemCodesInclusive { get; set; }
    }
}