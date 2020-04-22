using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }
        
        public int StudentAssignmentId { get; set; }
        [ForeignKey("StudentAssignmentId")]
        public virtual StudentAssignment StudentAssignment { get; set; }

        
        public string TextEntry { get; set; }

        public string FilePath { get; set; }

        public string Link { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Boolean Flag { get; set; }
    }
}
