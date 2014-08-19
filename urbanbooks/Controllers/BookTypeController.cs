using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    [Authorize(Roles="admin, employee")]
    public class BookTypeController : Controller
    {
        BusinessLogicHandler myHandler;
        BookCategory typeOf;
        [AllowAnonymous]
        public ActionResult Index()
        {
            myHandler = new BusinessLogicHandler();
            List<BookCategory> typeList = myHandler.GetBookTypeList();
            return View(typeList);
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            myHandler = new BusinessLogicHandler();
            typeOf  = myHandler.GetBookTypeList().Single(typ => typ.BookCategoryID == id);
            return View(typeOf);
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
                    //myHandler.UpdateBookType(typeOf);
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

            typeOf = myHandler.GetBookTypeList().Single(tlist => tlist.BookCategoryID == id);

            return View(typeOf);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                typeOf = new BookCategory();
                TryUpdateModel(typeOf);
                if (ModelState.IsValid)
                {
                    myHandler.UpdateBookType(typeOf);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

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
