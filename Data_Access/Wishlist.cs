using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Wishlist
    {
        [ScaffoldColumn(false)]
        public int WishlistID 
        { get; set; }
        [ScaffoldColumn(false)]
        public int CustomerID 
        { get; set; }
        public DateTime DateModified
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status 
        { get; set; }
    }
}
