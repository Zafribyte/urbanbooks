using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc;
using urbanbooks.Models;
using System.Drawing.Imaging;

namespace urbanbooks.Controllers
{
    public class TechnologyController : Controller
    {
        Technology gadget;
        BusinessLogicHandler myHandler;
        [Authorize(Roles="admin, employee")]
        public ActionResult ManageTechnology()
        { return View(); }

        public ActionResult Categories()
        {
            #region Categories Init

            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel model = new SearchViewModel();

            #endregion

            #region Get Categories From db

            model.GadgetCategoryResults = myHandler.GetTechnologyTypeList();

            #endregion
            return View(model);
        }

        public ActionResult ByCategory (string name, int CategoryID)
        {
            #region Init

            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel model = new SearchViewModel();
            Technology helper = new Technology();
            #endregion

            #region Get Devices By Category

            if (name != null)
            {
                model.GadgetResults = myHandler.CategoryDeviceSearch(name);
                model.TCategory = new TechCategory();
                helper = (Technology)model.GadgetResults.Take(1).FirstOrDefault();
                model.TCategory = myHandler.GetTechnologyType(helper.TechCategoryID);

            }
            else if (CategoryID != 0)
            {
                model.GadgetResults = myHandler.DevicesByCategory(CategoryID);//Replace
                model.TCategory = new TechCategory();
                model.TCategory = myHandler.GetTechnologyType(CategoryID);
            }

            #endregion

            return View(model);
        }

        public ActionResult DeletedIndex()
        {
            myHandler = new BusinessLogicHandler();
            List<Technology> myTechDelete = new List<Technology>();
            myTechDelete = myHandler.GetDeletedDevices();
            myTechDelete.OrderBy(m => m.DateAdded);

            return View(myTechDelete);
        }
        [Authorize(Roles="admin")]
        public ActionResult AdminIndex()
        {
            myHandler = new BusinessLogicHandler();
            List<Technology> myTechList = new List<Technology>();
            myTechList = myHandler.GetTechnology();
            IEnumerable<TechCategory> myType = myHandler.GetTechnologyTypeList();
            ViewBag.TechTypeBag = myType;

            TempData["Alert Message"] = "Device Successfully Deleted";
            
            return View(myTechList);
        }
        public ActionResult Index(int? page)
        {
            myHandler = new BusinessLogicHandler();
            List<Technology> myGadgetList = myHandler.GetTechnology();
            IEnumerable<Manufacturer> manufacturer = myHandler.GetManufacturers();
            ViewBag.Manufact = manufacturer;
            return View(myGadgetList.ToList().ToPagedList(page ?? 1, 18));
        }

        public ActionResult Details(int ProductID)
        {
            #region Prep Utilities

            myHandler = new BusinessLogicHandler();
            AddNewTechViewModel model = new AddNewTechViewModel();

            #endregion

            #region Get Device Data

            model.techs = new Technology();
            model.techs = myHandler.GetTechnologyDetails(ProductID);

            #endregion

            #region Get Category Data

            model.Category = new TechCategory();
            model.Category = myHandler.GetTechnologyType(model.techs.TechCategoryID);

            #endregion

            #region Get Manufacturer Data

            model.mans = new Manufacturer();
            model.mans = myHandler.GetManufacturer(model.techs.ManufacturerID);

            #endregion

            return View(model);
        }
        public ActionResult AdminDetails(int ProductID)
        {
            myHandler = new BusinessLogicHandler();
            gadget = new Technology();
            gadget = myHandler.GetTechnologyDetails(ProductID);
            return View(gadget);
        }

