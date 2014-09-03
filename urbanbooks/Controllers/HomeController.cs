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

        [HttpPost]
        public ActionResult Search(FormCollection collect)
        {
            #region Get Search Term
            string query = collect.GetValue("query").AttemptedValue;
            string criteria = collect.AllKeys[1];
            #endregion

            #region init search
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            IEnumerable<Book> findBook = myHandler.GetBooks();
            IEnumerable<Technology> findGadget = myHandler.GetTechnology();
            IEnumerable<BookCategory> findCategory = myHandler.GetBookCategoryList();
            IEnumerable<TechCategory> findGadgetCategory = myHandler.GetTechnologyTypeList();
            Book book;
            TechCategory gadgetCategory;
            Technology gadget;
            BookCategory bookCategory;
            SearchViewModel modelHelper = new SearchViewModel();
            #endregion

            #region execute search


            if(criteria == "books")
            {
                modelHelper.BookResults = (IEnumerable<Book>)findBook.Where(m => m.BookTitle == query);//contains
            }
            else if (criteria == "gadget")
            {
                modelHelper.GadgetResults = findGadget.Where(m => m.Specs).Contains(query);//solve
            }
            else if (criteria == " bookTitle")
            {
                modelHelper.BookResults = (IEnumerable<Book>)findBook.Where(m => m.BookTitle == query);
            }
            else if (criteria == "bookCategory")
            { 
            }
            else if (criteria == "bookIsbn")
            {

            }
            else if (criteria == "gadgetModel")
            {

            }
            else if (criteria == "gadgetModelNumber")
            {

            }
            else
            { }


            #endregion

            return View();
        }
    }
}