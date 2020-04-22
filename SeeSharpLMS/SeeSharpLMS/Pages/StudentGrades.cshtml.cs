using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;
using SeeSharpLMS.Utility;

namespace SeeSharpLMS
{
    public class StudentGradesModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public StudentGradesModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IList<StudentAssignment> StudentAssignment { get; set; }
        public IList<StudentAssignment> grades { get; set; }

        public async Task OnGetAsync(int? sectionID)
        {

            StudentAssignment = await _context.StudentAssignment.Where(s => s.StudentId == _userManager.GetUserId(User))
                .Where(s => s.Assignment.SectionId == sectionID)
                .Include(s => s.ApplicationUser)
                    .Include(s => s.Assignment)
                    .ToListAsync();

            foreach(StudentAssignment x in StudentAssignment)
            {
                IList<float?> allGrades = _context.StudentAssignment.Where(s => s.AssignmentId == x.AssignmentId).Where(y => y.Grade.HasValue).Select(g => g.Grade).OrderBy(n => n).ToList();
                if (allGrades.Count() > 0)
                {
                    x.AverageScore = allGrades.Average();
                    x.MinScore = allGrades[0];
                    x.MaxScore = allGrades[allGrades.Count()-1];
                    x.Q1 = allGrades[allGrades.Count() * 1 / 4];
                    x.Q3 = allGrades[allGrades.Count() * 3 / 4];
                    System.Console.WriteLine();
                }
            }
        }
    }
}
