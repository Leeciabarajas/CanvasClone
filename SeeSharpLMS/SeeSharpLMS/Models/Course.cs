using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Course Subject")]
        public string Subject { get; set; }
        [Required]
        [Display(Name ="Course Number")]
        public string Number { get; set; }
        [Required]
        [Display(Name = "Course Title")]
        public string Title { get; set; }

        [Required]
        //Course Description
        public string Description { get; set; }
        [Required]
        [Display(Name ="Credit Hours")]
        //Credit Hours
        public int CreditHours { get; set; }
        [Display(Name ="Course Fee")]
        [DataType(DataType.Currency)]
        //Course Fee
        public float CourseFee { get; set; }

        [NotMapped]
        [Display(Name = "Course Name")]
        public string DisplayCourse { get { return Subject + " " + Number + " " + Title; } }
    }
}
