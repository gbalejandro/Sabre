using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class ProfileTypes
    {
        [XmlElement(ElementName = "ProfileType")]
        public string ProfileType { get; set; }
    }
}