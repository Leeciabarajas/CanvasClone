using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class Term
    {
        [Key]
        public int Id { get; set; }
        public string Block { get; set; }
        public string Year { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime CloseDate { get; set; }

        [NotMapped]
        [Display(Name ="Term")]
        public string TermDisplay { get { return Block + " " + Year; } }
    }
}
