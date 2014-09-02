using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    
    public class Product_Keyword 
    {
        [Key]
        public int ProductID { get; set; }
        public int KeywordID { get; set; }

        public List<Keyword> KeywordList { get; set; }
        public List<Product> ProductList { get; set; }
    }
}
