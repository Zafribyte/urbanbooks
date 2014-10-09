using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class ManufacturerController : Controller
    {
        Manufacturer manufacturer;
        BusinessLogicHandler myHandler;
        // GET: Manufacturer
        public ActionResult Index()
        {
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            IEnumerable<Manufacturer> Manufacturers = myHandler.GetManufacturers();
            return View(Manufacturers);
        }
        public ActionResult CheckDuplicates(string manufact)
        {
            List<Manufacturer> myList = new List<Manufacturer>();
            myHandler = new BusinessLogicHandler();
            myList = myHandler.CheckDuplicatedManufacturer(manufact);
            var isDuplicate = false;

            if (myList != null)
            {
                foreach (var item in myList)
                {
                    string manName = item.Name;
                    if (manufact.ToUpper() == manName.ToUpper())
                    {
                        isDuplicate = true;
                    }
                }
            }
            var jsonData = new { isDuplicate };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewManufacturer()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ViewManufacturer(Manufacturer manufacturer)
        {
            myHandler = new BusinessLogicHandler();
            if (ModelState.IsValid)
            {
                myHandler.AddManufacturer(manufacturer);
            }
            return Json(new { success = true });
        }
        // GET: Manufacturer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturer/Create
        [HttpPost]
        public ActionResult Create(Manufacturer manufacturer)
        {
            try
            {
                BusinessLogicHandler myHandler = new BusinessLogicHandler();
                myHandler.AddManufacturer(manufacturer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int ManufacturerID)
        {
            myHandler = new BusinessLogicHandler();
            manufacturer = new Manufacturer();
            manufacturer.ManufacturerID = ManufacturerID;
            manufacturer = myHandler.GetManufacturer(ManufacturerID);
            return View(manufacturer);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(int ManufacturerID, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                manufacturer = new Manufacturer();
                TryUpdateModel(manufacturer);
                if (ModelState.IsValid)
                {
                    myHandler.UpdateManufacturer(manufacturer);
                }
                return RedirectToAction("Index", "Manufacturer");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manufacturer/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int ManufacturerID)
        {
            myHandler = new BusinessLogicHandler();
            manufacturer = new Manufacturer();
            manufacturer.ManufacturerID = ManufacturerID;
            manufacturer = myHandler.GetManufacturer(ManufacturerID);
            return View(manufacturer);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Delete(int ManufacturerID, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                manufacturer = new Manufacturer();
                manufacturer.ManufacturerID = ManufacturerID;
                myHandler.DeleteManufacturer(ManufacturerID);

                TempData["Alert Message"] = "Device Successfully Deleted";
                return RedirectToAction("Index", "Manufacturer");
            }
            catch
            {
                return View();
            }
        }
    }
}