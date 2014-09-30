using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Services;
using urbanbooks.Models;
using System.Web.Services;
using Newtonsoft.Json;

namespace urbanbooks.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {

        public ActionResult Index()
        {


            #region Prep Utilities

            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            AdminIndexViewModel model = new AdminIndexViewModel();
            model.BookSales = new List<CategorySalesPie>();
            CategorySalesPie modelItem = new CategorySalesPie();

            #endregion

            #region Get Data

            IEnumerable<InvoiceItem> soldItems = myHandler.Sales();
            IEnumerable<Book> books = myHandler.GetBooks();
            IEnumerable<BookCategory> categories = myHandler.GetBookCategoryList();

            #endregion

            #region Build Data Set

            var dataSet = from soldT in soldItems
                          join book in books on soldT.ProductID equals book.ProductID
                          join category in categories on book.BookCategoryID equals category.BookCategoryID
                          select new { soldT.Price, soldT.Quantity, category.CategoryName };
            string[] names = new string[dataSet.Count()];
            foreach (var item in dataSet)
            {
                if (names.Contains(item.CategoryName))
                {

                }
                else
                {
                    string[] items = new string[] { item.CategoryName};
                    names.Concat(items);
                    modelItem.Category = item.CategoryName;
                    modelItem.TotalSales = (item.Price * item.Quantity);
                    model.BookSales.Add(modelItem);
                }
                
            }


            #region Trial

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
                new System.Web.Script.Serialization.JavaScriptSerializer();

            #endregion

            //model.chartData = oSerializer.Serialize(model.BookSales.ToArray());
            //model.chartData =  JsonConvert.SerializeObject(model.BookSales.ToArray());
            //model.chartData = JsonConvert.SerializeObject(model.oData);
            #endregion

            return View(model);

        }

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
            return View();
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
