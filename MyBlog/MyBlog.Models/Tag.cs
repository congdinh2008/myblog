using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Tag : BaseEntity
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 3)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 3)]
        public string Slug { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 10)]
        public string Content { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}