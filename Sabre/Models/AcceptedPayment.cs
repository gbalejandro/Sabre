using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class AcceptedPayment
    {
        [XmlAttribute(AttributeName = "RPH")]
        public string RPH { get; set; }
        [XmlAttribute(AttributeName = "PaymentTransactionTypeCode")]
        public string PaymentTransactionTypeCode { get; set; }
        [XmlElement(ElementName = "PaymentCard")]
        public PaymentCard PaymentCard { get; set; }
    }
}