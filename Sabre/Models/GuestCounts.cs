using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class GuestCounts
    {
        [XmlAttribute(AttributeName = "IsPerRoom")]
        public string IsPerRoom { get; set; }
        [XmlElement(ElementName = "GuestCount")]
        public List<GuestCount> GuestCount { get; set; }
    }
}