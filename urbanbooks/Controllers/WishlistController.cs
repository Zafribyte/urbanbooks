using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using urbanbooks.Models;

namespace urbanbooks.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        BusinessLogicHandler myHandler;
        Wishlist list;
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            myHandler = new BusinessLogicHandler();
            list = new Wishlist();
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext mycontext = new ApplicationDbContext();
                UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(mycontext);
                ApplicationUserManager mgr = new ApplicationUserManager(myStore);
                var user = await mgr.FindByNameAsync(User.Identity.Name);

                WishlistActions grantMyWish = new WishlistActions();
                CartActions cart = new CartActions();
                ProductViewModel bridge = new ProductViewModel();
                myHandler = new BusinessLogicHandler();

                bridge.allWishlistItems =  grantMyWish.GetWishlistItems(user.Wishlists.WishlistID);
                {
                    bridge.allBook = (IEnumerable<Book>)myHandler.GetBooks();
                    bridge.allTechnology = (IEnumerable<Technology>)myHandler.GetTechnology();
                }

                Session["wishlistTotal"] =  grantMyWish.GetWishlistTotal(user.Wishlists.WishlistID);
                Session["cartTotal"] = cart.GetTotalAsync(user.Carts.CartID);
                return View(bridge);
            }
            else
                return RedirectToAction("Login", "Account", null);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                list = new Wishlist();
                TryUpdateModel(list);
                if (ModelState.IsValid)
                {
                    //myHandler.UpdateWishlist(list);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Add(int productId)
        {
            WishlistActions act = new WishlistActions();
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext mycontext = new ApplicationDbContext();
                UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(mycontext);
                ApplicationUserManager mgr = new ApplicationUserManager(myStore);
                var thisUser = await mgr.FindByNameAsync(User.Identity.Name);

                WishlistItem wish = new WishlistItem();
                CartActions cart = new CartActions();
                wish.ProductID = productId;

                myHandler = new BusinessLogicHandler();
                IEnumerable<CartItem> inItems = myHandler.GetCartItems(thisUser.Carts.CartID);
                if (inItems != null)
                {
                    CartItem items = inItems.SingleOrDefault(item => item.ProductID == productId);
                    if (items != null)
                    {
                        if (items.ProductID == productId)
                        { myHandler = new BusinessLogicHandler(); myHandler.DeleteCartItem(items.CartItemID); }
                    }

                }
                wish.WishlistID = (int)thisUser.Wishlists.WishlistID;
                wish.DateAdded = DateTime.Now;
                myHandler.AddWishlistItem(wish);

                Session["wishlistTotal"] =  act.GetWishlistTotal(thisUser.Wishlists.WishlistID);
                Session["cartTotal"] =(double) cart.GetTotalAsync(thisUser.Carts.CartID);

                return RedirectToAction("Index", "Home", null);
            }
            else
            { return RedirectToAction("Account", "Login", null); }
        }

        public ActionResult Delete(int WishlistItemID)
        {
            myHandler = new BusinessLogicHandler();
            myHandler.DeleteWishlistItem(WishlistItemID);
            return RedirectToAction("Edit");
        }
    }
}
