using Microsoft.AspNetCore.Identity;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBlog.DataAccessLayer.Data
{
    public static class DbInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, MyBlogDbContext context)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);

            SeedBlogData(context);
        }

        private static void SeedBlogData(MyBlogDbContext context)
        {
            if (!context.Categories.Any(x => x.CategoryName == "Visual Studio"))
            {
                var users = context.Users.ToList();
                #region Categories
                var categories = new List<Category>()
                {
                     new Category
                    {
                        CategoryId = Guid.NewGuid().ToString(),
                        CategoryName = "Visual Studio",
                        Slug = "visual-studio",
                        Content = "About Visual Studio",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    },
                    new Category
                    {
                        CategoryId = Guid.NewGuid().ToString(),
                        CategoryName = ".NET",
                        Slug = "net",
                        Content = "About .NET",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    },
                    new Category
                    {
                        CategoryId = Guid.NewGuid().ToString(),
                        CategoryName = "ASP.NET",
                        Slug = "asp-net",
                        Content = "About ASP.NET",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    },
                    new Category
                    {
                        CategoryId = Guid.NewGuid().ToString(),
                        CategoryName = "Programming Language",
                        Slug = "programming-language",
                        Content = "About Programming Language",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    }
                };
                #endregion
                context.Categories.AddRange(categories);

                #region Posts
                var posts = new List<Post>()
                {
                    new Post()
                    {
                        PostId = Guid.NewGuid().ToString(),
                        Title = "Visual Studio 2019 for Mac version 8.5 is now available",
                        Slug = "visual-studio-2019-for-mac-version-8-5-is-now-available",

                        Summary = "Visual Studio 2019 for Mac version 8.5 is available today and includes ASP.NET Core authentication templates," +
                                  " support for Azure Functions 3.0, and improvements to the overall experience for those using assistive technologies.",
                        Content = "Many of our ASP.NET Core developers have requested that we bring the ability to easily create ASP.NET Core apps with authentication to Visual Studio for Mac. " +
                                  "Now, when you create a new ASP.NET Core project that supports No Authentication or Individual Authentication using an in-app store, " +
                                  "you'll encounter an additional screen in the new project creation wizard. " +
                                  "Please give this new feature a try and let us know any feedback you have.",
                        ThumbnailUrl = "a-screenshot-of-a-cell-phone-description-automati.png",
                        Rating = 4.2,
                        Views = 100,
                        Published = true,
                        PublishedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Author = users.Single(u=>u.Email=="admin@congdinh.com")
                    },
                    new Post()
                    {
                        PostId = Guid.NewGuid().ToString(),
                        Title = "Visual Studio 2019 version 16.6 Preview 2 Brings New Features Your Way",
                        Slug = "visual-studio-2019-version-16-6-preview-2",

                        Summary = "Visual Studio 2019 version 16.6 Preview 2 comes with several new, exciting capabilities for you to try today. " +
                                  " We recognize that everyone is facing unprecedented stress and concerns with current world events.",
                        Content = "First of all, we are revamping our Git functionality to provide an improved experience when working with code on remote Git hosting services. " +
                                  "You can begin working on code by browsing online GitHub or Azure repositories through Visual Studio and cloning them locally. " +
                                  "For new projects, you can initialize the Git repository and push it to be hosted on GitHub with a single click. " +
                                  "Once the code is loaded in Visual Studio, the new Git tool window consolidates all the Git operations having to do with your code.",
                        ThumbnailUrl = "16.6_P2_MLNET.png",
                        Rating = 4.5,
                        Views = 103,
                        Published = true,
                        PublishedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Author = users.Single(u=>u.Email=="admin@congdinh.com")
                    },
                    new Post()
                    {
                        PostId = Guid.NewGuid().ToString(),
                        Title = "Default implementations in interfaces",
                        Slug = "default-implementations-in-interfaces",

                        Summary = "With last week's posts Announcing .NET Core 3.0 Preview 5 and Visual Studio 2019 version 16.1 Preview 3, " +
                                  "the last major feature of C# 8.0 is now available in preview.",
                        Content = "A big impediment to software evolution has been the fact that you couldn't add new members to a public interface. " +
                                  "You would break existing implementers of the interface; after all they would have no implementation for the new member!",
                        ThumbnailUrl = "swimlane-cross-platform.png",
                        Rating = 5,
                        Views = 13,
                        Published = true,
                        PublishedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Author = users.Single(u=>u.Email=="cong@congdinh.com")
                    },
                    new Post()
                    {
                        PostId = Guid.NewGuid().ToString(),
                        Title = "Announcing TypeScript 3.9 Beta",
                        Slug = "announcing-typescript-3-9-beta",

                        Summary = "Today we're announcing the availability of TypeScript 3.9 Beta!",
                        Content = "For this release our team been has been focusing on performance, polish, and stability. " +
                                  "We've been working on speeding up the compiler and editing experience, getting rid of friction and papercuts, and reducing bugs and crashes. " +
                                  "We've also received a number of useful and much-appreciated features and fixes from the external community!",
                        ThumbnailUrl = "typescriptfeature.png",
                        Rating = 5,
                        Views = 13,
                        Published = true,
                        PublishedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Author = users.Single(u=>u.Email=="cong@congdinh.com")
                    },
                    new Post()
                    {
                        PostId = Guid.NewGuid().ToString(),
                        Title = "Do more with patterns in C# 8.0",
                        Slug = "do-more-with-patterns-in-c-8-0",

                        Summary = "Visual Studio 2019 Preview 2 is out! And with it, a couple more C# 8.0 features are ready for you to try. " +
                                  "It's mostly about pattern matching, though I'll touch on a few other news and changes at the end.",
                        Content = "When C# 7.0 introduced pattern matching we said that we expected to add more patterns in more places in the future. " +
                                  "That time has come! We're adding what we call recursive patterns, " +
                                  "as well as a more compact expression form of switch statements called (you guessed it!) switch expressions.",
                        ThumbnailUrl = "swimlane-cross-platform.png",
                        Rating = 3.5,
                        Views = 133,
                        Published = true,
                        PublishedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Author = users.Single(u=>u.Email=="cong@congdinh.com")
                    },
                    new Post()
                    {
                        PostId = Guid.NewGuid().ToString(),
                        Title = "Blazor WebAssembly 3.2.0 Preview 4 release now available",
                        Slug = "blazor-webassembly-3-2-0-preview-4-release-now-available",

                        Summary = "A new preview update of Blazor WebAssembly is now available! Here's what's new in this release:",
                        Content = "If you're on Windows using Visual Studio, we recommend installing the latest preview of Visual Studio 2019 16.6. " +
                                  "For this preview you should still install the template from the command-line as described above to ensure that the Blazor WebAssembly template " +
                                  "shows up correctly in Visual Studio and on the command-line.",
                        ThumbnailUrl = "zerodata-build-release-pipeline.png",
                        Rating = 4.7,
                        Views = 1023,
                        Published = true,
                        PublishedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Author = users.Single(u=>u.Email=="cong@congdinh.com")
                    },
                    new Post()
                    {
                        PostId = Guid.NewGuid().ToString(),
                        Title = "ASP.NET Core updates in .NET 5 Preview 2",
                        Slug = "asp-net-core-updates-in-net-5-preview-2",

                        Summary = ".NET 5 Preview2 is now available and is ready for evaluation! .NET 5 will be a current release.",
                        Content = "ASP.NET Core in .NET 5 Preview 2 doesn't include any major new features just yet, but it does include plenty of minor bug fixes. " +
                                  "We expect to announce new features in upcoming preview releases.",
                        ThumbnailUrl = "swimlane-serverless-computing-1.png",
                        Rating = 3.5,
                        Views = 133,
                        Published = true,
                        PublishedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Author = users.Single(u=>u.Email=="van@congdinh.com")
                    }
                };
                #endregion
                context.Posts.AddRange(posts);

                #region Tags
                var tags = new List<Tag>()
                {
                    new Tag
                    {
                        TagId = Guid.NewGuid().ToString(),
                        TagName = "Visual Studio",
                        Slug = "visual-studio",
                        Content = "About Visual Studio",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    },
                    new Tag
                    {
                        TagId = Guid.NewGuid().ToString(),
                        TagName = ".NET",
                        Slug = "net",
                        Content = "About .NET",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    },
                    new Tag
                    {
                        TagId = Guid.NewGuid().ToString(),
                        TagName = "ASP.NET",
                        Slug = "asp-net",
                        Content = "About ASP.NET",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    },
                    new Tag
                    {
                        TagId = Guid.NewGuid().ToString(),
                        TagName = "Programming Language",
                        Slug = "programming-language",
                        Content = "About Programming Language",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    }
                };
                #endregion
                context.Tags.AddRange(tags);

                #region PostTags
                var postTags = new List<PostTag>()
                {
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 for Mac version 8.5 is now available"),
                        Tag = tags.Single(t=>t.TagName=="Visual Studio")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 version 16.6 Preview 2 Brings New Features Your Way"),
                        Tag = tags.Single(t=>t.TagName=="Visual Studio")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Default implementations in interfaces"),
                        Tag = tags.Single(t=>t.TagName=="Programming Language")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Default implementations in interfaces"),
                        Tag = tags.Single(t=>t.TagName==".NET")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Announcing TypeScript 3.9 Beta"),
                        Tag = tags.Single(t=>t.TagName=="Visual Studio")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Announcing TypeScript 3.9 Beta"),
                        Tag = tags.Single(t=>t.TagName=="Programming Language")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Do more with patterns in C# 8.0"),
                        Tag = tags.Single(t=>t.TagName=="Visual Studio")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Do more with patterns in C# 8.0"),
                        Tag = tags.Single(t=>t.TagName=="Programming Language")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Do more with patterns in C# 8.0"),
                        Tag = tags.Single(t=>t.TagName==".NET")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Blazor WebAssembly 3.2.0 Preview 4 release now available"),
                        Tag = tags.Single(t=>t.TagName=="ASP.NET")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="Blazor WebAssembly 3.2.0 Preview 4 release now available"),
                        Tag = tags.Single(t=>t.TagName==".NET")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2"),
                        Tag = tags.Single(t=>t.TagName=="ASP.NET")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2"),
                        Tag = tags.Single(t=>t.TagName=="Visual Studio")
                    },
                    new PostTag
                    {
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2"),
                        Tag = tags.Single(t=>t.TagName==".NET")
                    },
                };
                #endregion
                context.PostTags.AddRange(postTags);

                #region PostCategories

                var postCategories = new List<PostCategory>()
                {
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 for Mac version 8.5 is now available"),
                        Category = categories.Single(t=>t.CategoryName=="Visual Studio")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 version 16.6 Preview 2 Brings New Features Your Way"),
                        Category = categories.Single(t=>t.CategoryName=="Visual Studio")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Default implementations in interfaces"),
                        Category = categories.Single(t=>t.CategoryName=="Programming Language")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Default implementations in interfaces"),
                        Category = categories.Single(t=>t.CategoryName==".NET")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Announcing TypeScript 3.9 Beta"),
                        Category = categories.Single(t=>t.CategoryName=="Visual Studio")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Announcing TypeScript 3.9 Beta"),
                        Category = categories.Single(t=>t.CategoryName=="Programming Language")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Do more with patterns in C# 8.0"),
                        Category = categories.Single(t=>t.CategoryName=="Visual Studio")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Do more with patterns in C# 8.0"),
                        Category = categories.Single(t=>t.CategoryName=="Programming Language")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Do more with patterns in C# 8.0"),
                        Category = categories.Single(t=>t.CategoryName==".NET")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Blazor WebAssembly 3.2.0 Preview 4 release now available"),
                        Category = categories.Single(t=>t.CategoryName=="ASP.NET")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="Blazor WebAssembly 3.2.0 Preview 4 release now available"),
                        Category = categories.Single(t=>t.CategoryName==".NET")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2"),
                        Category = categories.Single(t=>t.CategoryName=="ASP.NET")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2"),
                        Category = categories.Single(t=>t.CategoryName=="Visual Studio")
                    },
                    new PostCategory
                    {
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2"),
                        Category = categories.Single(t=>t.CategoryName==".NET")
                    }
                };

                #endregion
                context.PostCategories.AddRange(postCategories);

                #region Comments
                var comments = new List<Comment>()
                {
                    #region Post 1
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = ".NET 5 will be awesome!",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 for Mac version 8.5 is now available")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "The added messagebox if .NET Core runtime is missing on non self-containd applications is my personal highlight of the .NET Core 3.1 release. I’ve been waiting for this since .NET Core 3.0 before I ship out my ported applications. Thank you so much for adding this!",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 for Mac version 8.5 is now available")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 for Mac version 8.5 is now available")
                    },
                    #endregion

                    #region Post 2
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = ".NET 5 will be awesome!",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 version 16.6 Preview 2 Brings New Features Your Way")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "What happens with Mono after .NET 5?",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="van@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 version 16.6 Preview 2 Brings New Features Your Way")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 version 16.6 Preview 2 Brings New Features Your Way")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="van@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Visual Studio 2019 version 16.6 Preview 2 Brings New Features Your Way")
                    },
                    #endregion

                    #region Post 3
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = ".NET 5 will be awesome!",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Default implementations in interfaces")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "What happens with Mono after .NET 5?",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="van@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Default implementations in interfaces")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Default implementations in interfaces")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="van@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Default implementations in interfaces")
                    },
                    #endregion

                    #region Post 4
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = ".NET 5 will be awesome!",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Announcing TypeScript 3.9 Beta")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "What happens with Mono after .NET 5?",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="van@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Announcing TypeScript 3.9 Beta")
                    },
                    #endregion

                    #region Post 5
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = ".NET 5 will be awesome!",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Do more with patterns in C# 8.0")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "What happens with Mono after .NET 5?",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="van@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Do more with patterns in C# 8.0")
                    },
                    #endregion

                    #region Post 6
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = ".NET 5 will be awesome!",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Blazor WebAssembly 3.2.0 Preview 4 release now available")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "What happens with Mono after .NET 5?",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="van@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Blazor WebAssembly 3.2.0 Preview 4 release now available")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Blazor WebAssembly 3.2.0 Preview 4 release now available")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="van@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="Blazor WebAssembly 3.2.0 Preview 4 release now available")
                    },
                    #endregion

                    #region Post 7
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = ".NET 5 will be awesome!",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "What happens with Mono after .NET 5?",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="van@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="quynh@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="van@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="cong@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2")
                    },
                    new Comment
                    {
                        CommentId = Guid.NewGuid().ToString(),
                        Content = "Will file stream for SQL server be available in asp.net core in .net 5???",
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        User = users.Single(u=>u.Email=="admin@congdinh.com"),
                        Post = posts.Single(p=>p.Title=="ASP.NET Core updates in .NET 5 Preview 2")
                    }
                    #endregion
                };
                #endregion
                context.Comments.AddRange(comments);
                context.SaveChanges();
            }
        }

        private static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync("admin@congdinh.com").Result == null)
            {
                var user = new User()
                {
                    UserName = "admin@congdinh.com",
                    Email = "admin@congdinh.com",
                    FirstName = "John",
                    LastName = "Doe",
                    MiddleName = "Smith",
                    Intro = "My name is John Doe",
                    Profile = "My name is John Doe",
                    Avatar = "avatar-1.jpg",
                    Address = "Co Nhue 2",
                    City = "Ha Noi",
                    Country = "Viet Nam",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                IdentityResult result = userManager.CreateAsync(user, "Abcd@1234").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }

            if (userManager.FindByEmailAsync("cong@congdinh.com").Result == null)
            {
                var user = new User()
                {
                    UserName = "cong@congdinh.com",
                    Email = "cong@congdinh.com",
                    FirstName = "Cong",
                    LastName = "Dinh",
                    MiddleName = "Xuan",
                    Intro = "My name is Cong Dinh",
                    Profile = "My name is Cong Dinh",
                    Avatar = "avatar-2.jpg",
                    Address = "Co Nhue 2",
                    City = "Nam Dinh",
                    Country = "Viet Nam",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                IdentityResult result = userManager.CreateAsync(user, "Abcd@1234").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }

            if (userManager.FindByEmailAsync("van@congdinh.com").Result == null)
            {
                var user = new User()
                {
                    UserName = "van@congdinh.com",
                    Email = "van@congdinh.com",
                    FirstName = "Van",
                    LastName = "Nguyen",
                    MiddleName = "Thi",
                    Intro = "My name is Van Nguyen",
                    Profile = "My name is Van Nguyen",
                    Avatar = "avatar-3.jpg",
                    Address = "Hung Ha",
                    City = "Thai Binh",
                    Country = "Viet Nam",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                IdentityResult result = userManager.CreateAsync(user, "Abcd@1234").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }

            if (userManager.FindByEmailAsync("quynh@congdinh.com").Result == null)
            {
                var user = new User()
                {
                    UserName = "quynh@congdinh.com",
                    Email = "quynh@congdinh.com",
                    FirstName = "Quynh",
                    LastName = "Dinh",
                    MiddleName = "Thi",
                    Intro = "My name is Quynh Dinh",
                    Profile = "My name is Quynh Dinh",
                    Avatar = "avatar-4.jpg",
                    Address = "Ninh Kieu",
                    City = "Can Tho",
                    Country = "Viet Nam",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                IdentityResult result = userManager.CreateAsync(user, "Abcd@1234").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var result = roleManager.CreateAsync(new IdentityRole() { Name = "Administrator" }).Result;
            }

            if (!roleManager.RoleExistsAsync("Manager").Result)
            {
                var result = roleManager.CreateAsync(new IdentityRole() { Name = "Manager" }).Result;
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var result = roleManager.CreateAsync(new IdentityRole() { Name = "User" }).Result;
            }
        }
    }
}
