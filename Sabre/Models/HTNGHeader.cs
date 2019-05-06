using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [XmlRoot(Namespace = "http://www.opentravel.org/OTA/2003/05")]
    [XmlInclude(typeof(Clase1))]
    [XmlInclude(typeof(Clase2))]
    public class HTNGHeader
    {
        [XmlElement(ElementName = "OmnibeesCredentials")]
        public OmnibeesCredentials OmnibeesCredentials { get; set; }

        //[XmlElement(ElementName = "From")]
        //public From from { get; set; }
        //[XmlElement(ElementName = "To")]
        //public To to { get; set; }
        //[XmlElement(ElementName = "timeStamp")]
        //public string timeStamp { get; set; }
        //[XmlElement(ElementName = "echoToken")]
        //public string echoToken { get; set; }
        //[XmlElement(ElementName = "transactionId")]
        //public string transactionId { get; set; }
        //[XmlElement(ElementName = "action")]
        //public string action { get; set; }
    }

    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Clase1 : HTNGHeader { }

    [XmlRoot(Namespace = "http://connectors.omnibees.com/")]
    public class Clase2 : HTNGHeader { }
}