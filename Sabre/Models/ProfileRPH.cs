using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class ProfileRPH
    {
        [XmlElement(ElementName = "RPH")]
        public string RPH { get; set; }
    }
}