using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class ProductViewModel
    {
        public IEnumerable<CartItem> allCartItem { get; set; }
        public IEnumerable<Book> allBook { get; set; }
        public IEnumerable<Technology> allTechnology { get; set; }
        public IEnumerable<WishlistItem> allWishlistItems { get; set; }
    }
}