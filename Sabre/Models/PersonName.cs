using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class PersonName
    {
        [XmlAttribute(AttributeName = "NameType")]
        public string NameType { get; set; }
        [XmlElement(ElementName = "NamePrefix")]
        public string NamePrefix { get; set; }
        [XmlElement(ElementName = "GivenName")]
        public string GivenName { get; set; }
        [XmlElement(ElementName = "Surname")]
        public string Surname { get; set; }
    }
}