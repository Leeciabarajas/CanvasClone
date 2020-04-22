using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Models;
using SeeSharpLMS.Utility;

namespace SeeSharpLMS.Pages.AccountBalance
{
    public class IndexModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationUser user { get; set; }
        public double TotalBalance { get; set; }
        public IndexModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Charge> Charge { get; set; }

        public async Task OnGetAsync()
        {
            user = HttpContext.Session.Get<ApplicationUser>("UserInfo");
            if (user == null)
            {
                user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
                HttpContext.Session.Set<ApplicationUser>("UserInfo", user);
            }
            Charge = await _context.Charge.Where(e => e.StudentId == user.Id).ToListAsync();
            foreach(var charge in Charge)
            {
                TotalBalance += charge.Amount;
            }
        }
    }
}