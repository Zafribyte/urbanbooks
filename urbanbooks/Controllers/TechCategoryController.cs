using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class TechCategoryController : Controller
    {
        BusinessLogicHandler myHandler;
        TechCategory tech;
        // GET: TechCategory
        public ActionResult Index()
        {
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            IEnumerable<TechCategory> TechCategory = myHandler.GetTechnologyTypeList();
            
            return View(TechCategory);
        }
        public ActionResult ViewTechCategory()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ViewTechCategory(TechCategory tech)
        {
            myHandler = new BusinessLogicHandler();
            if (ModelState.IsValid)
            {
                myHandler.AddTechnologyType(tech);
            }
            return Json(new { success = true });
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TechCategory techCategory)
        {
            try
            {
                BusinessLogicHandler myHandler = new BusinessLogicHandler();
                myHandler.AddTechnologyType(techCategory);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int TechCategoryID)
        {
            myHandler = new BusinessLogicHandler();
            tech = new TechCategory(); 
            tech.TechCategoryID = TechCategoryID;
            tech = myHandler.GetTechnologyType(TechCategoryID);
            return View(tech);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(int TechCategoryID, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                tech = new TechCategory();
                TryUpdateModel(tech);
                if (ModelState.IsValid)
                {
                    myHandler.UpdateTechnologyType(tech);
                }
                return RedirectToAction("Index", "TechCategory");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int TechCategoryID)
        {
            myHandler = new BusinessLogicHandler();
            tech = new TechCategory();
            tech.TechCategoryID = TechCategoryID;
            tech = myHandler.GetTechnologyType(TechCategoryID);
            return View(tech);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Delete(int TechCategoryID, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                tech = new TechCategory();
                tech.TechCategoryID = TechCategoryID;
                myHandler.DeleteTechnologyType(TechCategoryID);

                TempData["Alert Message"] = "Category Successfully Deleted";
                return RedirectToAction("Index", "TechCategory");
            }
            catch
            {
                return View();
            }
        }
    }
}