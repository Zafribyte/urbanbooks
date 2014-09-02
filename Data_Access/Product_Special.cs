using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    
    public class Product_Special
    {
        [Key]
        public int ProductID { get; set; }
        public int SpecialID { get; set; }
    }
}
