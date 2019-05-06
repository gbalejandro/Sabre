using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    [XmlRoot(ElementName = "soapenv:Header")]
    public class Header
    {
        [XmlElement(ElementName = "con:OmnibeesCredentials")]
        public OmnibeesCredentials OmnibeesCredentials { get; set; }
    }
}