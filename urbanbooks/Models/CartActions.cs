using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Expressions.Internal;
using System.Web;

namespace urbanbooks.Models
{
    public class CartActions
    {
        BusinessLogicHandler myHandler;
        CartItem item;

        public bool AddToCartAsync(int cartId, int ProductID)
        {
            myHandler = new BusinessLogicHandler();
            item = new CartItem();
            item = myHandler.CheckIfExist(cartId, ProductID);
            if(item == null)
            {
                item.CartID = cartId;
                item.ProductID = ProductID;
                item.DateAdded = DateTime.Now;
                item.Quantity = 1;
                if (myHandler.AddCartItem(item))
                { return true; }
                else
                    return false;
            }
            else
            {
                item.Quantity += 1;
                if (myHandler.UpdateCartItem(item))
                { return true; }
                else
                    return false;
            }
        }

        public double GetTotalAsync(int cartId)
        {
            
            myHandler = new BusinessLogicHandler();
            IEnumerable<Book> myBooks = myHandler.GetBooks();
            IEnumerable<Technology> myGadget = myHandler.GetTechnology();
            IEnumerable<CartItem> myItems;
            double final = 0;
            myItems = GetCartItemsAsync(cartId);
            if (myItems != null)
            {
                if (myBooks != null )
                {
                    var totalBook = from item in myItems
                                join bi in myBooks on item.ProductID equals bi.ProductID
                                where item.ProductID == bi.ProductID
                                select (item.Quantity * bi.SellingPrice);
                    final = (double)totalBook.Sum();
                    if (myGadget != null)
                    {
                        var total = from item in myItems
                                    join gadget in myGadget on item.ProductID equals gadget.ProductID
                                    where gadget.ProductID == item.ProductID
                                    select (item.Quantity * gadget.SellingPrice);
                        final += (double)total.Sum();
                    }
                }
                return Math.Round(final, 2);
            }
            else
                return 0.00;
           
        }

        public IEnumerable<CartItem> GetCartItemsAsync(int cartId)
        {
            myHandler = new BusinessLogicHandler();
            return (IEnumerable<CartItem>) myHandler.GetCartItems(cartId);
        }
    }
}