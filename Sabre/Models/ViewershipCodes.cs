using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class ViewershipCodes
    {
        [XmlElement(ElementName = "ViewershipCode")]
        public string ViewershipCode { get; set; }
    }
}