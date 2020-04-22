using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SeeSharpLMS
{
    public class PaymentConfirmationModel : PageModel
    {
        public bool Success { get; set; }
        public void OnGet(bool success)
        {
            Success = success;
        }
    }
}