﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class SpecialRequests
    {
        [XmlElement(ElementName = "SpecialRequest")]
        public List<SpecialRequest> SpecialRequest { get; set; }
    }
}