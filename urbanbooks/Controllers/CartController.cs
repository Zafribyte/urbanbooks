using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using urbanbooks.Models;
using System.Web.ModelBinding;
using urbanbooks.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace urbanbooks.Controllers
{
    public class CartController : Controller
    {
        BusinessLogicHandler myHandler;
        public async Task< ActionResult> Edit()
        {
            CartActions act = new CartActions(); WishlistActions wishAct = new WishlistActions();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            userMgr = new ApplicationUserManager(myStore); 
            var thisUser = await userMgr.FindByNameAsync(User.Identity.Name);
            int Id = (int)thisUser.Carts.CartID;
            Session["cartTotal"] = await act.GetTotalAsync(Id);
            Session["wishlistTotal"] = await wishAct.GetWishlistTotal(thisUser.Wishlists.WishlistID);
            IEnumerable<CartItem> myItems = await act.GetCartItemsAsync(Id);
            myHandler = new BusinessLogicHandler();
            IEnumerable<Book> ifBooks = myHandler.GetBooks();
            IEnumerable<Technology> ifGadget = myHandler.GetTechnology();
            ProductViewModel myNewModel = new ProductViewModel();
            myNewModel.allBook = ifBooks;
            myNewModel.allCartItem = myItems;
            myNewModel.allTechnology = ifGadget;
            return View(myNewModel);
        }

        [HttpPost]
        public ActionResult Edit(int CartItemID)
        {
            CartItem item = new CartItem();
            Cart cart = new Cart();
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            try
            {
                TryUpdateModel(cart);
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    //myHandler.UpdateCart(cart);
                    myHandler.UpdateCartItem(item);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        ApplicationUserManager userMgr;
        
        public async Task<ActionResult> AddToCart(int ProductID)
        {
            string userName = User.Identity.GetUserName();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            userMgr = new ApplicationUserManager(myStore);
            var user = await userMgr.FindByEmailAsync(userName);
            int customer = (int)user.Customers.CustomerID;
            CartActions myActions = new CartActions();
            Cart cart = new Cart();
            cart.CartID = user.Carts.CartID;
            if (await myActions.AddToCartAsync(cart.CartID, ProductID))
            {Session["cartTotal"] = await GetCartTotal(cart.CartID); }
            else
            {  }
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index", "Home");
        }

        public async Task<double> GetCartTotal(int CartID)
        {
            double total;
            CartActions myA = new CartActions();
            total = await myA.GetTotalAsync(CartID);
            return total;
        }

        public ActionResult Delete(int CartItemID)
        {
            myHandler = new BusinessLogicHandler();
            myHandler.DeleteCartItem(CartItemID);

            return RedirectToAction("Edit"); 
        }
       
    }
}
