using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class TelephoneInfo
    {
        [XmlAttribute(AttributeName = "PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}