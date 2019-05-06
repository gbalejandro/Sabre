using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class CardNumber
    {
        [XmlElement(ElementName = "PlainText")]
        public string PlainText { get; set; }
    }
}