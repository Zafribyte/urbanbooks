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
            if (!context.Users.Any(myUser => myUser.UserName == "admin@urbanbooks.co.za"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var roleStore = new RoleStore<IdentityRole>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var user = new ApplicationUser { UserName = "admin@urbanbooks.co.za", Email = "admin@urbanbooks.co.za" };
                user.Wishlists = new Wishlist { Status = false };
                user.Carts = new Cart { DateLastModified = DateTime.Now };
                userManager.Create(user, "password");
                roleManager.Create(new IdentityRole { Name = "admin" });
                userManager.AddToRole(user.Id, "admin");

            }
        }
    }
}
