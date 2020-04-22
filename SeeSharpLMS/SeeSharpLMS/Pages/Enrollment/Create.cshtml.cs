using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;




namespace SeeSharpLMS
{
    public class EnrollmentCreateModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationUser user;

        public EnrollmentCreateModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Section> Section { get; set; }
        public List<SelectListItem> Options { get; set; }

        public IActionResult OnGet()
        {
            ViewData["StudentId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            //ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Course");
            Options = _context.Section.Select(a =>
                                                new SelectListItem
                                                {
                                                    Value = a.Id.ToString(),
                                                    Text = a.Course.DisplayCourse
                                                }).ToList();

            ViewData["SectionId"] = new SelectList(Options, "Value", "Text");


            user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            return Page();
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Enrollment.Add(Enrollment);
            //await _context.SaveChangesAsync();
            DataAccessLayer.DataAccessClass dbAccess = new DataAccessLayer.DataAccessClass();
            dbAccess.AddEnrollment(Enrollment);

            return RedirectToPage("./Index");
        }
    }
}
