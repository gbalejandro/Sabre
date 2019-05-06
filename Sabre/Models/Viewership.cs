using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Viewership
    {
        [XmlElement(ElementName = "BookingChannelCodes")]
        public List<string> BookingChannelCodes { get; set; }
        [XmlElement(ElementName = "DistributorTypes")]
        public List<string> DistributorTypes { get; set; }
        [XmlElement(ElementName = "LocationCodes")]
        public List<string> LocationCodes { get; set; }
        [XmlElement(ElementName = "ProfileRefs")]
        public List<ProfileRefs> ProfileRefs { get; set; }
        [XmlElement(ElementName = "Profiles")]
        public List<Profiles> Profiles { get; set; }
        [XmlElement(ElementName = "ProfileTypes")]
        public List<ProfileTypes> ProfileTypes { get; set; }
        [XmlElement(ElementName = "SystemCodes")]
        public List<SystemCodes> SystemCodes { get; set; }
        [XmlElement(ElementName = "ViewershipCodes")]
        public List<ViewershipCodes> ViewershipCodes { get; set; }
        [XmlAttribute(AttributeName = "ViewershipRPH")]
        public string ViewershipRPH { get; set; }
        [XmlAttribute(AttributeName = "ViewOnly")]
        public string ViewOnly { get; set; }
    }
}