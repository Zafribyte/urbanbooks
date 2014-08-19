using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Cart
    {
        [ScaffoldColumn(false)]
        public int CartID 
        { get; set; }
        public DateTime DateLastModified 
        { get; set; }
        [ScaffoldColumn(false)]
        public int CustomerID 
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status 
        { get; set; }

        IEnumerable<CartItem> CartItems { get; set; }
    }
}
