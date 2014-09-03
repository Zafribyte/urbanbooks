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
            if (User.Identity.IsAuthenticated)
            {
                CartActions act = new CartActions();
                WishlistActions wish = new WishlistActions();
                ApplicationDbContext mycontext = new ApplicationDbContext();
                UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(mycontext);
                ApplicationUserManager mgr = new ApplicationUserManager(myStore);
                var thisUser = mgr.FindByNameAsync(User.Identity.Name);
                int cartId = Convert.ToInt32(thisUser.Result.Carts.CartID);
                try
                {
                    Session["cartTotal"] = (double)GetCartTotal(cartId);
                }
                catch
                { Session["cartTotal"] = 0.00; }
                try
                {
                    Session["wishlistTotal"] = wish.GetWishlistTotal(thisUser.Result.Wishlists.WishlistID);
                }
                catch
                { Session["wishlistTotal"] = 0; }
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

        public ActionResult GlobalSearch(string search)
        {
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            IEnumerable<Book> books = myHandler.GetBooks();
            List<string> complete = books.Where(book => book.BookTitle.StartsWith(search)).Select(title => title.BookTitle).ToList();
            return Json(complete, JsonRequestBehavior.AllowGet);
        }
    }
}