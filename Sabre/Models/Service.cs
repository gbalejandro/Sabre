using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Service
    {
        [XmlAttribute(AttributeName = "ServicePricingType")]
        public string ServicePricingType { get; set; }
        [XmlAttribute(AttributeName = "ServiceRPH")]
        public string ServiceRPH { get; set; }
        [XmlAttribute(AttributeName = "Inclusive")]
        public string Inclusive { get; set; }
        [XmlAttribute(AttributeName = "Quantity")]
        public string Quantity { get; set; }
        [XmlAttribute(AttributeName = "ID")]
        public string ID { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "ID_Type")]
        public string ID_Type { get; set; }
        [XmlAttribute(AttributeName = "ID_Context")]
        public string ID_Context { get; set; }
        [XmlElement(ElementName = "Price")]
        public Price Price { get; set; }
        [XmlElement(ElementName = "ServiceDetails")]
        public ServiceDetails ServiceDetails { get; set; }
    }
}