using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    public class ProfileRefs
    {
        [XmlElement(ElementName = "ProfileRef")]
        public ProfileRef ProfileRef { get; set; }
    }
}