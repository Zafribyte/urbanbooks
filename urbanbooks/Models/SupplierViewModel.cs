using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Models
{
    public class SupplierViewModel
    {
        [Key]
        public int the_Id { get; set; }
        public RegisterSupplier RegisterNewSupplier { get; set; }
        [NotMapped]
        public List<SelectListItem> SupplierType { get; set; }
        [NotMapped]
        public IEnumerable<urbanbooks.Supplier> RegisteredSuppliers { get; set; }
        public Supplier supplier { get; set; }

    }
}