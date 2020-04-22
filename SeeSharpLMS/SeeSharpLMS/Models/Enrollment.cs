using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Section Id")]
        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }
        
        [Display(Name = "Student Id")]
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Final Grade")]
        public float? FinalGrade { get; set; }

       
    }
}
