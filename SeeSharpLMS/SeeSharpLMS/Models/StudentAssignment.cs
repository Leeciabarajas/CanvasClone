using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class StudentAssignment
    {
        [Key]
        public int Id { get; set; }
        
        public int AssignmentId { get; set; }
        [ForeignKey("AssignmentId")]
        public virtual Assignment Assignment { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public float? Grade { get; set; }

        [NotMapped]
        public float? AverageScore { get; set; }
        [NotMapped]
        public float? MinScore { get; set; }
        [NotMapped]
        public float? MaxScore { get; set; }
        [NotMapped]
        public float? Q1 { get; set; }
        [NotMapped]
        public float? Q3 { get; set; }
    }
}
 