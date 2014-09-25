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
    }
    public class RangeValidate
    {
        [Key]
        public int TheKey { get; set; }
        [Required]
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}