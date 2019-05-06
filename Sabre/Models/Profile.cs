using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Profile
    {
        [XmlAttribute(AttributeName = "StatusCode")]
        public string StatusCode { get; set; }
        [XmlElement(ElementName = "Customer")]
        public Customer Customer { get; set; }
        [XmlElement(ElementName = "CompanyInfo")]
        public CompanyInfo CompanyInfo { get; set; }
        [XmlElement(ElementName = "TPA_Extensions")]
        public TPA_Extensions TPA_Extensions { get; set; }
    }
}