using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Viewerships
    {
        [XmlElement(ElementName = "Viewership")]
        public Viewership Viewership { get; set; }
    }
}