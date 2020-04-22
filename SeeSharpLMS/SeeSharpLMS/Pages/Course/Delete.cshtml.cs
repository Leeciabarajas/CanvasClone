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
    public class CourseDeleteModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;

        public CourseDeleteModel(SeeSharpLMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FirstOrDefaultAsync(m => m.Id == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FindAsync(id);

            if (Course != null)
            {
                //_context.Course.Remove(Course);
                //await _context.SaveChangesAsync();
                DataAccessLayer.DataAccessClass dbAccess = new DataAccessLayer.DataAccessClass();
                dbAccess.RemoveCourse(Course);
            }

            return RedirectToPage("./Index");
        }
    }
}
