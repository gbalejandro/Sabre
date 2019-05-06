using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class CompanyInfo
    {
        [XmlElement(ElementName = "CompanyName")]
        public CompanyName CompanyName { get; set; }
        [XmlElement(ElementName = "AddressInfo")]
        public AddressInfo AddressInfo { get; set; }
        [XmlElement(ElementName = "TelephoneInfo")]
        public TelephoneInfo TelephoneInfo { get; set; }
        [XmlElement(ElementName = "BusinessLocale")]
        public BusinessLocale BusinessLocale { get; set; }
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
    }
}