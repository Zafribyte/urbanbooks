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
        [Authorize(Roles="admin, employee")]
        public ActionResult AdminIndex()
        {
            myHandler = new BusinessLogicHandler();
            List<Book> myBookList = new List<Book>();
            myBookList = myHandler.GetBooks();
            IEnumerable<BookCategory> myType = myHandler.GetBookCategoryList();
            ViewBag.BookTypeBag = myType;
            return View(myBookList);
        }
        public ActionResult CustomerDetails(int ProductID)
        {
            #region Prep Utilities

            myHandler = new BusinessLogicHandler();
            AddNewBookViewModel model = new AddNewBookViewModel();
            book = new Book();
            BookCategory category = new BookCategory();
            Publisher pub = new Publisher();
            Author authors = new Author();

            #endregion

            #region Get Book Data

            book = myHandler.User_GetBook(ProductID);
            model.books = new Book();
            model.books = book;
            model.book = new List<Book>();
            model.book.Add(book);

            #endregion

            #region Get Book Category Data

            category = myHandler.GetBookType(book.BookCategoryID);
            model.bc = new BookCategory();
            model.bc = category;
            model.bookCategory = new List<BookCategory>();
            model.bookCategory.Add(category);

            #endregion

            #region Get Publisher Data

            pub = myHandler.GetPublisher(book.PublisherID);
            model.publisher = new Publisher();
            model.publisher = pub;
            model.Publishers = new List<Publisher>();
            model.Publishers.Add(pub);

            #endregion

            #region Get Authors Data

            model.Author = myHandler.GetAuthorsPerBook(book.BookID);

            #endregion

            return View(model);
        }
        public ActionResult Details(int ProductID)
        {
            myHandler = new BusinessLogicHandler();
            Book book = myHandler.GetBook(ProductID);
            return View(book);
        }
        public ActionResult Create()
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
            //bookCategory.Add(new SelectListItem { Text = "Select Category", Value = "", Selected = true });
            foreach (var item in typeList)
            {
                bookCategory.Add(new SelectListItem { Text = item.CategoryName, Value = item.BookCategoryID.ToString() });
            }
            bookM.bookCategories = new List<SelectListItem>();
            bookM.bookCategories = bookCategory;
            ViewData["bookCategories"] = bookCategory;

            List<SelectListItem> supplier = new List<SelectListItem>();
            //supplier.Add(new SelectListItem { Text = "Select Supplier", Value = "", Selected = true });
            foreach (var item in nameList)
            {
                supplier.Add(new SelectListItem { Text = item.Name, Value = item.SupplierID.ToString() });
            }
            bookM.suppliers = new List<SelectListItem>();
            bookM.suppliers = supplier;
            ViewData["suppliers"] = supplier;

            List<SelectListItem> author = new List<SelectListItem>();
            //author.Add(new SelectListItem { Text = "Select Author", Value = "", Selected = true });
            foreach (var item in authList)
            {
                author.Add(new SelectListItem { Text = item.Name, Value = item.AuthorID.ToString() });
            }
            bookM.authors = new List<SelectListItem>();
            bookM.authors = author;
            ViewData["authors"] = author;

            List<SelectListItem> publisher = new List<SelectListItem>();
            //publisher.Add(new SelectListItem { Text = "Select Publisher", Value = "", Selected = true });
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
        public ActionResult Create(FormCollection collection, HttpPostedFileBase file)
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
                //book.AuthorID = Convert.ToInt32(collection.GetValue("FullName").AttemptedValue);
                string[] Authors = (string[])collection.GetValue("FullName").RawValue;
                book.CostPrice = Convert.ToDouble(collection.GetValue("books.CostPrice").AttemptedValue);
                book.SellingPrice = Convert.ToDouble(collection.GetValue("books.SellingPrice").AttemptedValue);
                book.DateAdded = DateTime.Now;

                //TryUpdateModel(book); //CONSIDER REMOVING THIS..!
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        file.SaveAs(HttpContext.Server.MapPath("~/Uploads/Books/") + file.FileName);
                        book.CoverImage = file.FileName;
                    }

                    Book ba = new Book();
                    BookAuthor bookAuthors = new BookAuthor();

                    ba = myHandler.AddExperimentBook(book);
                    ba.BookTitle = book.BookTitle;
                    ba.Synopsis = book.Synopsis;
                    ba.ISBN = book.ISBN;
                    ba.BookCategoryID = book.BookCategoryID;
                    ba.PublisherID = book.PublisherID;
                    ba.SupplierID = book.SupplierID;
                    ba.CoverImage = book.CoverImage;

                    //myHandler.AddBook(ba);
                    bookAuthors = myHandler.TrialInsertBook(ba);

                    foreach (var item in Authors) //INSERTING BOOK AUTHORS
                    {
                        bookAuthors.AuthorID = Convert.ToInt32(item);
                        myHandler.InsertBookAuthor(bookAuthors);
                    }
                }
                //return RedirectToAction("Index", "Book", book);
                return RedirectToAction("Create", "Book", null); //REDIRECT TO GET ACTION METHOD #INCASE THEY WANT TO INSERT ANOTHER BOOK :)
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Categories()
        {
            #region Init Categories

            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel model = new SearchViewModel();

            #endregion

            #region Get Categories From db

            model.BookCategoryResults = myHandler.GetBookCategoryList();

            #endregion

            return View(model);
        }

        public ActionResult ByCategory(string name, int CategoryID)
        {

            #region Init

            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel model = new SearchViewModel();
            Book helper = new Book();
            #endregion

            #region Get Books By Category 

            if(name != null)
            {
                model.BookResults = myHandler.CategoryBookSearch(name);
                model.BCategory = new BookCategory();
                helper = (Book)model.BookResults.Take(1).FirstOrDefault();
                model.BCategory = myHandler.GetBookType(helper.BookCategoryID);

            }
            else if(CategoryID != 0)
            {
                model.BookResults = myHandler.GetBooksByCategory(CategoryID);
                model.BCategory = new BookCategory();
                model.BCategory = myHandler.GetBookType(CategoryID);
            }

            #endregion

            return View(model);
        }
        public ActionResult Edit(int productId)
        {
            myHandler = new BusinessLogicHandler();
            Book book = myHandler.GetBooks().Single(bk => bk.ProductID == productId);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book bl, Book b)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                book = new Book();
                TryUpdateModel(bl);
                TryUpdateModel(b);
                if (ModelState.IsValid)
                {
                    //myHandler.UpdateBookProduct(b);
                    myHandler.UpdateBook(bl);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult GetBookMarkup(string selectedValue)
        {
            double s = 0;
            double t = 0;

            List<Company> company = new List<Company>(); BusinessLogicHandler myHandler = new BusinessLogicHandler();
            company = myHandler.GetCompanyDetails();
            double vat = 0;
            foreach (var item in company)
            { vat = item.BookMarkUp; }
            try
            {
                s = Convert.ToDouble(selectedValue);
                t = (s * vat) + s;
                return Json(t);
            }
            catch
            {
                return Json("Error");
            }
        }
        public ActionResult Delete(int ProductID)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int ProductID, FormCollection collection)
        {
            try
            {
                return RedirectToAction("AdminIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}
