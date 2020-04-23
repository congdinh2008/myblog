using Microsoft.AspNetCore.Identity;
using MyBlog.Models;
using System;

namespace MyBlog.DataAccessLayer.Data
{
    public static class DbInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
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
                var result = roleManager.CreateAsync(new IdentityRole() {Name = "Administrator"}).Result;
            }

            if (!roleManager.RoleExistsAsync("Manager").Result)
            {
                var result = roleManager.CreateAsync(new IdentityRole() {Name = "Manager"}).Result;
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var result = roleManager.CreateAsync(new IdentityRole() {Name = "User"}).Result;
            }
        }
    }
}
