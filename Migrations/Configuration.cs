namespace Assignment_1.Migrations
{
    using Assignment_1.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Assignment_1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Assignment_1.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            ApplicationUser adminUser = null;

            if (!context.Users.Any(p=> p.UserName == "admin@assignment1.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "admin@assignment1.com";
                adminUser.Email = "admin@assignment1.com";
                adminUser.FirstName = "Admin";
                adminUser.LastName = "User";
                adminUser.DisplayName = "Admin User";

                userManager.Create(adminUser, "Password-1");
            }
            else
            {
                adminUser = context.Users.Where(p => p.UserName == "admin@assignment1.com")
                    .FirstOrDefault();
            }
            if (!userManager.IsInRole(adminUser.Id, "Admin"))
            {
                userManager.AddToRole(adminUser.Id, "Admin");
            }
        }

    }
}
