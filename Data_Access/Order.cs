using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Order
    {
        [ScaffoldColumn(true)]
        [Key] public int OrderNo 
        { get; set; }
        public DateTime DateCreated 
        { get; set; }
        public DateTime DateLastModified 
        { get; set; }
        public bool Status 
        { get; set; }
        public DateTime DateSent 
        { get; set; }
        [ScaffoldColumn(false)]
        public int SupplierID 
        { get; set; }
        [ScaffoldColumn(false)]
        public int EmployeeID 
        { get; set; }

        public DateTime DataModified { get; set; }
    }
}
