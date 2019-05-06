using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class AdditionalDetails
    {
        [XmlElement(ElementName = "AdditionalDetail")]
        public List<AdditionalDetail> AdditionalDetail { get; set; }
    }
}