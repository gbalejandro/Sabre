using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Errors
    {
        [XmlElement(ElementName = "Error")]
        public Error Error { get; set; }
    }
}