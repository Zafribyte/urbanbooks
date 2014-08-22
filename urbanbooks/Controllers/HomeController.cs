using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using urbanbooks.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using urbanbooks.Models;

namespace urbanbooks.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                CartActions act = new CartActions();
                WishlistActions wish = new WishlistActions();
                ApplicationDbContext mycontext = new ApplicationDbContext();
                UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(mycontext);
                ApplicationUserManager mgr = new ApplicationUserManager(myStore);
                var thisUser = mgr.FindByNameAsync(User.Identity.Name);
                int cartId = (int)thisUser.Result.Carts.CartID;
                Session["cartTotal"] = (double) GetCartTotal(cartId);
                Session["wishlistTotal"] = wish.GetWishlistTotal(thisUser.Result.Wishlists.WishlistID);
            }
            else
            { Session["cartTotal"] = "0.00"; Session["wishlistTotal"] = 0; }

            return View();
        }

        public double GetCartTotal(int CartID)
        {
            double total;
            CartActions myA = new CartActions();
            total = myA.GetTotalAsync(CartID);
            return total;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}