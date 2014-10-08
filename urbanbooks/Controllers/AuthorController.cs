using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using urbanbooks.Models;

namespace urbanbooks.Controllers
{
    public class AuthorController : Controller
    {
        Author author;
        BusinessLogicHandler myHandler;
        public ActionResult Index()
        {
            myHandler = new BusinessLogicHandler();
            IEnumerable<Author> authorList = myHandler.GetAuthors();
            
            return View(authorList);
        }
        public ActionResult CheckDuplicates(string name, string surname)
        {
            List<Author> myList = new List<Author>();
            myHandler = new BusinessLogicHandler();
            myList = myHandler.CheckDuplicatedAuthor(name, surname);
            var isDuplicate = false;

            foreach (var item in myList)
            {
                string authorName = item.Name;
                string authorSurname = item.Surname;
                if (name.ToUpper() == authorName.ToUpper() && surname.ToUpper() == authorSurname.ToUpper())
                {
                    isDuplicate = true;
                }
            }
            var jsonData = new { isDuplicate };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(Author author)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
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
        public ActionResult ViewAuthor()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ViewAuthor(Author author)
        {
            myHandler = new BusinessLogicHandler();
            if (ModelState.IsValid)
            {
                myHandler.AddAuthor(author);
            }
            return Json(new { success = true });
        }




        [Authorize(Roles = "admin, employee")]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult New(Author author)
        {
            

            try
            {
                myHandler = new BusinessLogicHandler();
                if (ModelState.IsValid)
                {
                    myHandler.AddAuthor(author);
                }
                return RedirectToAction("Create", "Book", null);
            }
            catch
            {
                return View();
            }
        }




        public ActionResult Books(int AuthorID)
        {
            #region Prep Utilities
            
            myHandler = new BusinessLogicHandler();
            AddNewBookViewModel model = new AddNewBookViewModel();

            #endregion

            #region Get Books

            model.bookList = new List<Book>();
            model.bookList = myHandler.GetBooksByAuthor(AuthorID);

            #endregion

            #region Get Author Data

            model.author = new Author();
            model.author = myHandler.GetAuthorDetails(AuthorID);

            #endregion

            return View(model);
        }


        [Authorize(Roles = "admin, employee")]
        public ActionResult Edit(int AuthorID)
        {
            myHandler = new BusinessLogicHandler();
            author = new Author();
            author.AuthorID = AuthorID;
            author = myHandler.GetAuthorDetails(AuthorID);
            return View(author);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Edit(int AuthorID, FormCollection collection)
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
        public ActionResult Delete(int AuthorID)
        {
            myHandler = new BusinessLogicHandler();
            author = new Author();
            author.AuthorID = AuthorID;
            author = myHandler.GetAuthorDetails(AuthorID);
            return View(author);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Delete(int AuthorID, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                author = new Author();
                author.AuthorID = AuthorID;
                myHandler.DeleteAuthor(AuthorID);

                TempData["Alert Message"] = "Device Successfully Deleted";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
