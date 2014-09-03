using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    [Authorize(Roles="admin, supplier")]
    public class SupplierController : Controller
    {
        BusinessLogicHandler myHandler;
        Supplier logistics;
        public ActionResult Index()
        {
            myHandler = new BusinessLogicHandler();
            List<Supplier> newList = myHandler.GetSuppliers();
            return View(newList);
        }

        public ActionResult Details(int id)
        {
            return View();
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
                logistics = new Supplier();
                if (ModelState.IsValid)
                {
                    myHandler.AddSupplier(logistics);
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
