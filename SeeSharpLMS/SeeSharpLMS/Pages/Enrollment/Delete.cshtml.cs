using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.DataAccessLayer;
using SeeSharpLMS.Models;
using SeeSharpLMS.Utility;
using Microsoft.AspNetCore.Http;
using SeeSharpLMS.Models.ViewModels;

namespace SeeSharpLMS
{
    public class EnrollmentDeleteModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationUser user { get; set; }
        public EnrollmentDeleteModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; }
        public IList<Enrollment> enrollment { get; set; }
        
        public Course Course { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<CourseInfo> CourseInfos = HttpContext.Session.Get<List<CourseInfo>>("CourseInfo");
            user = HttpContext.Session.Get<ApplicationUser>("UserInfo");

            Enrollment = await _context.Enrollment
                .Include(e => e.ApplicationUser)
                .Include(e => e.Section).FirstOrDefaultAsync(m => m.Id == id);
            CourseInfos.RemoveAll(c => c.SectionId == Enrollment.SectionId);
            HttpContext.Session.Set<List<CourseInfo>>("CourseInfo", CourseInfos);

            
            Course = _context.Course.FirstOrDefault(c => c.Id == Enrollment.Section.CourseId);
            Charge Charge = new Charge();
            Charge.Reason = "Withdraw from " + Course.Subject + " " + Course.Number + " Section " + Enrollment.SectionId;
            Charge.Date = DateTime.Now;
            Charge.Amount = -1*(Course.CreditHours * SD.CostPerCredit + Course.CourseFee);
            Charge.StudentId = user.Id;
            if (Enrollment != null)
            {
                DataAccessClass dataAccess = new DataAccessClass();
                dataAccess.RemoveEnrollment(Enrollment);
                dataAccess.AddCharge(Charge);
            }

            return RedirectToPage("./Index");
        }
    }
}
