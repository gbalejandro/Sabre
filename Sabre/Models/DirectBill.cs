using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class DirectBill
    {
        [XmlElement(ElementName = "Telephone")]
        public Telephone Telephone { get; set; }
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "Address")]
        public Address Address { get; set; }
    }
}