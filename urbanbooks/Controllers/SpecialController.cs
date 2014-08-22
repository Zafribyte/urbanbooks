using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class SpecialController : Controller
    {
        BusinessLogicHandler myHandler;
        Special mySpecial;
        List<Special> mySpecialList;
        public ActionResult Index()
        {
            myHandler = new BusinessLogicHandler(); 
            IEnumerable<Special> listing = (IEnumerable<Special>)myHandler.GetSpecialsList();
            return View(listing);
        }

        public ActionResult Details(int id)
        {
            myHandler = new BusinessLogicHandler();
            mySpecial = myHandler.GetSpecialsList().Single(sp => sp.SpecialID == id);

            return View(mySpecial);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                mySpecial = new Special();
                if (ModelState.IsValid)
                {
                    myHandler.AddSpecial(mySpecial);
                }
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
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                mySpecial = new Special();
                TryUpdateModel(mySpecial);
                if (ModelState.IsValid)
                {
                    myHandler.UpdateSpecial(mySpecial);
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
