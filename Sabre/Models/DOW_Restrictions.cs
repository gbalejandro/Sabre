using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class DOW_Restrictions
    {
        [XmlElement(ElementName = "ArrivalDaysOfWeek")]
        public string ArrivalDaysOfWeek { get; set; }
        [XmlElement(ElementName = "AvailableDaysOfWeek")]
        public string AvailableDaysOfWeek { get; set; }
        [XmlElement(ElementName = "DepartureDaysOfWeek")]
        public string DepartureDaysOfWeek { get; set; }
        [XmlElement(ElementName = "RequiredDaysOfWeek")]
        public string RequiredDaysOfWeek { get; set; }
    }
}