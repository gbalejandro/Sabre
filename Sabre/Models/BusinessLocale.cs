using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class BusinessLocale
    {
        [XmlElement(ElementName = "AddressLine")]
        public List<string> AddressLine { get; set; }
        [XmlElement(ElementName = "CityName")]
        public string CityName { get; set; }
        [XmlElement(ElementName = "PostalCode")]
        public string PostalCode { get; set; }
        [XmlElement(ElementName = "StateProv")]
        public string StateProv { get; set; }
        [XmlElement(ElementName = "CountryName")]
        public string CountryName { get; set; }
    }
}