using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class AdminIndexViewModel
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public List<CategorySalesPie> BookSales { get; set; }
        [NotMapped]
        public List<CategorySalesPie> DeviceSales { get; set; }

    }
    public class CategorySalesPie
    {
        [Key]
        public int Id { get; set; }
        
        public string Category { get; set; }
        [Display(Name = "Total Sales")]
        public double TotalSales { get; set; }
    }
}