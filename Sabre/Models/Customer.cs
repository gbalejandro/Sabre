using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Customer
    {
        [XmlAttribute(AttributeName = "Birthday")]
        public string Birthday { get; set; }
        [XmlElement(ElementName = "PersonName")]
        public PersonName PersonName { get; set; }
        [XmlElement(ElementName = "Telephone")]
        public Telephone Telephone { get; set; }
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "Address")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "PaymentForm")]
        public PaymentForm PaymentForm { get; set; }
        [XmlElement(ElementName = "CustLoyalty")]
        public List<CustLoyalty> CustLoyalty { get; set; }
        [XmlElement(ElementName = "Document")]
        public List<Document> Document { get; set; }
        [XmlElement(ElementName = "AdditionalPersonNames")]
        public string AdditionalPersonNames { get; set; }
    }
}