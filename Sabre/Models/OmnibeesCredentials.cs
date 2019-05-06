using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class OmnibeesCredentials
    {
        [XmlElement(ElementName = "con:UserName")]
        public string UserName { get; set; }
        [XmlElement(ElementName = "con:UserPassword")]
        public string UserPassword { get; set; }
    }
}