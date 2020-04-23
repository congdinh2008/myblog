using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    public class Post
    {
        public string PostId { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 10)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 10)]
        public string Slug { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 10)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(10000, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 20)]
        public string Content { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 10)]
        [Display(Name = "Meta Title")]
        public string MetaTitle { get; set; }

        [DefaultValue(5)]
        public double Rating { get; set; }

        [DefaultValue(0)]
        public int Views { get; set; }

        [DefaultValue(false)]
        public bool Published { get; set; }

        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        [Display(Name = "Created Date")]
        public DataType CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("User")]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<PostCategory> PostCategories { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}