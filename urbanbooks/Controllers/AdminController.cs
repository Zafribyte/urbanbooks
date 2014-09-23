using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Services;
using urbanbooks.Models;
using System.Web.Services;

namespace urbanbooks.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public ActionResult BookSales()
        {

            #region Prep Utilities

            BusinessLogicHandler myHandler = new BusinessLogicHandler();

            #endregion

            #region Get Data

            IEnumerable<InvoiceItem> soldItems = myHandler.Sales();
            IEnumerable<Book> books = myHandler.GetBooks();
            IEnumerable<BookCategory> categories = myHandler.GetBookCategoryList();
            AdminIndexViewModel model = new AdminIndexViewModel();
            model.BookSales = new List<CategorySalesPie>();
            CategorySalesPie modelItem = new CategorySalesPie();

            #endregion

            #region Build Data Set

            var dataSet = from soldT in soldItems
                          join book in books on soldT.ProductID equals book.ProductID
                          join category in categories on book.BookCategoryID equals category.BookCategoryID
                          select new {soldT.Price, soldT.Quantity, category.CategoryName };

            foreach(var item in dataSet)
            {
                modelItem.Category = item.CategoryName;
                modelItem.TotalSales = (item.Price * item.Quantity);
                model.BookSales.Add(modelItem);
            }

            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
