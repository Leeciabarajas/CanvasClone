using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Models;
using SeeSharpLMS.Utility;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;



namespace SeeSharpLMS.DataAccessLayer
{
    public class DataAccessClass
    {

        //private readonly UserManager<IdentityUser> _userManager;
        public readonly Data.ApplicationDbContext _context;

        public DataAccessClass()
        {
            DbContextOptions<SeeSharpLMS.Data.ApplicationDbContext> options = new DbContextOptions<SeeSharpLMS.Data.ApplicationDbContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Server=titan.cs.weber.edu,10433;Initial Catalog=LMS_SeeSharp;PersistSecurityInfo=False;User ID=seesharp;Password=Password*1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);
            _context = new SeeSharpLMS.Data.ApplicationDbContext((DbContextOptions<SeeSharpLMS.Data.ApplicationDbContext>)builder.Options);
        }

        
        //User methods
        public void UpdateUser(Models.ApplicationUser user, string firstname, string lastname, string email, string description)
        {
            var updateUser = _context.ApplicationUser.FirstOrDefault(u => u.Id == user.Id);
            //assign firstname 
            updateUser.FirstName = firstname;
            
            //assign last name 
            updateUser.LastName = lastname;
          
            //assign things related to email 
            updateUser.Email = email;
            updateUser.UserName = email;
            updateUser.NormalizedEmail = email.ToUpper();
            updateUser.NormalizedUserName = email.ToUpper();
 
            //assign description 
            updateUser.Description = description;
   
            _context.SaveChanges();
        }

        //Submission methods
        public void AddSubmission(Models.Submission submission, int studentAssignmentId)
        {
            submission = new Models.Submission();
            submission.StudentAssignmentId = studentAssignmentId;

            _context.Submission.Add(submission);
            _context.SaveChanges();

        }


        public void RemoveSubmission(int studentAssignmentId)
        {
            Models.Submission submission = _context.Submission.FirstOrDefault(s => s.StudentAssignmentId == studentAssignmentId);
            _context.Submission.Remove(submission);
            _context.SaveChanges();
        }

        async public void UpdateSubmission(Models.Submission submission)
        {
            _context.Attach(submission).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }


        //Assignment methods
        public void AddAssignment(Models.ApplicationUser user, int sectionid)
        {
            Models.Assignment assignment = new Models.Assignment();
            assignment.SectionId = sectionid;

            _context.Assignment.Add(assignment);
            _context.SaveChanges();

        }

        async public void AddAssignment(Models.Assignment assignment)
        {
            _context.Assignment.Add(assignment);
            List<Enrollment> enrollments = _context.Enrollment.Where(x => x.SectionId == assignment.SectionId).ToList();
            await _context.SaveChangesAsync();
            foreach (Enrollment x in enrollments)
            {
                StudentAssignment studentAssignment = new StudentAssignment();
                studentAssignment.StudentId = x.StudentId;
                studentAssignment.Grade = null;
                studentAssignment.AssignmentId = assignment.Id;
                _context.Add(studentAssignment);
                _context.SaveChanges();
            };
        }

        async public void UpdateAssignment(Models.Assignment assignment)
        {
            _context.Attach(assignment).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        async public void RemoveAssignment(Models.Assignment assignment)
        {
            List<StudentAssignment> studentAssignments = _context.StudentAssignment.Where(x => x.AssignmentId == assignment.Id).ToList();
            foreach(StudentAssignment x in studentAssignments)
            {
                _context.StudentAssignment.Remove(x);
                _context.SaveChanges();
            }



            _context.Assignment.Remove(assignment);
            await _context.SaveChangesAsync();
        }
        //In the add/delete assignment functions, make sure to add/delete a studentAssignment 
        // entry in the studentAssignment table for every student in the section.
        //public void AddStudentAssignment()
        //{

        //    _context.
        //    _context.SaveChanges();
        //}




        //Enrollment Methods
        public void AddEnrollment(Models.ApplicationUser user, int sectionid)
        {
            Models.Enrollment enrollment = new Models.Enrollment();
            enrollment.SectionId = sectionid;
            enrollment.StudentId = user.Id;

            var assignments = _context.Assignment.Where(a => a.SectionId == enrollment.SectionId).ToList();
            foreach (var assignment in assignments)
            {
                StudentAssignment studentAssignment = new StudentAssignment
                {
                    StudentId = enrollment.StudentId,
                    AssignmentId = assignment.Id
                };
                _context.StudentAssignment.Add(studentAssignment);
            }

            _context.Enrollment.Add(enrollment);
            _context.SaveChanges();
        }

        public void AddEnrollment(Models.Enrollment enrollment)
        {
            var assignments = _context.Assignment.Where(a => a.SectionId == enrollment.SectionId).ToList();
            foreach(var assignment in assignments)
            {
                StudentAssignment studentAssignment = new StudentAssignment
                {
                    StudentId = enrollment.StudentId,
                    AssignmentId = assignment.Id
                };
                _context.StudentAssignment.Add(studentAssignment);
            }
            _context.Enrollment.Add(enrollment);
            _context.SaveChanges();
        }

        public void AddCharge (Models.Charge charge)
        {
            _context.Charge.Add(charge);
            _context.SaveChanges();
        }
        
        public void RemoveEnrollment(Models.ApplicationUser user, int sectionId)
        {
            Models.Enrollment enrollment = _context.Enrollment.FirstOrDefault(e => e.StudentId == user.Id && e.SectionId == sectionId);

            var assignments = _context.Assignment.Where(a => a.SectionId == enrollment.SectionId);

            foreach( var assignment in assignments)
            {
                _context.StudentAssignment.Remove(_context.StudentAssignment.First(s => s.AssignmentId == assignment.Id && s.StudentId == enrollment.StudentId));
            }
            _context.Enrollment.Remove(enrollment);
            _context.SaveChanges();
        }

        public void RemoveEnrollment(Models.Enrollment enrollment)
        {
            _context.Enrollment.Remove(enrollment);
            _context.SaveChanges();
        }



        //Section methods
        async public void AddSection(Models.Section section)
        {
            _context.Section.Add(section);
            await _context.SaveChangesAsync();
        }

        async public void UpdateSection(Models.Section section)
        {
            _context.Attach(section).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        async public void RemoveSection(Models.Section section)
        {
            _context.Section.Remove(section);
            await _context.SaveChangesAsync();
        }

        //Course methods
        async public void AddCourse(Models.Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();
        }

        async public void UpdateCourse(Models.Course course)
        {
            _context.Attach(course).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        async public void RemoveCourse(Models.Course course)
        {
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
        }

    }   
}
