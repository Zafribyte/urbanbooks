using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class PublisherController : Controller
    {
        // GET: Publisher
        public ActionResult Index()
        {
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            IEnumerable<Publisher> Publishers = myHandler.GetPublishers();
            return View(Publishers);
        }

        public ActionResult Details(int id)
        {
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            Publisher publisher = new Publisher();
            publisher = myHandler.GetPublisher(id);
            return View(publisher);
        }

        // GET: Publisher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publisher/Create
        [HttpPost]
        public ActionResult Create(Publisher publisher)
        {
            try
            {
                BusinessLogicHandler myHandler = new BusinessLogicHandler();
                myHandler.AddPublisher(publisher);
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
        public ActionResult Edit(Publisher publisher)
        {
            try
            {
                BusinessLogicHandler myHandler = new BusinessLogicHandler();
                myHandler.UpdatePublisher(publisher);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Publisher/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Publisher/Delete/5
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
