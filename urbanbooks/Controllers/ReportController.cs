using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using urbanbooks.Models;
using System.Collections;
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
        public ActionResult Detailed(RangeViewModel model, FormCollection collector)
        {
            #region Prep Utilities

            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            IEnumerable<InvoiceItem> invoiceItems;
            model.Detailed = new List<DetailedCustom>();

            #endregion

            #region Get The Data
            invoiceItems = myHandler.Sales();
            invoiceItems.OrderBy(m => m.InvoiceID);
            #endregion

            #region All
            if (model.radioButtons == "All" || model.radioButtons == null)
            {
                int inv = 0;
                foreach(var item in invoiceItems)
                {
                    inv = item.InvoiceID;
                    DetailedCustom customJob = new DetailedCustom();
                    Invoice invoice = new Invoice();
                    invoice = myHandler.GetInvoice(item.InvoiceID);
                    customJob.DateIssued = invoice.DateCreated;
                    customJob.InvoiceID = item.InvoiceID;

                    //while(item.InvoiceID == inv)
                    //{
                    //    customJob.InvoiceTotal += (item.Price*item.Quantity);
                    //}

                    foreach (var iterate in invoiceItems)
                    {
                        if(iterate.InvoiceID == item.InvoiceID)
                        {
                            customJob.InvoiceTotal += (iterate.Price * iterate.Quantity);
                        }
                    }
                    if(model.Detailed.Contains(customJob))
                    {

                    }
                    else{
                    model.Detailed.Add(customJob);
                    }
                }
            }
            #endregion

            #region Books
            else if(model.radioButtons == "Books")
            {
                foreach (var item in invoiceItems)
                {
                    if(myHandler.CheckProductType(item.ProductID))
                    {

                    }
                }
            }
            #endregion
            else if(model.radioButtons == "Dev")
            {

            }

            return View(model);
        }

        public ActionResult Monthly()
        {
            return View();
        }
    }
}