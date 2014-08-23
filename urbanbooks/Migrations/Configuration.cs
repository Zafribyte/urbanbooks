namespace urbanbooks.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using urbanbooks.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<urbanbooks.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(urbanbooks.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string name = "admin@urbanbooks.com";
            string password = "password";
            string test = "admin";

            //Create Role Test and User Test
           // RoleManager.Create(new IdentityRole(test));
            //UserManager.Create(new ApplicationUser() { UserName = test });

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(test))
            {
                var roleresult = RoleManager.Create(new IdentityRole(test));
            }

            //Create User=Admin with password=123456
            var user = new ApplicationUser();
            user.UserName = name;
            user.Email = name;
            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, test);
            }
        }
    }
}
