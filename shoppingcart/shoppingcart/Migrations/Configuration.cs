namespace shoppingcart.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<shoppingcart.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(shoppingcart.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                // Uses a named parameter to form a Create method
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "pdking319@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "pdking319@gmail.com",
                    Email = "pdking319@gmail.com",
                    FirstName = "Patrick",
                    LastName = "King",
                    DisplayName = "Patrick"
                }, "password");
            }

            var userId = userManager.FindByEmail("pdking319@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");
        }
    }
}
