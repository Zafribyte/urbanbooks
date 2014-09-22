using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using urbanbooks.Models;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class PublisherController : Controller
    {
        
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
        [Authorize(Roles="admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }
        [Authorize(Roles = "admin")]
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
        [AllowAnonymous]
        public ActionResult Books(int PublisherID)
        {

            #region Prep Utilities
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel model = new SearchViewModel();
            #endregion

            #region Get Publisher Books
            model.BookResults = myHandler.GetBooksByPublisher(PublisherID);
            #endregion
            return View(model);
        }
        [Authorize(Roles = "admin")]
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
