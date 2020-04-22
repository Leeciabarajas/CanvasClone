using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SeeSharpLMS.Models;
using SeeSharpLMS.Utility;
using Microsoft.EntityFrameworkCore;

namespace SeeSharpLMS
{
    public class CalendarModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationUser user;
        public IList<StudentAssignment> studentAssignment { get; set; }
        public IList<Assignment> assignments { get; set; }
        public IList<int> sections { get; set; }

        public CalendarModel(Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole(SD.InstructorRole))
            {
                //Instructor Assignment
                sections = await _context.Section.Where(s => s.InstructorId == _userManager.GetUserId(User))
                    .Select(x => x.Id).ToListAsync();

                assignments = _context.Assignment.Where(x => sections.Contains(x.SectionId)).ToList();

            }

            else
            {
                //Student Assignment
                studentAssignment = await _context.StudentAssignment.Where(sa => sa.StudentId == _userManager.GetUserId(User))
                    .Include(sa => sa.ApplicationUser)
                    .Include(sa => sa.Assignment).ToListAsync();
            }

            

            return Page();
        }
    }
}