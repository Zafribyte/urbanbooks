using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class AuthorController : Controller
    {
        Author author;
        BusinessLogicHandler myHandler;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int authorID)
        {
            author = myHandler.GetAuthorDetails(authorID);
            return View(author);
        }

        [Authorize(Roles="admin, employee")]
        public ActionResult Create()
        {
                return View();
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                author = new Author();
                if (ModelState.IsValid)
                {
                    myHandler.AddAuthor(author);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "admin, employee")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                author = new Author();
                TryUpdateModel(author);
                if (ModelState.IsValid)
                {
                    myHandler.UpdateAuthor(author);
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
