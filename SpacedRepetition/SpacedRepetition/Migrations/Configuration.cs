namespace SpacedRepetition.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SpacedRepetition.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SpacedRepetition.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SpacedRepetition.Models.ApplicationDbContext context)
        {
            string[] roles = new string[] { "admin", "basicuser" };
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            foreach (string role in roles)
            {

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }


            var user = new ApplicationUser
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

            };

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                userManager.Create(user, "Pass#1");
                userManager.AddToRole(user.Id, "admin");

            }

        }



    }
}