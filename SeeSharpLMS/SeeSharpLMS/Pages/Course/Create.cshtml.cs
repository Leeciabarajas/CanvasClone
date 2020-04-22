using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;

namespace SeeSharpLMS
{
    public class CourseCreateModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;

        public CourseCreateModel(SeeSharpLMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Course.Add(Course);
            //await _context.SaveChangesAsync();

            DataAccessLayer.DataAccessClass dbAccess = new DataAccessLayer.DataAccessClass();
            dbAccess.AddCourse(Course);

            return RedirectToPage("./Index");
        }
    }
}
