using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;

namespace SeeSharpLMS
{
    public class GradingEditModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;

        public GradingEditModel(SeeSharpLMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Submission> Submission { get;set; }
        [BindProperty]
        public StudentAssignment Input { get; set; }
        
        public async Task OnGetAsync(int? id)
        {
            Input =  _context.StudentAssignment
                         .Include(a => a.Assignment)
                         .Include(u => u.ApplicationUser).FirstOrDefault(a => a.Id == (int)id);

            Submission = await _context.Submission
                .Include(s => s.StudentAssignment)
                    .ThenInclude(u => u.ApplicationUser)
                .Include(s => s.StudentAssignment)
                    .ThenInclude(a => a.Assignment)
                .Where(s => s.StudentAssignmentId == (int)id)
                .OrderByDescending(s => s.Date).ToListAsync();

        }

        public IActionResult OnPost()
        {
            
            _context.StudentAssignment.Update(Input);

            if (Input.Grade != null)
            {
                
            }

            _context.SaveChanges();
            return RedirectToPage("Index", new { id = Input.AssignmentId});
        }
    }
}
