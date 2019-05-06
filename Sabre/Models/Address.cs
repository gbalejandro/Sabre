using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Address
    {
        [XmlAttribute(AttributeName = "UseType")]
        public string UseType { get; set; }
        [XmlElement(ElementName = "AddressLine")]
        public List<string> AddressLine { get; set; }
        [XmlElement(ElementName = "CityName")]
        public string CityName { get; set; }
        [XmlElement(ElementName = "PostalCode")]
        public string PostalCode { get; set; }
        [XmlElement(ElementName = "StateProv")]
        public StateProv StateProv { get; set; }
        [XmlElement(ElementName = "CountryName")]
        public CountryName CountryName { get; set; }
    }
}