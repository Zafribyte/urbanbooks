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
        Publisher publisher;
        BusinessLogicHandler myHandler;
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
        public ActionResult ViewPublisher()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ViewPublisher(Publisher publisher)
        {
            myHandler = new BusinessLogicHandler();
            if (ModelState.IsValid)
            {
                myHandler.AddPublisher(publisher);
            }
            return Json(new { success = true });
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
        public ActionResult Edit(int PublisherID)
        {
            myHandler = new BusinessLogicHandler();
            publisher = new Publisher();
            publisher.PublisherID = PublisherID;
            publisher = myHandler.GetPublisher(PublisherID);
            return View(publisher);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(int PublisherID, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                publisher = new Publisher();
                TryUpdateModel(publisher);
                if (ModelState.IsValid)
                {
                    myHandler.UpdatePublisher(publisher);
                }
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
        public ActionResult Delete(int PublisherID)
        {
            myHandler = new BusinessLogicHandler();
            publisher = new Publisher();
            publisher.PublisherID = PublisherID;
            publisher = myHandler.GetPublisher(PublisherID);
            return View(publisher);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost]
        public ActionResult Delete(int PublisherID, FormCollection collection)
        {
            try
            {
                myHandler = new BusinessLogicHandler();
                publisher = new Publisher();
                publisher.PublisherID = PublisherID;
                myHandler.DeleteAuthor(PublisherID);

                TempData["Alert Message"] = "Device Successfully Deleted";
                return RedirectToAction("Index", "Publisher");
            }
            catch
            {
                return View();
            }
        }
    }
}
