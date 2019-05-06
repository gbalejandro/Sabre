using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Rate
    {
        [XmlAttribute(AttributeName = "EffectiveDate")]
        public string EffectiveDate { get; set; }
        [XmlElement(ElementName = "Total")]
        public Total Total { get; set; }
        public List<PaymentPolicies> PaymentPolicies { get; set; }
    }
}