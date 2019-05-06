using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Sabre.Models
{
    [Serializable]
    public class Fees
    {
        public List<Fee> Fee { get; set; }
    }
}