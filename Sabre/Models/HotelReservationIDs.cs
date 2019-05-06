using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class HotelReservationIDs
    {
        [XmlElement(ElementName = "HotelReservationID")]
        public List<HotelReservationID> HotelReservationID { get; set; }
    }
}