using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class RoomRates
    {
        [XmlElement(ElementName = "RoomRate")]
        public List<RoomRate> RoomRate { get; set; }
    }
}