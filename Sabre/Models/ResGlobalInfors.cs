using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    public class ResGlobalInfors
    {
        [XmlElement(ElementName = "HotelReservationIDs")]
        private List<HotelReservationIDrs> hotelReservations;

        public List<HotelReservationIDrs> HotelReservations
        {
            get
            {
                return hotelReservations;
            }

            set
            {
                hotelReservations = value;
            }
        }
    }
}