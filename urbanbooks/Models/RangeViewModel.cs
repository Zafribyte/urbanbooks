using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class RangeViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public RangeValidate myRange { get; set; }
        public List<Monthly> MonthlySales { get; set; }
        public string radioButtons { get; set; }
    }
    public class RangeValidate
    {
        [Key]
        public int TheKey { get; set; }
        [Required]
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
    public class Monthly 
    {
        [Key]
        public int key { get; set; }

        public string Month { get; set; }
        public double TotalSales { get; set; }
    }

}