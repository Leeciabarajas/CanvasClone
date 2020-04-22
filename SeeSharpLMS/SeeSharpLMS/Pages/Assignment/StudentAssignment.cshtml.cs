using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;
using SeeSharpLMS.Utility;

namespace SeeSharpLMS
{
    public class StudentAssignmentModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentAssignmentModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IList<StudentAssignment> StudentAssignment { get; set; }
        
        public async Task OnGetAsync(int? sectionID)
        {

            StudentAssignment = await _context.StudentAssignment.Where(s => s.StudentId == _userManager.GetUserId(User))
                .Where(s => s.Assignment.SectionId == sectionID)
                .Include(s => s.ApplicationUser)
                    .Include(s => s.Assignment)
                    .ToListAsync();

        }
    }
}
