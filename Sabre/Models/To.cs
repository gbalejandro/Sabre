using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    public class To
    {
        [XmlElement(ElementName = "systemId")]
        public string systemId { get; set; }

        public To()
        {

        }

        public To(string systemId)
        {
            this.systemId = systemId;
        }
    }
}