using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using urbanbooks.Models;

namespace urbanbooks.Controllers
{
    public class BookController : Controller
    {
        BusinessLogicHandler myHandler;
        Book book;


        [Authorize(Roles="admin, employee")]
        public ActionResult ManageBooks()
        { return View(); }
        public ActionResult Index()
        {
            myHandler = new BusinessLogicHandler();
            List<Book> myBookList = new List<Book>();
            myBookList = myHandler.GetBooks();
            IEnumerable<BookCategory> myType = myHandler.GetBookTypeList();
            ViewBag.BookTypeBag = myType;
            return View(myBookList);
        }

        public ActionResult Details(int bookId)
        {
            myHandler = new BusinessLogicHandler();
            Book book = myHandler.GetBooks().Single(bk => bk.ProductID == bookId);
            return View(book);
        }

        [Authorize(Roles = "admin, employee")]
        public ActionResult Create()
        {
            SupplierHandler supHandler = new SupplierHandler();
            IEnumerable<Supplier> nameList = (IEnumerable<Supplier>)supHandler.GetSupplierList();
            var disp = from nameAndId in nameList
                       select new { Value = nameAndId.SupplierID, Text = nameAndId.Name };

            ViewBag.SupplierList = new SelectList(disp.ToList());

            BookCategoryHandler typeHandler = new BookCategoryHandler();
            List<BookCategory> typeList = (List<BookCategory>)typeHandler.GetBookCategoryList();
            AddNewBookViewModel bookM = new AddNewBookViewModel();
            bookM.bookTypes = typeList;

            List<SelectListItem> myItem = new List<SelectListItem>();
            myItem.Add(new SelectListItem { Text = "Select Type", Value = "", Selected = true });
            foreach(var item in typeList)
            {
                myItem.Add(new SelectListItem { Text = item.CategoryDescription, Value = item.BookCategoryID.ToString() });
            }
            bookM.booktype = new List<SelectListItem>();
            bookM.booktype = myItem;

            ViewData["Booooooo"] = myItem;



            List<SelectListItem> supplier = new List<SelectListItem>();
            supplier.Add(new SelectListItem { Text = "Select Supplier", Value = "", Selected = true });
            foreach (var item in nameList)
            {
                supplier.Add(new SelectListItem { Text = item.Name, Value = item.SupplierID.ToString() });
            }
            bookM.suppliers = new List<SelectListItem>();
            bookM.suppliers = supplier;
            ViewData["suppliers"] = supplier;



            return View(bookM);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index","Home" , null);
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin, employee")]
        public ActionResult Edit(int bookId)
        {
            myHandler = new BusinessLogicHandler();
            Book book = myHandler.GetBooks().Single(bk => bk.ProductID == bookId);
            return View(book);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                book = new Book();
                TryUpdateModel(book);
                if(ModelState.IsValid)
                {
                    myHandler.UpdateBook(book);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin, employee")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
