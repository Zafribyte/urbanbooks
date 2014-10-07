using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using urbanbooks.Models;
using System.Collections;

namespace urbanbooks.Controllers
{
    [Authorize(Roles="admin, supplier")]
    public class SupplierController : Controller
    {
        private ApplicationUserManager _userManager;
        BusinessLogicHandler myHandler;
        Supplier logistics;
        public ActionResult BookIndex()
        {
            myHandler = new BusinessLogicHandler();
            List<Supplier> mySuppBookList = new List<Supplier>();
            mySuppBookList = myHandler.GetBookSuppliers();

            return View(mySuppBookList);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize(Roles="supplier")]
        public ActionResult Home()
        {
            #region Prep Utilities

            myHandler = new BusinessLogicHandler();
            Supplier supplier = new Supplier();
            RangeViewModel model = new RangeViewModel();

            #endregion

            #region Get User(Supplier)

            string userName = User.Identity.GetUserName();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            _userManager = new ApplicationUserManager(myStore);
            var user = _userManager.FindByEmail(userName);

            #endregion

            #region Get Supplier Details

            supplier = myHandler.GetSupplier(user.Id);

            #endregion

            #region Get Orders For Supplier

            model.Orders = myHandler.GetSupplierOrders(supplier.SupplierID);
            model.Orders.OrderBy(m => m.DateCreated);
            #endregion

            return View(model);
        }

        public ActionResult Technology(int? page)
        {
            #region Prep Utilities
            myHandler = new BusinessLogicHandler();
            #endregion

            #region Get data
            IEnumerable<Supplier> suppliers = myHandler.GetTechSuppliers();
            #endregion

            return View(suppliers.ToList().ToPagedList(page ?? 1, 10));
        }

        public ActionResult Product(int ProductID)
        {

            #region Prep Utilities
            myHandler = new BusinessLogicHandler();
            UnifiedViewModel model = new UnifiedViewModel();
            Author Author = new Author();
            #endregion

            #region Config Roles
            if(User.IsInRole("supplier"))
            {
                model.iSupplier = true;
            }
            #endregion

            #region Get the Data
            if (myHandler.CheckProductType(ProductID))
            {
                model.Book = new Book();
                model.Book = myHandler.GetBook(ProductID);
                model.BookCategory = new BookCategory();
                model.BookCategory = myHandler.GetBookCategory(model.Book.BookCategoryID);
                model.Publisher = new Publisher();
                model.Publisher = myHandler.GetPublisher(model.Book.PublisherID);
                IEnumerable<BookAuthor> authorsINbook = myHandler.GetBookAuthors(model.Book.BookID);
                model.Authors = new List<Author>();
                foreach(var item in authorsINbook)
                {
                    Author = myHandler.GetAuthorDetails(item.AuthorID);
                    model.Authors.Add(Author);
                }
            }
            else
            {
                model.Device = new Technology();
                model.Device = myHandler.GetTechnologyDetails(ProductID);
                model.Manufacturer = new Manufacturer();
                model.Manufacturer = myHandler.GetManufacturer(model.Device.ManufacturerID);
                model.Category = new TechCategory();
                model.Category = myHandler.GetTechnologyType(model.Device.TechCategoryID);
            }
            #endregion

            return View(model);
        }

