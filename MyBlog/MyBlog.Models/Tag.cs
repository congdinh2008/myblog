using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Tag
    {
        public string TagId { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 10)]
        [Display(Name = "Category Name")]
        public string TagName { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 10)]
        public string Slug { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 20)]
        public string Content { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}