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
        public ActionResult Index()
        {
            #region Cart and wishlist actions
            if (User.Identity.IsAuthenticated)
            {
                #region Check iDentity
                if (HttpContext.User.IsInRole("admin"))
                {
                   return  RedirectToAction("Index", "Admin");
                }
                else if (HttpContext.User.IsInRole("supplier"))
                {
                  return  RedirectToAction("Home", "Supplier");
                }
                else if (HttpContext.User.IsInRole("employee"))
                {
                  return  RedirectToAction("Index", "Employee");
                }
                #endregion

                #region Getting cart total
                CartActions act = new CartActions();
                WishlistActions wish = new WishlistActions();
                ApplicationDbContext mycontext = new ApplicationDbContext();
                UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(mycontext);
                ApplicationUserManager mgr = new ApplicationUserManager(myStore);
                var thisUser = mgr.FindByNameAsync(User.Identity.Name);
                int cartId = Convert.ToInt32(thisUser.Result.Carts.CartID);
                //
                try
                {
                    double nm = GetCartTotal(cartId); string[] xn = nm.ToString().Split('.'); Session["cartTotal"] = xn[0] + "," + xn[1];
                }
                catch
                { Session["cartTotal"] = (double)GetCartTotal(cartId); }
                Session["wishlistTotal"] = wish.GetWishlistTotal(thisUser.Result.Wishlists.WishlistID);
                #endregion
            }
            else
            { Session["cartTotal"] = "0,00"; Session["wishlistTotal"] = 0; }
            #endregion

            #region Prep utilities
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel model = new SearchViewModel();
            #endregion

            #region Get New Books
            model.BookResults = myHandler.GetNewBooks();
            #endregion

            #region Get Devices
            model.GadgetResults = myHandler.GetNewDevices();
            #endregion

            return View(model);
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