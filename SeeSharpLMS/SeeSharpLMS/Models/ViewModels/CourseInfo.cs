using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models.ViewModels
{
    public class CourseInfo
    {
        public Course Course { get; set; }

        public int SectionId { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<NotificationUpdated> NotificationUpdateds { get; internal set; }
    }
}
