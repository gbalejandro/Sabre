using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Profiles
    {
        [XmlElement(ElementName = "ProfileInfo")]
        public List<ProfileInfo> ProfileInfo { get; set; }
    }
}