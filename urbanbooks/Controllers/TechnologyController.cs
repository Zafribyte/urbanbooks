using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class TechnologyController : Controller
    {
        Technology gadget;
        BusinessLogicHandler myHandler;
        [Authorize(Roles="admin, employee")]
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

        [Authorize(Roles = "admin, employee")]
        public ActionResult Create()
        {
            myHandler = new BusinessLogicHandler();
            IEnumerable<TechCategory> toys = (IEnumerable<TechCategory>)myHandler.GetTechnologyTypeList();
            var thisList = from list in toys
                           select new { Value = list.TechCategoryID, Text = list.CategoryName };
            return View();
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                gadget = new Technology();

                return RedirectToAction("ManageTechnology");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin, employee")]
        public ActionResult Edit(int gadgetID)
        {
            myHandler = new BusinessLogicHandler();
            gadget = new Technology();
            gadget = myHandler.GetTechnologyDetails(gadgetID);
            return View(gadget);
        }

        [Authorize(Roles = "admin, employee")]
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
