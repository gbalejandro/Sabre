using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class AddressInfo
    {
        [XmlElement(ElementName = "AddressLine")]
        public List<string> AddressLine { get; set; }
        [XmlElement(ElementName = "CityName")]
        public string CityName { get; set; }
        [XmlElement(ElementName = "Country")]
        public string Country { get; set; }
    }
}