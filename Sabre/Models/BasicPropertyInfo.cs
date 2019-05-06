using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class BasicPropertyInfo
    {
        [XmlAttribute(AttributeName = "HotelCode")]
        public string HotelCode { get; set; }
        [XmlAttribute(AttributeName = "ChainCode")]
        public string ChainCode { get; set; }
        [XmlElement(ElementName = "VendorMessages")]
        public List<VendorMessages> VendorMessages { get; set; }
        [XmlElement(ElementName = "ContactNumbers")]
        public List<ContactNumbers> ContactNumbers { get; set; }
    }
}