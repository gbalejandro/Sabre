﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class AcceptableGuarantees
    {
        [XmlElement(ElementName = "AcceptableGuarantee")]
        public AcceptableGuarantee AcceptableGuarantee { get; set; }
    }
}