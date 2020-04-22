using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class ProfileLink
    {
        [Key]
        public int Id { get; set; }

        public string InstructorId { get; set; }
        [ForeignKey("InstructorId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Link { get; set; }
    }
}
