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
            model.DeviceSales = new List<CategorySalesPie>();

            #endregion

            #region Get Book Data

            IEnumerable<InvoiceItem> soldItems = myHandler.Sales();
            IEnumerable<Book> books = myHandler.GetBooks();
            IEnumerable<BookCategory> categories = myHandler.GetBookCategoryList();

            #endregion

            #region Get Device Data
            IEnumerable<TechCategory> DeviceCategories = myHandler.GetTechnologyTypeList();
            IEnumerable<Technology> Devices = myHandler.GetTechnology();
            #endregion

            #region Build Book Data Set

            var dataSet = from soldT in soldItems
                          join book in books on soldT.ProductID equals book.ProductID
                          join category in categories on book.BookCategoryID equals category.BookCategoryID
                          select new { soldT.Price, soldT.Quantity, category.CategoryName };
            List<string> names = new List<string>();
            foreach (var item in dataSet)
            {
                if (names.Contains(item.CategoryName))
                {
                    CategorySalesPie pie = new CategorySalesPie();
                    pie.Category = item.CategoryName;
                    pie = model.BookSales.SingleOrDefault(m => m.Category == item.CategoryName);
                    int x = model.BookSales.IndexOf(pie);
                    if(x > -1)
                    {
                        model.BookSales[x].TotalSales += model.BookSales[x].TotalSales + (item.Price * item.Quantity);
                    }
                }
                else
                {
                    CategorySalesPie modelItem = new CategorySalesPie();
                    names.Add(item.CategoryName);
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


            #region Build Device Data Set

            var DeviceDataSet = from soldT in soldItems
                                join device in Devices on soldT.ProductID equals device.ProductID
                                join category in DeviceCategories on device.TechCategoryID equals category.TechCategoryID
                                select new { soldT.Price, soldT.Quantity, category.CategoryName };
            List<string> namesList = new List<string>();
            foreach (var item in DeviceDataSet)
            {
                if (namesList.Contains(item.CategoryName))
                {
                    CategorySalesPie pie = new CategorySalesPie();
                    pie.Category = item.CategoryName;
                    pie = model.DeviceSales.SingleOrDefault(m => m.Category == item.CategoryName);
                    int x = model.DeviceSales.IndexOf(pie);
                    if (x > -1)
                    {
                        model.DeviceSales[x].TotalSales += model.DeviceSales[x].TotalSales + (item.Price * item.Quantity);
                    }
                }
                else
                {
                    CategorySalesPie modelItem = new CategorySalesPie();
                    namesList.Add(item.CategoryName);
                    modelItem.Category = item.CategoryName;
                    modelItem.TotalSales = (item.Price * item.Quantity);
                    model.DeviceSales.Add(modelItem);
                }

            }

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
