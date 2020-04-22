using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;
using SeeSharpLMS.Utility;
using Microsoft.AspNetCore.Identity;



namespace SeeSharpLMS
{
    public class EnrollmentDetailsModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationUser user { get; set; }
        public EnrollmentDetailsModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Enrollment Enrollment { get; set; }
        public IList<Enrollment> enrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Enrollment = await _context.Enrollment
                .Include(e => e.ApplicationUser)
                .Include(e => e.Section).FirstOrDefaultAsync(m => m.Id == id);

            if (Enrollment == null)
            {
                return NotFound();
            }



            user = HttpContext.Session.Get<ApplicationUser>("UserInfo");
            if (user == null)
            {
                user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
                HttpContext.Session.Set<ApplicationUser>("UserInfo", user);
            }

            enrollment = await _context.Enrollment.Where(e => e.StudentId == user.Id)
                .Include(e => e.Section)
                    .ThenInclude(u => u.ApplicationUser)
                .Include(e => e.Section)
                    .ThenInclude(s => s.Course).ToListAsync();

            return Page();

        }
    }
}
