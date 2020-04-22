using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class Section
    {
        [Key]
        [Display(Name ="Section Id")]
        public int Id { get; set; }
        
        [Display(Name ="Instructor Id")]
        public string InstructorId { get; set; }
        [ForeignKey("InstructorId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        [Required]
        [Display(Name ="Course Id")]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        [Required]
        [Display(Name = "Term")]
        public int TermId { get; set; }
        [ForeignKey("TermId")]
        public virtual Term Term { get; set; }

        //start time
        [Display(Name ="Start Time")]
        public TimeSpan StartTime { get; set; }
        //end time
        [Display(Name ="End Time")]
        public TimeSpan EndTime { get; set; }
        //days of the week
        public string Days { get; set; }
        //place
        public string Location { get; set; }
        //student cap
        [Display(Name ="Enrollment Cap")]
        public int EnrollmentCap { get; set; }

        [NotMapped]
        [Display(Name ="Meeting Time")]
        public string DisplayTime { get { return StartTime.ToString().Substring(0,5) + " - " + EndTime.ToString().Substring(0,5); } }

        //[NotMapped]
        //[Display(Name ="Course Name")]
        //public string CourseInfo { get { return Course.DisplayCourse; } }
    }
}
