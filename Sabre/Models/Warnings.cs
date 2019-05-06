using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class Warnings
    {
        [XmlElement]
        public List<Warning> Warning { get; set; }
    }
}