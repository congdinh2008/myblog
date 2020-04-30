using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    public class Comment : BaseEntity
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(1024, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 20)]
        public string Content { get; set; }

        [DefaultValue(false)]
        public bool Approved { get; set; }

        [Display(Name = "Approved Date")]
        public DateTime ApprovedDate { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Post")]
        public string PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}