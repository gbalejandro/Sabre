using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class MemberShip
    {
        [XmlAttribute(AttributeName = "AccountID")]
        public string AccountID { get; set; }
        [XmlAttribute(AttributeName = "BonusCode")]
        public string BonusCode { get; set; }
        [XmlAttribute(AttributeName = "PointsEarned")]
        public string PointsEarned { get; set; }
        [XmlAttribute(AttributeName = "ProgramCode")]
        public string ProgramCode { get; set; }
        [XmlAttribute(AttributeName = "TravelSector")]
        public string TravelSector { get; set; }
    }
}