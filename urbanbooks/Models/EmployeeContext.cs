using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace urbanbooks.Models
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext()
            : base("DataSocket")
        { }

        DbSet<Employee> Employees { get; set; }
    }
}