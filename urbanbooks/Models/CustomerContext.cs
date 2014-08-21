using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity;

namespace urbanbooks.Models
{
    public class CustomerContext: DbContext
    {
        public CustomerContext():base("DataSocket")
        { }

       public DbSet<Customer> Customers { get; set; }

       public override int SaveChanges()
       {
           try {
               return base.SaveChanges();
           }
           catch(System.Data.Entity.Validation.DbEntityValidationException e) {
               Exception except = e;
               foreach (var item in e.EntityValidationErrors)
               {
                   foreach(var inner in item.ValidationErrors)
                   {
                       //string msg = string.Format("{0}:{1}", item.Entry.Entity.ToString(), inner.ErrorMessage);

                       //except = new InvalidOperationException(message, e);
                       Trace.TraceInformation("Property: {0} Error: {1}", inner.PropertyName, inner.ErrorMessage);
                   }
               }
               throw except;
           }
       }
    }
}