        public ActionResult Create()
        {
            #region Prep Utilities
            myHandler = new BusinessLogicHandler();
            SupplierViewModel model = new SupplierViewModel();
            #endregion

            #region Get The data
            //model.RegisteredSuppliers = myHandler.GetSuppliers();
            #endregion

            #region Set Up dropdown

            model.SupplierType = new List<SelectListItem>();
            model.SupplierType.Add(
                new SelectListItem { Text = "Please Select Supplier Type", Value = "", Selected = true}
            );
            model.SupplierType.Add(
                new SelectListItem { Text = "Book Supplier", Value = "0" }
                );
            model.SupplierType.Add(
                new SelectListItem { Text = "Technology Supplier", Value = "1" }
                );
            ViewData["SupplierType"] = model.SupplierType;
            #endregion

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Create(SupplierViewModel model, FormCollection collect)
        {

            #region Cathing model errors
            var error = ModelState.Values.SelectMany(e => e.Errors);
            var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();
            #endregion

            #region Prep Utilities
            SupplierContext dataSocket = new SupplierContext();
            ApplicationDbContext context = new ApplicationDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(context);
            ApplicationUserManager userMgr = new ApplicationUserManager(myStore);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            #endregion

            #region Bend the rules
            try
            {
                if(collect.GetValue("SupplierType").AttemptedValue == "0")
                { /*Book Supplier*/ model.RegisterNewSupplier.SupplierType = true; }
                else if (collect.GetValue("SupplierType").AttemptedValue == "1")
                { /*Technology Supplier*/ model.RegisterNewSupplier.SupplierType = false; }
                if (ModelState.ContainsKey("SupplierType"))
                    ModelState["SupplierType"].Errors.Clear();
            }
            catch
            { ModelState.AddModelError("SupplierType", "Please select a valid supplier type from dropdown !"); }
            #endregion
            try
            {
                if (ModelState.IsValid)
                {
                    #region Register Supplier

                    var user = new ApplicationUser() { UserName = model.RegisterNewSupplier.Email, Email = model.RegisterNewSupplier.Email, Address = model.RegisterNewSupplier.Address, PhoneNumber = model.RegisterNewSupplier.ContactPersonNumber };
                    Models.Supplier supplier = new Models.Supplier { Name = model.RegisterNewSupplier.Name, ContactPerson = model.RegisterNewSupplier.ContactPerson, Fax = model.RegisterNewSupplier.Fax, ContactPersonNumber = model.RegisterNewSupplier.ContactPersonNumber, LastName = model.RegisterNewSupplier.LastName, User_Id = user.Id, IsBookSupplier = model.RegisterNewSupplier.SupplierType };
                    user.Carts = new Cart { DateLastModified = DateTime.Now };
                    user.Wishlists = new Wishlist { Status = false };
                    IdentityResult result = await userMgr.CreateAsync(user, model.RegisterNewSupplier.Password);
                    roleManager.Create(new IdentityRole { Name = "supplier" });
                    userMgr.AddToRole(user.Id, "supplier");
                    //dataSocket.Suppliers.Add(supplier);
                    dataSocket.Suppliers.Add(supplier);

                    dataSocket.SaveChanges();

                     #endregion

                    return RedirectToAction("AdminIndex", "Admin", null);
                }

                return View();

            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Search(FormCollection collector)
        {
            #region Prep Utilities

            string Query = collector.GetValue("query").AttemptedValue;
            myHandler = new BusinessLogicHandler();
            Supplier supplier = new Supplier();
            InvoiceModel model = new InvoiceModel();

            #endregion

            #region Get User(Supplier)

            string userName = User.Identity.GetUserName();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            _userManager = new ApplicationUserManager(myStore);
            var user = _userManager.FindByEmail(userName);

            #endregion

            #region Get Supplier Details

            supplier = myHandler.GetSupplier(user.Id);

            #endregion


            #region Get the data

            model.Order = new Order();
            model.Invoice = new Invoice();
            try
            {
                model.Order = myHandler.GetOrder(Convert.ToInt32(Query));
            }
            catch
            { model.Order = null; }
            try
            {
                model.Invoice = myHandler.GetInvoice(Convert.ToInt32(Query));
            }
            catch
            { model.Invoice = null; }
            ///get product
            #endregion


            return View(model);
        }

        public ActionResult Edit(int SupplierID)
        {
            myHandler = new BusinessLogicHandler();
            logistics = new Supplier();
            logistics.SupplierID = SupplierID;
            logistics = myHandler.GetSupplier(SupplierID);
            return View(logistics);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                logistics = new Supplier();
                TryUpdateModel(logistics);
                if (ModelState.IsValid)
                {
                    myHandler.UpdateSupplier(logistics);
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
