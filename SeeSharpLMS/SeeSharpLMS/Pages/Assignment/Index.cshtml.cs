using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;
using SeeSharpLMS.Models.ViewModels;
using SeeSharpLMS.Utility;

namespace SeeSharpLMS
{
    public class IndexModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        //public List<CourseInfo> CourseInfos;



        //public string displayCourse { get; set; }
        public ApplicationUser user { get; set; }

        public IndexModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Assignment> Assignment { get; set; }
        public IList<Section> section { get; set; }
        public IList<StudentAssignment> StudentAssignment { get; set; }

        public Analytics Analytics { get; set; }

        public async Task OnGetAsync(int? sectionID)
        {
            Analytics = new Analytics();
            if (sectionID != null)
            {
                HttpContext.Session.SetInt32("SectionID", (int)sectionID);
            }
            else
            {
                sectionID = HttpContext.Session.GetInt32("SectionID");
            }

            if (User.IsInRole(SD.InstructorRole))
            {
                Assignment = await _context.Assignment.Where(a => a.SectionId == sectionID)
                            .Include(a => a.Section)
                            .Include(a => a.AssignmentType).ToListAsync();

                IEnumerable<StudentAssignment> query = _context.StudentAssignment
                                        .Include(a => a.Assignment).ToList();

                var StudentGrades = query.Where(s => s.Grade != null && s.Assignment.SectionId == sectionID)
                                        .GroupBy(a => new { a.Assignment.SectionId, a.StudentId })
                                        .Select(g => g.Sum(a => a.Grade) / g.Sum(a => a.Assignment.PointValue));

                Analytics.GradeA = StudentGrades.Where(g => g >= 0.9).ToList().Count();
                Analytics.GradeB = StudentGrades.Where(g => g >= 0.8 && g < 0.9).ToList().Count();
                Analytics.GradeC = StudentGrades.Where(g => g >= 0.7 && g < 0.8).ToList().Count();
                Analytics.GradeD = StudentGrades.Where(g => g >= 0.6 && g < 0.7).ToList().Count();
                Analytics.GradeF = StudentGrades.Where(g => g < 0.6).ToList().Count();
            }
            else
            {
                StudentAssignment = await _context.StudentAssignment.Where(s => s.StudentId == _userManager.GetUserId(User))
                    .Include(s => s.ApplicationUser)
                    .Include(s => s.Assignment).ToListAsync();
            }
        }
    }
}
