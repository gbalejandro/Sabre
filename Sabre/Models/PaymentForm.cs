using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class PaymentForm
    {
        [XmlElement(ElementName = "DirectBill")]
        public DirectBill DirectBill { get; set; }
    }
}