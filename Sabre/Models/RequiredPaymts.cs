using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabre.Models
{
    [Serializable]
    public class RequiredPaymts
    {
        public GuaranteePayment GuaranteePayment { get; set; }
    }
}