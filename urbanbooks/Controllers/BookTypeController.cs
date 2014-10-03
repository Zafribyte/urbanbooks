using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    //[Authorize(Roles="admin, employee")]
    public class BookTypeController : Controller
    {
        BusinessLogicHandler myHandler;
        BookCategory typeOf;
        [AllowAnonymous]
        public ActionResult Index()
        {
            myHandler = new BusinessLogicHandler();
            List<BookCategory> typeList = myHandler.GetBookCategoryList();
            return View(typeList);
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            myHandler = new BusinessLogicHandler();
            typeOf = myHandler.GetBookCategoryList().Single(typ => typ.BookCategoryID == id);
            return View(typeOf);
        }
        public ActionResult ViewBookCategory()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ViewBookCategory(BookCategory bookCategory)
        {
            myHandler = new BusinessLogicHandler();
            if (ModelState.IsValid)
            {
                myHandler.AddBookType(bookCategory);
            }
            return Json(new { success = true });
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                typeOf = new BookCategory();
                TryUpdateModel(typeOf);
                if (ModelState.IsValid)
                {
                    myHandler.AddBookType(typeOf);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            myHandler = new BusinessLogicHandler();
            typeOf = new BookCategory();

            typeOf = myHandler.GetBookCategoryList().Single(tlist => tlist.BookCategoryID == id);

            return View(typeOf);
        }

        [HttpPost]
        public ActionResult Edit(BookCategory bc)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                typeOf = new BookCategory();
                TryUpdateModel(bc);
                if (ModelState.IsValid)
                {
                    myHandler.UpdateBookType(bc);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int BookCategoryID)
        {
            myHandler = new BusinessLogicHandler();
            typeOf = new BookCategory();
            typeOf.BookCategoryID = BookCategoryID;
            typeOf = myHandler.GetBookCategory(BookCategoryID);
            return View(typeOf);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Delete(int BookCategoryID, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                typeOf = new BookCategory();
                typeOf.BookCategoryID = BookCategoryID;
                myHandler.DeleteBookType(BookCategoryID);

                TempData["Alert Message"] = "Device Successfully Deleted";
                return RedirectToAction("Index", "BookType");
            }
            catch
            {
                return View();
            }
        }
    }
}
