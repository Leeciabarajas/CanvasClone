using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class NotificationUpdated
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Submission Id")]
        public int SubmissionId { get; set; }
        [ForeignKey("SubmissionId")]
        public virtual Submission Submission { get; set; }

        public string CheckPoint { get; set; }
    }
}
