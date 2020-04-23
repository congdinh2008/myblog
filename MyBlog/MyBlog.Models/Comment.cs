using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    public class Comment
    {
        public string CommentId { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 10)]
        [Display(Name = "Category Name")]
        public string TagName { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 10)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(1024, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 20)]
        public string Content { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 10)]
        public string UserIp { get; set; }

        [DefaultValue(false)]
        public bool Approved { get; set; }

        [Display(Name = "Approved Date")]
        public DateTime ApprovedDate { get; set; }

        [Display(Name = "Created Date")]
        public DataType CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

    }
}