        [Authorize(Roles = "admin, employee")]
        public ActionResult Create()
        {
            AddNewTechViewModel techM = new AddNewTechViewModel();
            /*TEMP LIST*/
            //List<Supplier> nameList = new List<Supplier>();
            SupplierHandler supHandler = new SupplierHandler();
            IEnumerable<Supplier> nameList = (IEnumerable<Supplier>)supHandler.GetTechSupplierList();
            var disp = from nameAndId in nameList
                       select new { Value = nameAndId.SupplierID, Text = nameAndId.Name };

            ViewBag.SupplierList = new SelectList(disp.ToList());

            TechCategoryHandler typeHandler = new TechCategoryHandler();
            IEnumerable<TechCategory> typeList = (IEnumerable<TechCategory>)typeHandler.GetTechCategoryList();
            var dispTC = from name in typeList
                         select new { Value = name.TechCategoryID, Text = name.CategoryName };
            ViewBag.TechCategoryList = new SelectList(dispTC.ToList());

            ManufacturerHandler manHandler = new ManufacturerHandler();
            IEnumerable<Manufacturer> manList = (IEnumerable<Manufacturer>)manHandler.GetManufacturerList();
            var dispM = from mName in manList
                        select new { Value = mName.ManufacturerID, Text = mName.Name };
            ViewBag.ManufacturerList = new SelectList(dispM.ToList());

            List<SelectListItem> supplier = new List<SelectListItem>();
            //supplier.Add(new SelectListItem { Text = "Select Supplier", Value = "", Selected = true });
            foreach (var item in nameList)
            {
                supplier.Add(new SelectListItem { Text = item.Name, Value = item.SupplierID.ToString() });
            }
            techM.suppliers = new List<SelectListItem>();
            techM.suppliers = supplier;
            ViewData["suppliers"] = supplier;

            List<SelectListItem> techCategory = new List<SelectListItem>();
            //techCategory.Add(new SelectListItem { Text = "Select Category", Value = "", Selected = true });
            foreach (var item in typeList)
            {
                techCategory.Add(new SelectListItem { Text = item.CategoryName, Value = item.TechCategoryID.ToString(), Selected = true });
            }
            techM.techCategories = new List<SelectListItem>();
            techM.techCategories = techCategory;
            ViewData["techCategories"] = techCategory;

            List<SelectListItem> manufacturer = new List<SelectListItem>();
            //manufacturer.Add(new SelectListItem { Text = "Select Manufacturer", Value = "", Selected = true });
            foreach (var item in manList)
            {
                manufacturer.Add(new SelectListItem { Text = item.Name, Value = item.ManufacturerID.ToString() });
            }
            techM.manufacturers = new List<SelectListItem>();
            techM.manufacturers = manufacturer;
            ViewData["manufacturers"] = manufacturer;

            return View(techM);
        
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
                {
                
                myHandler = new BusinessLogicHandler();
                gadget = new Technology();
                Company c = new Company();
                gadget.DateAdded = DateTime.Now;
                gadget.ModelName = collection.GetValue("techs.ModelName").AttemptedValue.ToString();
                gadget.Specs = collection.GetValue("techs.Specs").AttemptedValue.ToString();
                gadget.ModelNumber = collection.GetValue("techs.ModelNumber").AttemptedValue.ToString();
                gadget.ManufacturerID = Convert.ToInt32(collection.GetValue("Manufacturer").AttemptedValue);
                gadget.TechCategoryID = Convert.ToInt32(collection.GetValue("CategoryName").AttemptedValue);
                gadget.SupplierID = Convert.ToInt32(collection.GetValue("Name").AttemptedValue);
                gadget.CostPrice = Convert.ToDouble(collection.GetValue("techs.CostPrice").AttemptedValue);
                gadget.SellingPrice = Convert.ToDouble(collection.GetValue("techs.SellingPrice").AttemptedValue);
                gadget.IsBook = false;
                
                
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        file.SaveAs(HttpContext.Server.MapPath("~/Uploads/Tech/") + file.FileName);
                        gadget.ImageFront = file.FileName;
                    }
                    if (file2 != null)
                    {
                        file2.SaveAs(HttpContext.Server.MapPath("~/Uploads/Tech/") + file2.FileName);
                        gadget.ImageTop = file2.FileName;
                    }
                    if (file3 != null)
                    {
                        file3.SaveAs(HttpContext.Server.MapPath("~/Uploads/Tech/") + file3.FileName);
                        gadget.ImageSide = file3.FileName;
                    }
                    Technology ta = new Technology();
                    ta = myHandler.AddExperimentTech(gadget);
                    ta.ModelName = gadget.ModelName;
                    ta.ModelNumber = gadget.ModelNumber;
                    ta.Specs = gadget.Specs;
                    ta.ManufacturerID = gadget.ManufacturerID;
                    ta.TechCategoryID = gadget.TechCategoryID;
                    ta.SupplierID = gadget.SupplierID;
                    //ta.CostPrice = gadget.CostPrice;
                    //ta.SellingPrice = gadget.SellingPrice;
                    //ta.IsBook = gadget.IsBook;
                    ta.ImageFront = gadget.ImageFront;
                    ta.ImageTop = gadget.ImageTop;
                    ta.ImageSide = gadget.ImageSide;
                    myHandler.AddTechnology(ta);

                    TempData["AlertMessage"] = "Device Successfully Added";
                }
                return RedirectToAction("Create", "Technology", gadget);
            }

            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin, employee")]
        public ActionResult Edit(int ProductID)
        {
            AddNewTechViewModel model = new AddNewTechViewModel();

            myHandler = new BusinessLogicHandler();
            gadget = new Technology();
            gadget = myHandler.GetTechnologyDetails(ProductID);

            model.techs = new Technology();
            model.techs = gadget;

            SupplierHandler supHandler = new SupplierHandler();
            /*TEMP LIST*/
            //List<Supplier> nameList = new List<Supplier>();
            IEnumerable<Supplier> nameList = (IEnumerable<Supplier>)supHandler.GetTechSupplierList();
            var disp = from nameAndId in nameList
                       select new { Value = nameAndId.SupplierID, Text = nameAndId.Name };

            ViewBag.SupplierList = new SelectList(disp.ToList());

            TechCategoryHandler typeHandler = new TechCategoryHandler();
            IEnumerable<TechCategory> typeList = (IEnumerable<TechCategory>)typeHandler.GetTechCategoryList();
            var dispTC = from name in typeList
                         select new { Value = name.TechCategoryID, Text = name.CategoryName };
            ViewBag.TechCategoryList = new SelectList(dispTC.ToList());

            ManufacturerHandler manHandler = new ManufacturerHandler();
            IEnumerable<Manufacturer> manList = (IEnumerable<Manufacturer>)manHandler.GetManufacturerList();
            var dispM = from mName in manList
                        select new { Value = mName.ManufacturerID, Text = mName.Name };
            ViewBag.ManufacturerList = new SelectList(dispM.ToList());

            Supplier sp = new Supplier();
            TechCategory tck = new TechCategory();
            Manufacturer mna = new Manufacturer();

            foreach(var item in manList)
            {
                if(item.ManufacturerID == model.techs.ManufacturerID)
                {
                    mna.ManufacturerID = item.ManufacturerID;
                    mna.Name = item.Name;
                }
            }
            foreach(var item in nameList)
            {
                if(item.SupplierID == model.techs.SupplierID)
                {
                    sp.SupplierID = item.SupplierID;
                    sp.Name = item.Name;
                }
            }
            foreach(var item in typeList)
            {
                if(item.TechCategoryID == model.techs.TechCategoryID)
                {
                    tck.TechCategoryID = item.TechCategoryID;
                    tck.CategoryName = item.CategoryName;
                }
            }

            List<SelectListItem> supplier = new List<SelectListItem>(); 
            supplier.Add(new SelectListItem { Value = sp.SupplierID.ToString(), Text = sp.Name, Selected = true });
            foreach (var item in nameList)
            {
                supplier.Add(new SelectListItem { Text = item.Name, Value = item.SupplierID.ToString() });
            }
            model.suppliers = new List<SelectListItem>();
            model.suppliers = supplier;
            ViewData["suppliers"] = supplier;

            List<SelectListItem> techCategory = new List<SelectListItem>();
            techCategory.Add(new SelectListItem { Value = tck.TechCategoryID.ToString(), Text = tck.CategoryName, Selected = true });
            foreach (var item in typeList)
            {
                techCategory.Add(new SelectListItem { Text = item.CategoryName, Value = item.TechCategoryID.ToString() });
            }
            model.techCategories = new List<SelectListItem>();
            model.techCategories = techCategory;
            ViewData["techCategories"] = techCategory;

            List<SelectListItem> manufacturer = new List<SelectListItem>();
            manufacturer.Add(new SelectListItem { Value = mna.ManufacturerID.ToString(), Text = mna.Name, Selected = true });
            foreach (var item in manList)
            {
                manufacturer.Add(new SelectListItem { Text = item.Name, Value = item.ManufacturerID.ToString() });
            }
            model.manufacturers = new List<SelectListItem>();
            model.manufacturers = manufacturer;
            ViewData["manufacturers"] = manufacturer;

            return View(model);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Edit(AddNewTechViewModel model, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();

                #region Cathing model errors
                try
                {
                    model.techs.ManufacturerID = Convert.ToInt32(collection.GetValue("Manufacturer").AttemptedValue);
                    if (ModelState.ContainsKey("Manufacturer"))
                        ModelState["Manufacturer"].Errors.Clear();
                }
                catch
                { }
                #endregion


                if (ModelState.IsValid)
                {
                    
                    model.techs.TechCategoryID = Convert.ToInt32(collection.GetValue("CategoryName").AttemptedValue);
                    model.techs.SupplierID = Convert.ToInt32(collection.GetValue("Name").AttemptedValue);
                    model.techs.Status = Convert.ToBoolean(collection.GetValue("Status"));
                    myHandler.UpdateTechnology(model.techs);
                    myHandler.UpdateTechProduct(model.techs);
                }
                return RedirectToAction("ManageTechnology");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult GetTechMarkup(string selectedValue)
            {
            double s = 0;
            double t = 0;

            List<Company> company = new List<Company>(); BusinessLogicHandler myHandler = new BusinessLogicHandler();
            company = myHandler.GetCompanyDetails();
            double vat = 0;
            foreach (var item in company)
            { vat = item.TechMarkUp; }
            try
            {
                s = Convert.ToDouble(selectedValue);
                t = Math.Round((s * vat) + s, 2);
                return Json(t);
            }
            catch
            {
                return Json("Error");
            }
        }

        [Authorize(Roles = "admin, employee")]
        public ActionResult Delete(int ProductID)
        {
            AddNewTechViewModel model = new AddNewTechViewModel();
            model.techs = new Technology();
            
            myHandler = new BusinessLogicHandler();
            gadget = myHandler.GetTechnologyDetails(ProductID);
            model.techs = gadget;
            return View(model);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Delete(int ProductID, AddNewTechViewModel model, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                gadget = new Technology();
                gadget.ProductID = ProductID;
                myHandler.DeleteTechnology(gadget);

                TempData["Alert Message"] = "Device Successfully Deleted";

                
                return RedirectToAction("AdminIndex", "Technology");
            }

            catch
            {
                return View();
            }
        }
        public ActionResult Restore(int ProductID)
        {
            {
                AddNewTechViewModel model = new AddNewTechViewModel();
                model.techs = new Technology();

                myHandler = new BusinessLogicHandler();
                gadget = myHandler.GetTechnologyDetails(ProductID);
                model.techs = gadget;
                return View(model);
            }
        }
        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Restore(int ProductID, AddNewTechViewModel model, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                gadget = new Technology();
                gadget.ProductID = ProductID;
                myHandler.RestoreDevice(gadget);

                TempData["Alert Message"] = "Device Successfully Restored";


                return RedirectToAction("DeletedIndex", "Technology");
            }

            catch
            {
                return View();
            }
        }

        public ActionResult ByManufacturer(int ManufacturerID)
        {
            #region Prep Utilties
            myHandler = new BusinessLogicHandler();
            IEnumerable<Technology> ManufacturerList = myHandler.GetDevicesByManufacurer(ManufacturerID);
            #endregion

            return View(ManufacturerList);
        }
    }
}
