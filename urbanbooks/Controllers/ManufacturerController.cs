using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class ManufacturerController : Controller
    {
        // GET: Manufacturer
        public ActionResult Index()
        {
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            IEnumerable<Manufacturer> Manufacturers = myHandler.GetManufacturers();
            return View(Manufacturers);
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

        public ActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            try
            {
                BusinessLogicHandler myHandler = new BusinessLogicHandler();
                myHandler.UpdateManufacturer(manufacturer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manufacturer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manufacturer/Delete/5
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