using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeeSharpLMS.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using SeeSharpLMS.Utility;
using SeeSharpLMS.Models.ViewModels;
using System;
using Microsoft.EntityFrameworkCore;

namespace SeeSharpLMS
{
    public class LandingPageModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationUser user;
        public List<CourseInfo> CourseInfos;

        public IList<Section> Section { get; set; }
        public IList<Assignment> Assignment { get; set; }
        public IList<StudentAssignment> studentAssignment { get; set; }

        public IList<Submission> Submissions { get; set; }
        //public bool IsInstructor;

        //public Instructor instructor;

        //public Student student;

        public DateTime time;

        public LandingPageModel(Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.Get<List<CourseInfo>>("CourseInfo") == null)
            {
                CourseInfos = new List<CourseInfo>();

                user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));

                //get the instructor if possible
                if (User.IsInRole(SD.InstructorRole))
                {
                    //get the courses the instructor teaches
                    var query2 = _context.Section.Where(s => s.InstructorId == user.Id);
                    foreach (Section section in query2.ToList())
                    {
                        CourseInfos.Add(new CourseInfo
                        {
                            Course = _context.Course.Find(section.CourseId),
                            SectionId = section.Id,
                            Notifications = new List<Notification>(_context.Notification
                                    .Where(n => n.SectionId == section.Id)
                                    .Include(n => n.Section)
                                    .ToList()),
                            Assignments = new List<Assignment>(_context.Assignment
                                    .Where(a => a.SectionId == section.Id)
                                    .Include(a => a.AssignmentType)
                                    .ToList())
                            
                        });
                    }
                }
                else if (User.IsInRole(SD.StudentRole)) //get the student
                {
                    //get the courses that the student is enrolled in
                    List<int> SectionIds = new List<int>();
                    var query = _context.Enrollment.Where(s => s.StudentId == user.Id);
                    foreach (Enrollment enrollment in query.ToList())
                    {
                        SectionIds.Add(enrollment.SectionId);
                    }

                    var query2 = _context.Section.Where(s => SectionIds.Contains(s.Id));
                    foreach (Section section in query2.ToList())
                    {
                        CourseInfos.Add(new CourseInfo
                        {
                            Course = _context.Course.Find(section.CourseId),
                            SectionId = section.Id,
                            Notifications = new List<Notification>(_context.Notification.Where(n => n.SectionId == section.Id).ToList()),
                            Assignments = new List<Assignment>(_context.Assignment
                                       .Where(a => a.SectionId == section.Id)
                                       .Include(a => a.AssignmentType)
                                       .ToList())
                        });
                    }
                }
                HttpContext.Session.Set<List<CourseInfo>>("CourseInfo", CourseInfos);
                HttpContext.Session.Set<ApplicationUser>("UserInfo", user);
            }
            else
            {
                user = HttpContext.Session.Get<ApplicationUser>("UserInfo");
                CourseInfos = HttpContext.Session.Get<List<CourseInfo>>("CourseInfo");
            }

            if (User.IsInRole(SD.InstructorRole))
            {
                //Instructor Assignment
                Section = await _context.Section.Where(s => s.InstructorId == _userManager.GetUserId(User))
                    .Include(s => s.ApplicationUser)
                    .Include(s => s.Course)
                    .Include(s => s.Term).ToListAsync();
                Submissions = await _context.Submission.Where(s => s.StudentAssignment.Assignment.Section.InstructorId == _userManager.GetUserId(User))
                    .Include(s => s.StudentAssignment)
                    .ThenInclude(s => s.Assignment)
                    .ThenInclude(s => s.Section)
                    .ThenInclude(s => s.ApplicationUser)
                    .ToListAsync();
                //Student Assignment
                studentAssignment = await _context.StudentAssignment.Where(sa => sa.StudentId == _userManager.GetUserId(User))
                    .Include(sa => sa.ApplicationUser)
                    .Include(sa => sa.Assignment).ToListAsync();
            }

            else
            {
                Submissions = await _context.Submission.Where(s => s.StudentAssignment.StudentId == _userManager.GetUserId(User))
                    .Include(s => s.StudentAssignment)
                    .ThenInclude(s => s.Assignment)
                    .ThenInclude(s => s.Section)
                    .ThenInclude(s => s.ApplicationUser)
                    .ToListAsync();
                //Student Assignment
                studentAssignment = await _context.StudentAssignment.Where(sa => sa.StudentId == _userManager.GetUserId(User))
                    .Include(sa => sa.ApplicationUser)
                    .Include(sa => sa.Assignment).ToListAsync();
                
            }

            return Page();
        }
    }
}