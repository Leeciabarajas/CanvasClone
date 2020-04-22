using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Section Id")]
        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }

        public string NotificationLink { get; set; }

        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        [Display(Name ="Publish Date")]
        public DateTime PublishDate { get; set; }
    }
}
