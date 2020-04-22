using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;
using SeeSharpLMS.Models.ViewModels;
using SeeSharpLMS.Utility;

namespace SeeSharpLMS
{
    public class SectionDeleteModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;

        public SectionDeleteModel(SeeSharpLMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Section Section { get; set; }

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

            if (Section == null)
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
            
            //remove the section from the session
            List<CourseInfo> CourseInfos = HttpContext.Session.Get<List<CourseInfo>>("CourseInfo");
           
            CourseInfos.RemoveAll(c => c.SectionId == Section.Id);
            HttpContext.Session.Set<List<CourseInfo>>("CourseInfo", CourseInfos);

            Section = await _context.Section.FindAsync(id);

            if (Section != null)
            {
                DataAccessLayer.DataAccessClass dbAccess = new DataAccessLayer.DataAccessClass();
                dbAccess.RemoveSection(Section);
                //_context.Section.Remove(Section);
                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
