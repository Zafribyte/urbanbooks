using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace urbanbooks.Models
{
    public class WishlistActions
    {
        BusinessLogicHandler myHandler;
        public async Task<IEnumerable<WishlistItem>> GetWishlistItems(int wishlistID)
        {
            myHandler = new BusinessLogicHandler();
            IEnumerable<WishlistItem> wishes = (IEnumerable<WishlistItem>)myHandler.GetWishlistItems(wishlistID);
            return wishes;
        }

        public async Task<int> GetWishlistTotal(int wishlistID)
        {
            IEnumerable<WishlistItem> wishdata = await GetWishlistItems(wishlistID);
            if (wishdata != null)
            {
                var total = from tot in wishdata
                            select (tot.ProductID);
                return (int)total.Count();
            }
            return 0;
        }

        public async Task<bool> InsertWishlistItem(WishlistItem wish)
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