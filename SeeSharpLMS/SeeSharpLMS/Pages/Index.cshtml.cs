using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace SeeSharpLMS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Data.ApplicationDbContext _context;

        public string ErrorMessage;
        private string Email;
        private string Password;
        
       


        public IndexModel(ILogger<IndexModel> logger, Data.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            HttpContext.Session.Clear();
            foreach (var cookie in Request.Cookies.Keys)

            {
                if (cookie == ".AspNetCore.Session")
                    Response.Cookies.Delete(cookie);
            }
        }

        public void OnPost()
        {
            /*
            //Get user input
            Email = Request.Form["Login_Email"].ToString();
            Password = Request.Form["Login_Password"].ToString();

            //Validate input email and password
            bool validPassword = false;
            if (_context.Student.Any(x => x.Email == Email))
            {
                validPassword = BCrypt.Net.BCrypt.Verify(Password, _context.Student.Where(x => x.Email == Email).Select(y => y.Password).FirstOrDefault().ToString());
            }
            else if (_context.Instructor.Any(e => e.Email == Email))
            {
                validPassword = BCrypt.Net.BCrypt.Verify(Password, _context.Instructor.Where(x => x.Email == Email).Select(y => y.Password).FirstOrDefault().ToString());
            }

            //redirect to the Landing Page if valid login
            if (validPassword)
            {
            
                HttpContext.Session.SetString("email", Email);


                return RedirectToPage("/LandingPage");
            }
            else //display error invalid login
            {
            
                ErrorMessage = "invalid email or password";
                return Page();
            }*/
        }
    }
}
