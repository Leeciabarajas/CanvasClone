using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Models;

namespace SeeSharpLMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Course { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Term> Term { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<AssignmentType> AssignmentType { get; set; }
        public DbSet<Submission> Submission { get; set; }
        public DbSet<StudentAssignment> StudentAssignment { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ProfileLink> ProfileLink { get; set; }
        public DbSet<Charge> Charge { get; set; }

        public DbSet<NotificationUpdated> NotificationUpdateds { get; set; }

    }
}

