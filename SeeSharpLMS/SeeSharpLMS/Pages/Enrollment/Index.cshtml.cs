using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;
using SeeSharpLMS.Utility;

namespace SeeSharpLMS
{
    public class EnrollmentIndexModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationUser user { get; set; }
        public EnrollmentIndexModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Enrollment> Enrollment { get; set; }

        public async Task OnGetAsync()
        {
            user = HttpContext.Session.Get<ApplicationUser>("UserInfo");
            if(user == null)
            {
                user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
                HttpContext.Session.Set<ApplicationUser>("UserInfo", user);
            }
            Enrollment = await _context.Enrollment.Where(e => e.StudentId == user.Id)
                .Include(e => e.Section)
                    .ThenInclude(u => u.ApplicationUser)
                .Include(e => e.Section)
                    .ThenInclude(s => s.Course).ToListAsync();
        }
    }
}
