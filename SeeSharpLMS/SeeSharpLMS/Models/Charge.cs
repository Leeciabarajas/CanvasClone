using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class Charge
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Reason { get; set; }
        public double Amount{ get; set; }
        public DateTime Date { get; set; }

    }
}
