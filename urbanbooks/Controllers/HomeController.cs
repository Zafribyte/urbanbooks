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
                #region Check iDentity
                if (HttpContext.User.IsInRole("admin"))
                {
                   return  RedirectToAction("Index", "Admin");
                }
                else if (HttpContext.User.IsInRole("supplier"))
                {
                  return  RedirectToAction("Index", "Supplier");
                }
                else if (HttpContext.User.IsInRole("employee"))
                {
                  return  RedirectToAction("Index", "Employee");
                }
                #endregion


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
            SearchViewModel modelHelper = new SearchViewModel();
            #endregion

            #region execute search


            if(criteria == " books")
            {
                modelHelper.BookResults = (IEnumerable<Book>)findBook.Where(m => m.BookTitle.StartsWith(query));//contains
            }
            else if (criteria == " gadget")
            {
                modelHelper.GadgetResults = findGadget.Where(m => m.Specs.StartsWith(query)).ToList();//solve
            }
            else if (criteria == " bookTitle")
            {
                modelHelper.BookResults = (IEnumerable<Book>)findBook.Where(m => m.BookTitle.StartsWith(query));
            }
            else if (criteria == " bookCategory")
            {
                modelHelper.BookResults = (IEnumerable<Book>)findCategory.Where(m => m.CategoryName.StartsWith(query));
            }
            else if (criteria == " bookIsbn")
            {
                modelHelper.BookResults = (IEnumerable<Book>)findBook.Where(m => m.ISBN.StartsWith(query));
            }
            else if (criteria == " gadgetModel")
            {
                modelHelper.GadgetResults = findGadget.Where(m => m.ModelName.StartsWith(query)).ToList();
            }
            else if (criteria == " gadgetModelNumber")
            {
                modelHelper.GadgetResults = findGadget.Where(m => m.ModelNumber.StartsWith(query)).ToList();
            }
            else
            { }

            modelHelper.Query = query;
            #endregion

            return View(modelHelper);
        }
    }
}