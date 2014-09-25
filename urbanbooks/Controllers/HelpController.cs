using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class HelpController : Controller
    {
        public ActionResult FAQs()
        {
            #region Check iDentity
            if (HttpContext.User.IsInRole("admin"))
            {
                return RedirectToAction("Documentation");
            }
            #endregion

            return View();
        }
        [Authorize(Roles="admin") ]
        public ActionResult Documentation()
        {
            return View();
        }
    }
}