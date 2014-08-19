using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace urbanbooks.Models
{
    public class CustomerContext: DbContext
    {
        public CustomerContext():base("DataSocket")
        { }

       public DbSet<Customer> Customers { get; set; }
    }
}