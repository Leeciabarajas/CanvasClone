using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;

namespace SeeSharpLMS
{
    public class SectionIndexModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SectionIndexModel(SeeSharpLMS.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Section> Section { get;set; }

        public async Task OnGetAsync()
        {
            //Get all the sections and necessary information for courses taught by the active instructor
            Section = await _context.Section.Where(s=>s.InstructorId == _userManager.GetUserId(User))
                .Include(s => s.ApplicationUser)
                .Include(s => s.Course)
                .Include(s => s.Term).ToListAsync();
        }
    }
}
