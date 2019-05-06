using Sabre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class RatePlan
    {
        [XmlAttribute(AttributeName = "RatePlanCode")]
        public string RatePlanCode { get; set; }
        [XmlElement(ElementName = "Guarantee")]
        public Guarantee Guarantee { get; set; }
        [XmlElement(ElementName = "MealsIncluded")]
        public MealsIncluded MealsIncluded { get; set; }
        [XmlElement(ElementName = "AdditionalDetails")]
        public List<AdditionalDetails> AdditionalDetails { get; set; }
    }
}