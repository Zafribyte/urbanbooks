using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace urbanbooks.Models
{
    public class CartActions
    {
        BusinessLogicHandler myHandler;
        Cart cart;
        CartItem item;
        //public async Task<Cart> GetCartAsync(int customerId)
        //{ myHandler = new BusinessLogicHandler(); return myHandler.GetCart(customerId); }

        public async Task<bool> AddToCartAsync(int cartId, int ProductID)
        {
            myHandler = new BusinessLogicHandler();
            item = new CartItem();
            item.CartID = cartId;
            item.ProductID = ProductID;
            item.DateAdded = DateTime.Now;
            item.Quantity = 1;
            if (myHandler.AddCartItem(item))
            { return true; }
            else
                return false;
            
        }

        //public async Task<bool> UpdateCartAsync(int cartId)
        //{
        //    myHandler = new BusinessLogicHandler();
        //    cart = new Cart();
        //    cart.DateLastModified = DateTime.Now;
        //    return tru

        //}

        public async Task<double> GetTotalAsync(int cartId)
        {
            
            myHandler = new BusinessLogicHandler();
            IEnumerable<Book> myBooks = myHandler.GetBooks();
            IEnumerable<Technology> myGadget = myHandler.GetTechnology();
            IEnumerable<CartItem> myItems;
            double final = 0;
            myItems = await GetCartItemsAsync(cartId);
            if (myItems != null)
            {
                if (myBooks != null || myGadget !=null)
                {
                    var totalBook = from item in myItems
                                join bi in myBooks on item.ProductID equals bi.ProductID
                                where item.ProductID == bi.ProductID
                                select (item.Quantity * bi.SellingPrice);
                    final = (double)totalBook.Sum();
                    var total = from item in myItems
                                join gadget in myGadget on item.ProductID equals gadget.ProductID
                                where gadget.ProductID == item.ProductID
                                select (item.Quantity * gadget.SellingPrice);
                    final += (double)total.Sum();
                }
                return final;
            }
            else
                return 0.00;
           
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId)
        {
            myHandler = new BusinessLogicHandler();
            return myHandler.GetCartItems(cartId);
        }

        public async Task<bool> UpdateCartItem(CartItem item)
        {
            myHandler = new BusinessLogicHandler();
            return myHandler.UpdateCartItem(item);
        }

    }
}