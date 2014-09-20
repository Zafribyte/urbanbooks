using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace urbanbooks.Models
{
    public class SupplierContext:DbContext
    {
        public SupplierContext():base("DataSocket")
        { }
        public DbSet<Supplier> Suppliers { get; set; }
        
    }
}