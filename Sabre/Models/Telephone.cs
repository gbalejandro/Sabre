using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Telephone
    {
        [XmlAttribute(AttributeName = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [XmlAttribute(AttributeName = "PhoneTechType")]
        public string PhoneTechType { get; set; }
        [XmlAttribute(AttributeName = "PhoneUseType")]
        public string PhoneUseType { get; set; }
        [XmlAttribute(AttributeName = "CountryAccessCode")]
        public string CountryAccessCode { get; set; }
    }
}