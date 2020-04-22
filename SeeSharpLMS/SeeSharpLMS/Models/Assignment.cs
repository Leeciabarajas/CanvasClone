using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Section Id")]
        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }

        [Display(Name ="Assignment Type Id")]
        public int AssignmentTypeId { get; set; }
        [ForeignKey("AssignmentTypeId")]
        public virtual AssignmentType AssignmentType { get; set; }

        [Required]
        [Display(Name ="Point Value")]
        public float PointValue { get; set; }

        [Required]
        public string Title { get; set; }

        
        public string Instructions { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CloseDate { get; set; }

        public DateTime OpenDate { get; set; }


        public Boolean Flag { get; set; } = false;
    }
}
