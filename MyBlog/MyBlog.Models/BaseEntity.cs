using System;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public abstract class BaseEntity
    {
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }
    }
}
