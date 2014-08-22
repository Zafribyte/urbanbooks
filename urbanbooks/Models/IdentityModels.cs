using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

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

        public virtual Employee Employees { get; set; }
        public virtual Customer Customers { get; set; }
        public virtual Cart Carts { get; set; }
        public virtual Wishlist Wishlists { get; set; }
    }



    public class Customer
    {
        [ScaffoldColumn(false)]
        public int CustomerID
        { get; set; }
        [Display(Name = "Name")]
        public string FirstName
        { get; set; }
        [Display(Name = "Surname")]
        public string LastName
        { get; set; }
        [Display(Name = "Address")]
        public string PhysicalAddress
        { get; set; }
        [Display(Name = "Cell Phone")]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone
        { get; set; }
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string Telephone
        { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "email address is not valid.")]
        [Display(Name = "e-mail Address")]
        public string Email
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status
        { get; set; }
    }





    public class Employee
    {
        [ScaffoldColumn(false)]
        public int EmployeeID
        { get; set; }
        [Display(Name = "Name")]
        public string FirstName
        { get; set; }
        [Display(Name = "Surname")]
        public string LastName
        { get; set; }
        public string Address
        { get; set; }
        [Display(Name = "Cell Phone")]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone
        { get; set; }
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string Telephone
        { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "e-mail Address ")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "email address is not valid.")]
        public string Email
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status
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
        public int CustomerID
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status
        { get; set; }

        IEnumerable<CartItem> CartItems { get; set; }
    }


    public class Wishlist
    {
        [ScaffoldColumn(false)]
        public int WishlistID
        { get; set; }
        [ScaffoldColumn(false)]
        public int CustomerID
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

        public System.Data.Entity.DbSet<Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<urbanbooks.Book> Books { get; set; }

        public System.Data.Entity.DbSet<Cart> Carts { get; set; }

        public System.Data.Entity.DbSet<urbanbooks.Special> Specials { get; set; }

        public System.Data.Entity.DbSet<urbanbooks.Technology> Technologies { get; set; }

        public System.Data.Entity.DbSet<Wishlist> Wishlists { get; set; }

        public System.Data.Entity.DbSet<urbanbooks.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<urbanbooks.BookCategory> Categories { get; set; }

        public System.Data.Entity.DbSet<urbanbooks.Models.FullRegisterViewModel> FullRegisterViewModels { get; set; }

        public System.Data.Entity.DbSet<urbanbooks.Product> Products { get; set; }

        public System.Data.Entity.DbSet<urbanbooks.CartItem> CartItems { get; set; }

        public System.Data.Entity.DbSet<urbanbooks.WishlistItem> WishlistItems { get; set; }
    }
}