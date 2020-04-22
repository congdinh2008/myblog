using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Presentation.Data
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(50, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(50, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 2)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(255, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 10)]
        public string Intro { get; set; }

        [Required(ErrorMessage = "The {0} must be required")]
        [StringLength(1024, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 10)]
        public string Profile { get; set; }

        [StringLength(1024, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 5)]
        public string Avatar { get; set; }

        [StringLength(255, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 5)]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 5)]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least 2 characters long", MinimumLength = 5)]
        public string Country { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }
    }
}
