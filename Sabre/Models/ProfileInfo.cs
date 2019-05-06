using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class ProfileInfo
    {
        [XmlElement(ElementName = "Profile")]
        public Profile Profile { get; set; }
        [XmlElement(ElementName = "UniqueID")]
        public UniqueID UniqueID { get; set; }
    }
}