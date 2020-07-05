namespace MVCPresentationLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCPresentationLayer.Models;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCPresentationLayer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MVCPresentationLayer.Models.ApplicationDbContext";
        }

        protected override void Seed(MVCPresentationLayer.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string admin = "admin@company.com";
            const string adminPassword = "P@ssw0rd";

            LogicLayer.UserManager userMgr = new LogicLayer.UserManager();
            var roles = userMgr.RetrieveAllEmployeeRoles();
            foreach (var role in roles)
            {
                context.Roles.AddOrUpdate(r => r.Name,
                    new IdentityRole() { Name = role });
            }
            if (!roles.Contains("Administrator"))
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Administrator" });
            }

            if(!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin,
                    GivenName = "Admin",
                    FamilyName = "Company"
                };

                IdentityResult result = userManager.Create(user, adminPassword);
                context.SaveChanges();

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                    context.SaveChanges();
                }

            }

        }
    }
}
