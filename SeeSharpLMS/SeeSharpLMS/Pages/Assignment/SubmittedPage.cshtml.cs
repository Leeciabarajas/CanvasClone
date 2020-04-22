using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SeeSharpLMS
{
    public class SubmittedPageModel : PageModel
    {
        public SubmitAssignmentModel submit { get; set; }
        public void OnGet()
        {

        }
    }
}