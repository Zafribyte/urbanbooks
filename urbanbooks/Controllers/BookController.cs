using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using urbanbooks.Models;
using System.Drawing.Imaging;

namespace urbanbooks.Controllers
{
    public class BookController : Controller
    {
        BusinessLogicHandler myHandler;
        Book book;


        public ActionResult ManageBooks()
        { return View(); }
        public ActionResult Index()
        {
            myHandler = new BusinessLogicHandler();
            List<Book> myBookList = new List<Book>();
            myBookList = myHandler.GetBooks();
            IEnumerable<BookCategory> myType = myHandler.GetBookCategoryList();
            ViewBag.BookTypeBag = myType;
            return View(myBookList);
        }
        public ActionResult Details(int bookId)
        {
            myHandler = new BusinessLogicHandler();
            Book book = myHandler.GetBook(bookId);
            return View(book);
        }
        public ActionResult CreateBook()
        {
            #region Create
            AddNewBookViewModel bookM = new AddNewBookViewModel();
            SupplierHandler supHandler = new SupplierHandler();
            IEnumerable<Supplier> nameList = (IEnumerable<Supplier>)supHandler.GetSupplierList();
            var disp = from nameAndId in nameList
                       select new { Value = nameAndId.SupplierID, Text = nameAndId.Name };

            ViewBag.SupplierList = new SelectList(disp.ToList());

            BookCategoryHandler typeHandler = new BookCategoryHandler();
            IEnumerable<BookCategory> typeList = (IEnumerable<BookCategory>)typeHandler.GetBookCategoryList();
            var dispBC = from name in typeList
                         select new { Value = name.BookCategoryID, Text = name.CategoryName };

            ViewBag.BookCategoryList = new SelectList(dispBC.ToList());

            AuthorHandler authHandler = new AuthorHandler();
            IEnumerable<Author> authList = (IEnumerable<Author>)authHandler.GetAuthorList();
            var dispAuth = from nameAndSurname in authList
                           select new { Value = nameAndSurname.AuthorID, Text = nameAndSurname.Name, nameAndSurname.Surname };
            ViewBag.authList = new SelectList(dispAuth.ToList());

            PublisherHandler publHandler = new PublisherHandler();
            IEnumerable<Publisher> pubList = (IEnumerable<Publisher>)publHandler.GetPublisherList();
            var dispPublisher = from pubName in pubList
                                select new { Value = pubName.PublisherID, Text = pubName.Name };
            ViewBag.pubList = new SelectList(dispPublisher.ToList());

            #endregion 

            #region Display
            List<SelectListItem> bookCategory = new List<SelectListItem>();
            bookCategory.Add(new SelectListItem { Text = "Select Category", Value = "", Selected = true });
            foreach (var item in typeList)
            {
                bookCategory.Add(new SelectListItem { Text = item.CategoryName, Value = item.BookCategoryID.ToString() });
            }
            bookM.bookCategories = new List<SelectListItem>();
            bookM.bookCategories = bookCategory;
            ViewData["bookCategories"] = bookCategory;

            List<SelectListItem> supplier = new List<SelectListItem>();
            supplier.Add(new SelectListItem { Text = "Select Supplier", Value = "", Selected = true });
            foreach (var item in nameList)
            {
                supplier.Add(new SelectListItem { Text = item.Name, Value = item.SupplierID.ToString() });
            }
            bookM.suppliers = new List<SelectListItem>();
            bookM.suppliers = supplier;
            ViewData["suppliers"] = supplier;

            List<SelectListItem> author = new List<SelectListItem>();
            author.Add(new SelectListItem { Text = "Select Author", Value = "", Selected = true });
            foreach (var item in authList)
            {
                author.Add(new SelectListItem { Text = item.Name, Value = item.AuthorID.ToString() });
            }
            bookM.authors = new List<SelectListItem>();
            bookM.authors = author;
            ViewData["authors"] = author;

            List<SelectListItem> publisher = new List<SelectListItem>();
            publisher.Add(new SelectListItem { Text = "Select Publisher", Value = "", Selected = true });
            foreach (var item in pubList)
            {
                publisher.Add(new SelectListItem { Text = item.Name, Value = item.PublisherID.ToString() });
            }
            bookM.publishers = new List<SelectListItem>();
            bookM.publishers = publisher;
            ViewData["publishers"] = publisher;
#endregion 
            return View(bookM);
        }
        [HttpPost]
        public ActionResult CreateBook(FormCollection collection, HttpPostedFileBase file)
        {
            
            try
            {
                myHandler = new BusinessLogicHandler();
                book = new Book();
                book.BookTitle = collection.GetValue("books.BookTitle").AttemptedValue.ToString();
                book.Synopsis = collection.GetValue("books.Synopsis").AttemptedValue.ToString();
                book.ISBN = collection.GetValue("books.ISBN").AttemptedValue.ToString();
                book.BookCategoryID = Convert.ToInt32(collection.GetValue("CategoryName").AttemptedValue);
                book.PublisherID = Convert.ToInt32(collection.GetValue("PublisherName").AttemptedValue);
                book.SupplierID = Convert.ToInt32(collection.GetValue("Name").AttemptedValue);
                book.AuthorID = Convert.ToInt32(collection.GetValue("FullName").AttemptedValue);
                book.CostPrice = Convert.ToDouble(collection.GetValue("books.CostPrice").AttemptedValue);
                book.SellingPrice = Convert.ToDouble(collection.GetValue("books.SellingPrice").AttemptedValue);
                book.DateAdded = DateTime.Now;
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";
                        book.CoverImage = path;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }
                else
                {
                    ViewBag.Message = "You Have not specified a file.";
                }
                TryUpdateModel(book);
                if (ModelState.IsValid)
                {
                    Book ba = new Book();
                    ba = myHandler.AddExperimentBook(book);
                    ba.BookTitle = book.BookTitle;
                    ba.Synopsis = book.Synopsis;
                    ba.ISBN = book.ISBN;
                    ba.BookCategoryID = book.BookCategoryID;
                    ba.PublisherID = book.PublisherID;
                    ba.SupplierID = book.SupplierID;
                    ba.AuthorID = book.AuthorID;
                    ba.CoverImage = book.CoverImage;
                    myHandler.AddBook(ba);
                }
                return RedirectToAction("ManageBooks","Book", book);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int bookId)
        {
            myHandler = new BusinessLogicHandler();
            Book book = myHandler.GetBooks().Single(bk => bk.BookID == bookId);
            return View(book);
        }

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

        public ActionResult Delete(int BookID)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int BookID, FormCollection collection)
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
