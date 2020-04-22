using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models.ViewModels
{
    public class PaymentInfo
    {
        public string CardNumber { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string CVC { get; set; }
        public double Amount { get; set; }
    }
}
