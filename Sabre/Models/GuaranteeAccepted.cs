using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class GuaranteeAccepted
    {
        [XmlAttribute(AttributeName = "GuaranteeTypeCode")]
        public string GuaranteeTypeCode { get; set; }
        [XmlAttribute(AttributeName = "GuaranteeID")]
        public string GuaranteeID { get; set; }
        [XmlElement(ElementName = "PaymentCard")]
        public PaymentCard PaymentCard { get; set; }
    }
}