using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult CompanyDetails()
        {
            Company company = new Company();
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            company = myHandler.GetCompanyDetail();
            return View(company);
        }

        public ActionResult Edit()
        {
            Company company = new Company();
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            company = myHandler.GetCompanyDetail();
            return View(company);
        }

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            BusinessLogicHandler myHandler = new BusinessLogicHandler();

            if(ModelState.IsValid)
            {
                myHandler.UpdateCompany(company);
            }
            return View();
        }
    }
}