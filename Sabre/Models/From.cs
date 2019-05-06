using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    public class From
    {
        [XmlElement(ElementName = "systemId")]
        public string systemId { get; set; }
        [XmlElement(ElementName = "Credential")]
        public Credential credential { get; set; }

        public From()
        {

        }

        public From(string systemId, Credential credential)
        {
            this.systemId = systemId;
            this.credential = credential;
        }
    }
}