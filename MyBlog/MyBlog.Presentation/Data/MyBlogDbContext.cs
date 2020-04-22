using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Presentation.Data
{
    public class MyBlogDbContext : IdentityDbContext
    {
        public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options)
            : base(options)
        {
        }
    }
}
