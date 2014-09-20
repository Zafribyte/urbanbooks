using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace urbanbooks.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public virtual Wishlist Wishlists { get; set; }
        public virtual Cart Carts { get; set; }

    }



    public class Customer
    {
        [ScaffoldColumn(false)]
        public int CustomerID
        { get; set; }
        [ScaffoldColumn(false)]
        public string User_Id
        { get; set; }
        [Display(Name = "Name")]
        public string FirstName
        { get; set; }
        [Display(Name = "Surname")]
        public string LastName
        { get; set; }
    }





    public class Employee
    {
        [ScaffoldColumn(false)]
        public int EmployeeID
        { get; set; }
        [ScaffoldColumn(false)]
        public string User_Id
        { get; set; }
        [Display(Name = "Name")]
        public string FirstName
        { get; set; }
        [Display(Name = "Surname")]
        public string LastName
        { get; set; }
    }



    public class Cart
    {
        [ScaffoldColumn(false)]
        public int CartID
        { get; set; }
        public DateTime DateLastModified
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status
        { get; set; }

    }


    public class Wishlist
    {
        [ScaffoldColumn(false)]
        public int WishlistID
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status
        { get; set; }
    }

    public class Supplier
    {
        [Key]
        [ScaffoldColumn(false)]
        public int SupplierID
        { get; set; }
        [Required]
        public string Name
        { get; set; }
        public string User_Id
        { get; set; }
        [Required]
        [Display(Name = "Contact Person Name")]
        public string ContactPerson
        { get; set; }
        [Required]
        [Display(Name = "Contact Person Last Name")]
        public string LastName
        { get; set; }
        [Required]
        [StringLength(10)]
        public string Fax
        { get; set; }
        [Display(Name = "Telephone")]
        [StringLength(10)]
        [Required]
        public string ContactPersonNumber
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status
        { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DataSocket", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }

       public DbSet<Supplier> Suppliers { get; set; }

       public System.Data.Entity.DbSet<urbanbooks.Models.Device_Advanced> Device_Advanced { get; set; }

       public System.Data.Entity.DbSet<urbanbooks.Models.Book_Advanced> Book_Advanced { get; set; }

       public System.Data.Entity.DbSet<urbanbooks.Models.RegisterSupplier> RegisterSuppliers { get; set; }

       public System.Data.Entity.DbSet<urbanbooks.Author> Authors { get; set; }

       public System.Data.Entity.DbSet<urbanbooks.Publisher> Publishers { get; set; }

       public System.Data.Entity.DbSet<urbanbooks.Manufacturer> Manufacturers { get; set; }

       public System.Data.Entity.DbSet<urbanbooks.TechCategory> TechCategories { get; set; }

       public System.Data.Entity.DbSet<urbanbooks.Order> Orders { get; set; }

    }
}

        
