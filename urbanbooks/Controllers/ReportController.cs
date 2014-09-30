using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Detailed()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Detailed(FormCollection collector)
        {
            return View();
        }

        public ActionResult Monthly()
        {
            return View();
        }
    }
}