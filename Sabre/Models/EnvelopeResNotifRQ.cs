using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    [XmlRoot(ElementName = "soap:Envelope", Namespace = "http://www.opentravel.org/OTA/2003/05")]
    public class EnvelopeResNotifRQ
    {
        [XmlAttribute(AttributeName = "xmlns:soapenv")]
        public string soapenv { get; set; }
        [XmlAttribute(AttributeName = "xmlns:con")]
        public string con { get; set; }
        [XmlElement(ElementName = "Header")]
        public Header Header { get; set; }
        [XmlElement(ElementName = "Body")]
        public BodyResNotifRQ Body { get; set; }
    }
}