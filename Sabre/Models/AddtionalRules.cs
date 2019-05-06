using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class AddtionalRules
    {
        [XmlElement(ElementName = "AdditionalRule")]
        public string AdditionalRule { get; set; }
    }
}