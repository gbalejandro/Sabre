using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class ResGuests
    {
        [XmlElement(ElementName = "ResGuest")]
        public List<ResGuest> ResGuest { get; set; }
    }
}