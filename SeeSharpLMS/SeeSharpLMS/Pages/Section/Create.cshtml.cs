using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;
using Microsoft.AspNetCore.Identity;
using SeeSharpLMS.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using SeeSharpLMS.Utility;

namespace SeeSharpLMS
{
    public class SectionCreateModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationUser user;
        public List<string> Days { get; set; }

        public SectionCreateModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        //ViewData["InstructorId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
        ViewData["CourseId"] = new SelectList(_context.Course, "Id", "DisplayCourse");
        ViewData["TermId"] = new SelectList(_context.Term, "Id", "TermDisplay");
        user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            return Page();
        }

        [BindProperty]
        public Section Section { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //Fills Days with list of checked days.
            Days = Request.Form["Days"].ToList();

            //Initializes Section.Days to empty string.
            Section.Days = "";

            //For each day in list, append to Section.Days.
            foreach (var x in Days)
            {
                Section.Days += x.ToString();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }


            DataAccessLayer.DataAccessClass dbAccess = new DataAccessLayer.DataAccessClass();
            dbAccess.AddSection(Section);
            //_context.Section.Add(Section);
            //await _context.SaveChangesAsync();

            //add new section to the session
            List<CourseInfo> CourseInfos = HttpContext.Session.Get<List<CourseInfo>>("CourseInfo");
            CourseInfo temp = new CourseInfo() { 
                Course = _context.Course.FirstOrDefault(c => c.Id == Section.CourseId),
                SectionId = Section.Id,
                Notifications = new List<Notification>(),
                Assignments = new List<Assignment>()
            };
            CourseInfos.Add(temp);
            HttpContext.Session.Set<List<CourseInfo>>("CourseInfo", CourseInfos);
            return RedirectToPage("./Index");
        }
    }
}
