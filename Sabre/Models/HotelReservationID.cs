using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class HotelReservationID
    {
        [XmlAttribute(AttributeName = "ResID_Type")]
        public string ResID_Type { get; set; }
        [XmlAttribute(AttributeName = "ResID_Value")]
        public string ResID_Value { get; set; }
        [XmlAttribute(AttributeName = "ResID_Source")]
        public string ResID_Source { get; set; }

        //public HotelReservationID()
        //{

        //}

        //public HotelReservationID(string ResID_Type, string ResID_Value, string ResID_Source)
        //{
        //    this.ResID_Type = ResID_Type;
        //    this.ResID_Value = ResID_Value;
        //    this.ResID_Source = ResID_Source;
        //}
    }
}