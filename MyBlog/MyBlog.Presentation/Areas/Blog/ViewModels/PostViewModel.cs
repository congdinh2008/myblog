using MyBlog.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Presentation.Areas.Blog.ViewModels
{
    public class PostViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 5)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 5)]
        public string Slug { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 20)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 20)]
        public string Content { get; set; }

        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 5)]
        [Display(Name = "Thumbnail Url")]
        public string ThumbnailUrl { get; set; }

        [DefaultValue(5)]
        public double Rating { get; set; }

        [DefaultValue(0)]
        public int Views { get; set; }

        [DefaultValue(false)]
        public bool Published { get; set; }

        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        public List<string> selectedCategories { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public List<string> selectedTags { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
