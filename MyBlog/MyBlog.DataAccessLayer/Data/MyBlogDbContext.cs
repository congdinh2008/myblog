using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models;

namespace MyBlog.DataAccessLayer.Data
{
    public class MyBlogDbContext : IdentityDbContext<User>
    {
        public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<PostCategory> PostCategories { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            // Config Many to Many between Category and Post
            builder.Entity<PostCategory>()
                .HasKey(pc => new { pc.PostId, pc.CategoryId });

            builder.Entity<PostCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.PostCategories)
                .HasForeignKey(pc => pc.CategoryId);

            builder.Entity<PostCategory>()
                .HasOne(pc => pc.Post)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(pc => pc.PostId);

            // Config Many to Many between Post and Tag
            builder.Entity<PostTag>().HasKey(pt => new { pt.PostId, pt.TagId });

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaving();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaving()
        {

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Modified:

                        // Set the modified date to "now"
                        ((BaseEntity)entityEntry.Entity).ModifiedDate = DateTime.Now;

                        // Mark property as "don't touch"; don't want to update on a Modify operation
                        entityEntry.Property("CreatedDate").IsModified = false;

                        break;

                    case EntityState.Added:

                        // set both updated &amp; created date to "now"
                        ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;

                        ((BaseEntity)entityEntry.Entity).ModifiedDate = DateTime.Now;

                        break;
                }
            }
        }
    }
}
