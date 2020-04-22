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
using SeeSharpLMS.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace SeeSharpLMS
{
    public class ConfirmEnrollmentModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationUser user { get; set; }
        public List<CourseInfo> CourseInfos { get; set; }
        public ConfirmEnrollmentModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Section Section { get; set; }
        public IList<Section> section { get; set; }
        public Enrollment Enrollment { get; set; }
        public Charge Charge { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Section = await _context.Section
                .Include(c => c.Course)
                .Include(e => e.ApplicationUser).FirstOrDefaultAsync(m => m.Id == id);

            if (Section == null)
            {
                return NotFound();
            }

            user = HttpContext.Session.Get<ApplicationUser>("UserInfo");
            if (user == null)
            {
                user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
                HttpContext.Session.Set<ApplicationUser>("UserInfo", user);
            }
            CourseInfos = HttpContext.Session.Get<List<CourseInfo>>("CourseInfo");
            if (CourseInfos == null)
            {
                CourseInfos = new List<CourseInfo>();
                //get the courses that the student is enrolled in
                List<int> SectionIds = new List<int>();
                var query = _context.Enrollment.Where(s => s.StudentId == user.Id);
                foreach (Enrollment enrollment in query.ToList())
                {
                    SectionIds.Add(enrollment.SectionId);
                }

                var query2 = _context.Section.Where(s => SectionIds.Contains(s.Id));
                foreach (Section section in query2.ToList())
                {
                    CourseInfos.Add(new CourseInfo
                    {
                        Course = _context.Course.Find(section.CourseId),
                        SectionId = section.Id,
                        Notifications = new List<Notification>(_context.Notification.Where(n => n.SectionId == section.Id).ToList()),
                        Assignments = new List<Assignment>(_context.Assignment.Where(a => a.SectionId == section.Id).ToList())
                    });
                }
                HttpContext.Session.Set<List<CourseInfo>>("CourseInfo", CourseInfos);
            }
            
            section = await _context.Section.Where(e => e.Id == id)
                .Include(e => e.Course).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            user = HttpContext.Session.Get<ApplicationUser>("UserInfo");
            Section = await _context.Section
                .Include(c => c.Course).FirstOrDefaultAsync(m => m.Id == id);
            Enrollment = new Enrollment();
            Enrollment.SectionId = Section.Id;
            Enrollment.StudentId = user.Id;

            Charge = new Charge();
            Charge.Reason = "Registration for " + Section.Course.Subject + " " + Section.Course.Number + " Section " + Section.Id;
            Charge.Date = DateTime.Now;
            Charge.Amount = Section.Course.CreditHours * SD.CostPerCredit + Section.Course.CourseFee;
            Charge.StudentId = user.Id;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Enrollment.Add(Enrollment);
            //await _context.SaveChangesAsync();
            DataAccessLayer.DataAccessClass dbAccess = new DataAccessLayer.DataAccessClass();
            dbAccess.AddEnrollment(Enrollment);
            dbAccess.AddCharge(Charge);
            
            //add new section to the session
            List<CourseInfo> CourseInfos = HttpContext.Session.Get<List<CourseInfo>>("CourseInfo");
            //Course testCourse = _context.Section.Include(e => e.Course).FirstOrDefault(c => c.Id == Enrollment.SectionId).Course;
            CourseInfo temp = new CourseInfo()
            {
                Course = _context.Section.Include(e => e.Course).FirstOrDefault(c => c.Id == Enrollment.SectionId).Course,
                SectionId = Enrollment.SectionId,
                Notifications = new List<Notification>(_context.Notification.Where(n => n.SectionId == Enrollment.SectionId).ToList()),
                Assignments = new List<Assignment>(_context.Assignment.Where(a => a.SectionId == Enrollment.SectionId).ToList())
            };
            CourseInfos.Add(temp);
            HttpContext.Session.Set<List<CourseInfo>>("CourseInfo", CourseInfos);


            return RedirectToPage("./Index");
        }
    }
}
