using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;
using SeeSharpLMS.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace SeeSharpLMS
{
    public class CreateModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        public int? sectId { get; set; }

        public CreateModel(SeeSharpLMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? sectionID)
        {
        ViewData["AssignmentTypeId"] = new SelectList(_context.AssignmentType, "Id", "Description");
        ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Id");
            if (sectionID != null)
            {
                HttpContext.Session.SetInt32("SectionID", (int)sectionID);
            }
            else
            {
                sectionID = HttpContext.Session.GetInt32("SectionID");
            }
            sectId = sectionID;
            return Page();
        }
        
        [BindProperty]
        public Assignment Assignment { get; set; }
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Assignment.Add(Assignment);
            //await _context.SaveChangesAsync();
            DataAccessLayer.DataAccessClass dbAccess = new DataAccessLayer.DataAccessClass();
            dbAccess.AddAssignment(Assignment);
            
            
            return RedirectToPage("./Index");
        }
    }
}
