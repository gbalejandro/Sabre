using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class POS
    {
        [XmlElement(ElementName = "Source")]
        public List<Source> Source { get; set; }
    }
}