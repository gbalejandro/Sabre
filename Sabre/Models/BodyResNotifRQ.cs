using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    [XmlRoot(ElementName = "soapenv:Body")]
    public class BodyResNotifRQ
    {
        [XmlElement(ElementName = "OTA_HotelResNotifRQ")]
        public OTA_HotelResNotifRQ request { get; set; }
    }
}