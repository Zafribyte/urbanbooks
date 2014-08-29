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
    public class TechnologyController : Controller
    {
        Technology gadget;
        BusinessLogicHandler myHandler;
        //[Authorize(Roles="admin, employee")]
        public ActionResult ManageTechnology()
        { return View(); } 


        public ActionResult Index()
        {
            myHandler = new BusinessLogicHandler();
            List<Technology> myGadgetList = myHandler.GetTechnology();
            IEnumerable<Manufacturer> manufacturer = myHandler.GetManufacturers();
            ViewBag.Manufact = manufacturer;
            return View(myGadgetList);
        }

        public ActionResult Details(int gadgetID)
        {
            myHandler = new BusinessLogicHandler();
            gadget = new Technology();
            gadget = myHandler.GetTechnologyDetails(gadgetID);
            return View(gadget);
        }

        //[Authorize(Roles = "admin, employee")]
        public ActionResult Create()
        {
            AddNewTechViewModel techM = new AddNewTechViewModel();

            SupplierHandler supHandler = new SupplierHandler();
            IEnumerable<Supplier> nameList = (IEnumerable<Supplier>)supHandler.GetSupplierList();
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
            supplier.Add(new SelectListItem { Text = "Select Supplier", Value = "", Selected = true });
            foreach (var item in nameList)
            {
                supplier.Add(new SelectListItem { Text = item.Name, Value = item.SupplierID.ToString() });
            }
            techM.suppliers = new List<SelectListItem>();
            techM.suppliers = supplier;
            ViewData["suppliers"] = supplier;

            List<SelectListItem> techCategory = new List<SelectListItem>();
            techCategory.Add(new SelectListItem { Text = "Select Category", Value = "", Selected = true });
            foreach (var item in typeList)
            {
                techCategory.Add(new SelectListItem { Text = item.CategoryName, Value = item.TechCategoryID.ToString() });
            }
            techM.techCategories = new List<SelectListItem>();
            techM.techCategories = techCategory;
            ViewData["techCategories"] = techCategory;

            List<SelectListItem> manufacturer = new List<SelectListItem>();
            manufacturer.Add(new SelectListItem { Text = "Select Manufacturer", Value = "", Selected = true });
            foreach (var item in manList)
            {
                manufacturer.Add(new SelectListItem { Text = item.Name, Value = item.ManufacturerID.ToString() });
            }
            techM.manufacturers = new List<SelectListItem>();
            techM.manufacturers = manufacturer;
            ViewData["manufacturers"] = manufacturer;

            return View(techM);
        
        }

        //[Authorize(Roles = "admin, employee")]
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
                gadget.ManufacturerID = Convert.ToInt32(collection.GetValue("Name").AttemptedValue);
                gadget.TechCategoryID = Convert.ToInt32(collection.GetValue("CategoryName").AttemptedValue);
                gadget.SupplierID = Convert.ToInt32(collection.GetValue("Name").AttemptedValue);
                gadget.CostPrice = Convert.ToDouble(collection.GetValue("techs.CostPrice").AttemptedValue);
                gadget.SellingPrice = Convert.ToDouble(collection.GetValue("techs.SellingPrice").AttemptedValue);
                gadget.IsBook = false;
                
                TryUpdateModel(gadget);
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        file.SaveAs(HttpContext.Server.MapPath("~/Uploads/Tech/ImageFront") + file.FileName);
                        gadget.ImageFront = file.FileName;
                    }
                    if (file2 != null)
                    {
                        file2.SaveAs(HttpContext.Server.MapPath("~/Uploads/Tech/ImageTop") + file2.FileName);
                        gadget.ImageTop = file2.FileName;
                    }
                    if (file3 != null)
                    {
                        file3.SaveAs(HttpContext.Server.MapPath("~/Uploads/Tech/ImageSide") + file3.FileName);
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
                }
                return RedirectToAction("ManageTechnology", "Technology", gadget);
            }

            catch
            {
                return View();
            }
        }

        //[Authorize(Roles = "admin, employee")]
        public ActionResult Edit(int gadgetID)
        {
            myHandler = new BusinessLogicHandler();
            gadget = new Technology();
            gadget = myHandler.GetTechnologyDetails(gadgetID);
            return View(gadget);
        }

        //[Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                gadget = new Technology();
                TryUpdateModel(gadget);
                if (ModelState.IsValid)
                {
                    myHandler.UpdateTechnology(gadget);
                }
                return RedirectToAction("ManageTechnology");
            }
            catch
            {
                return View();
            }
        }

        //[Authorize(Roles = "admin, employee")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //[Authorize(Roles = "admin, employee")]
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
