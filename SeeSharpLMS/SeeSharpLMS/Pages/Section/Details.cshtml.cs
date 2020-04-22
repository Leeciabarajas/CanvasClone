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
    public class SectionDetailsModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;

        public SectionDetailsModel(SeeSharpLMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Section Section { get; set; }
        public IList<Enrollment> Enrollments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Section = await _context.Section
                .Include(s => s.ApplicationUser)
                .Include(s => s.Course)
                .Include(s => s.Term).FirstOrDefaultAsync(m => m.Id == id);

            Enrollments = _context.Enrollment
                            .Where(e => e.SectionId == id)
                            .Include(u => u.ApplicationUser).ToList();

            if (Section == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
