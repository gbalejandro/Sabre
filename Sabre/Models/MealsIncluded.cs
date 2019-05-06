using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    [Serializable]
    public class MealsIncluded
    {
        [XmlAttribute(AttributeName = "MealPlanIndicator")]
        public bool MealPlanIndicator { get; set; }
        [XmlAttribute(AttributeName = "MealPlanCodes")]
        public string MealPlanCodes { get; set; }
    }
}