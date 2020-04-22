using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;
using Microsoft.AspNetCore.Identity;
using SeeSharpLMS.Models.ViewModels;
using SeeSharpLMS.Utility;

namespace SeeSharpLMS
{
    public class SectionEditModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationUser user;
        
        //Used as a list of checked days.
        public List<string> Days { get; set; }


        public SectionEditModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                //.Include(s => s.ApplicationUser)
                .Include(s => s.Course)
                .Include(s => s.Term).FirstOrDefaultAsync(m => m.Id == id);

            if (Section == null)
            {
                return NotFound();
            }
            user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            //ViewData["InstructorId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title");
           ViewData["TermId"] = new SelectList(_context.Term, "Id", "TermDisplay");
            Days = new List<string>();
            for(int i = 0; i < Section.Days.Length; i++)
            {
                Days.Add(Section.Days[i].ToString());
            }
            return Page();
        }

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

            //_context.Attach(Section).State = EntityState.Modified;

            try
            {
                DataAccessLayer.DataAccessClass dbAccess = new DataAccessLayer.DataAccessClass();
                dbAccess.UpdateSection(Section);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(Section.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //add the new section to the session
            List<CourseInfo> CourseInfos = HttpContext.Session.Get<List<CourseInfo>>("CourseInfo");

            CourseInfos.RemoveAll(c => c.SectionId == Section.Id);
            CourseInfo temp = new CourseInfo()
            {
                Course = _context.Course.FirstOrDefault(c => c.Id == Section.CourseId),
                SectionId = Section.Id,
                Notifications = _context.Notification.Where(n => n.Id == Section.CourseId).ToList(),
                Assignments = _context.Assignment.Where(a => a.Id == Section.CourseId).ToList()
            };
            CourseInfos.Add(temp);
            HttpContext.Session.Set<List<CourseInfo>>("CourseInfo", CourseInfos);

            return RedirectToPage("./Index");
        }

        private bool SectionExists(int id)
        {
            return _context.Section.Any(e => e.Id == id);
        }
    }
}
