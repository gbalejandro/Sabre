﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class GuaranteeDescription
    {
        [XmlElement(ElementName = "Text")]
        public string Text { get; set; }
    }
}