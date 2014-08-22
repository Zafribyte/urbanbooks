using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class WishlistActions
    {
        BusinessLogicHandler myHandler;
        public IEnumerable<WishlistItem> GetWishlistItems(int wishlistID)
        {
            myHandler = new BusinessLogicHandler();
            IEnumerable<WishlistItem> wishes = myHandler.GetWishlistItems(wishlistID);
            return wishes;
        }

        public int GetWishlistTotal(int wishlistID)
        {
            IEnumerable<WishlistItem> wishdata = GetWishlistItems(wishlistID);
            if (wishdata != null)
            {
                var total = from tot in wishdata
                            select (tot.ProductID);
                return (int)total.Count();
            }
            return 0;
        }

        public bool InsertWishlistItem(WishlistItem wish)
        {
            myHandler = new BusinessLogicHandler();
            if (myHandler.AddWishlistItem(wish))
            {
                return true;
            }
            else
                return false;
        }
    }
}