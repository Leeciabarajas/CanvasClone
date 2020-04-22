using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Models;
using SeeSharpLMS.Utility;

namespace SeeSharpLMS
{
    public class GradingIndexModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;

        public GradingIndexModel(SeeSharpLMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        
        public string FilePath { get; set; }
        public Assignment Assignment { get; set; }
        public IList<StudentAssignment> StudentAssignment { get; set; }

        public IList<StudentAssignment> GradeA { get; set; }

        public int GradeACount = 0;

        public IList<StudentAssignment> GradeB { get; set; }

        public int GradeBCount = 0;

        public IList<StudentAssignment> GradeC { get; set; }

        public int GradeCCount = 0;

        public IList<StudentAssignment> GradeD { get; set; }

        public int GradeDCount = 0;


        public IList<StudentAssignment> GradeF { get; set; }

        public int GradeFCount = 0;

        public int GradeCount { get { return GradeACount + GradeBCount + GradeCCount + GradeDCount + GradeFCount; } }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assignment = await _context.Assignment
                .Include(a => a.AssignmentType)
                .Include(a => a.Section).FirstOrDefaultAsync(m => m.Id == id);

            StudentAssignment = _context.StudentAssignment
                .Include(u => u.ApplicationUser)
                .Where(a => a.AssignmentId == Assignment.Id).ToList();

            float points = Assignment.PointValue; 

          //students with 90-100
            GradeA = _context.StudentAssignment 
                .Include(u => u.ApplicationUser)
                .Where(a => a.AssignmentId == Assignment.Id && ((float)a.Grade/points >= .9 )).ToList(); 

            GradeACount = GradeA.Count; 

            //Grab students with grade B 80-89
             GradeB = _context.StudentAssignment //connect to db 
                .Include(u => u.ApplicationUser)
                .Where(a => a.AssignmentId == Assignment.Id && ((float)a.Grade/points >= .8 ) && ((float)a.Grade / points <= .89)).ToList(); 
            GradeBCount = GradeB.Count; 

            //Grab Students with Grade C of 70-79
             GradeC = _context.StudentAssignment //connect to db 
                .Include(u => u.ApplicationUser)
                .Where(a => a.AssignmentId == Assignment.Id && ((float)a.Grade/points >= .7 ) && ((float)a.Grade / points <= .79)).ToList(); 
            GradeCCount = GradeC.Count; 

            //Grab student with Grade D of  60-69
            GradeD = _context.StudentAssignment //connect to db 
                .Include(u => u.ApplicationUser)
                .Where(a => a.AssignmentId == Assignment.Id && ((float)a.Grade/points >= .6 ) && ((float)a.Grade / points < .69)).ToList(); 
            GradeDCount = GradeD.Count;

            //Grab student with Grade F  50 and under 
            GradeF = _context.StudentAssignment //connect to db 
                .Include(u => u.ApplicationUser)
                .Where(a => a.AssignmentId == Assignment.Id && ((float)a.Grade / points <= .5) && ((float)a.Grade/points >= 0)).ToList();
            GradeFCount = GradeF.Count;













            if (Assignment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}