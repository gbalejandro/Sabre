using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Fee
    {
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "Taxes")]
        public List<Taxes> Taxes { get; set; }
        [XmlElement(ElementName = "TPA_Extensions")]
        public TPA_Extensions TPA_Extensions { get; set; }
        [XmlAttribute(AttributeName = "MandatoryInd")]
        public string MandatoryInd { get; set; }
        [XmlAttribute(AttributeName = "MaxAge")]
        public string MaxAge { get; set; }
        [XmlAttribute(AttributeName = "MinAge")]
        public string MinAge { get; set; }
        [XmlAttribute(AttributeName = "RPH")]
        public string RPH { get; set; }
        [XmlAttribute(AttributeName = "TaxableIndicator")]
        public string TaxableIndicator { get; set; }
        [XmlAttribute(AttributeName = "TaxInclusiveInd")]
        public string TaxInclusiveInd { get; set; }
    }
}