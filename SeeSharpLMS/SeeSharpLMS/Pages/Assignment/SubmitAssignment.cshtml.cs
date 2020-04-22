using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Infrastructure;
using SeeSharpLMS.Models;

namespace SeeSharpLMS
{
    public class SubmitAssignmentModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        private IHostingEnvironment _hostingEnvironment;

        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SubmitAssignmentModel(SeeSharpLMS.Data.ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IUnitOfWork unitOfWork,
            IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        public StudentAssignment StudentAssignment { get; set; }
        [BindProperty]
        public Submission Submission { get; set; }

        public InputModel Input { get; set; }

        public class InputModel
        {
            public string FilePath { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //ViewData["AssignmentTypeId"] = new SelectList(_context.AssignmentType, "Id", "Description");
            ViewData["StudentAssignmentId"] = new SelectList(_context.StudentAssignment, "Id", "Id");
            if (id == null)
            {
                return NotFound();
            }
            
            StudentAssignment = await _context.StudentAssignment
                .Include(s => s.Assignment)
                .Include(s => s.ApplicationUser).FirstOrDefaultAsync(s => s.Id == id);
            
            return Page();
        }

        
       
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            if (file != null)
            {
                _unitOfWork.UploadFile(file);
                Submission.FilePath = file.FileName;
            }

            Submission.Date = DateTime.Now;

            _context.Submission.Add(Submission);

            await _context.SaveChangesAsync();
            
            return RedirectToPage("./SubmittedPage");
        }

    }
}
