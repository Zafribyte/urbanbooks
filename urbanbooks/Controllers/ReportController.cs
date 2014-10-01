using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using urbanbooks.Models;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    [Authorize(Roles="admin")]
    public class ReportController : Controller
    {
        public ActionResult Detailed()
        {
            RangeViewModel model = new RangeViewModel();
            return View(model);
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
            invoiceItems = myHandler.SalesGroupedByInvoiceID();
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
                    customJob.InvoiceTotal = item.Price;
                    model.Detailed.Add(customJob);
                }
            }
            #endregion

            #region Books
            else if(model.radioButtons == "Book")
            {
                foreach (var item in invoiceItems)
                {

                    DetailedCustom customJob = new DetailedCustom();
                    Invoice invoice = new Invoice();
                    invoice = myHandler.GetInvoice(item.InvoiceID);
                    customJob.DateIssued = invoice.DateCreated;
                    customJob.InvoiceID = item.InvoiceID;
                    IEnumerable<InvoiceItem> rawItems = myHandler.GetInvoiceItems(item.InvoiceID);
                    foreach(var inv in rawItems)
                    {
                        if(myHandler.CheckProductType(inv.ProductID))
                        {
                            if (customJob.InvoiceTotal == 0.0)
                            { customJob.InvoiceTotal = inv.Price * inv.Quantity; }
                            else {customJob.InvoiceTotal += inv.Price * inv.Quantity; }
                        }
                    }
                    if(customJob.InvoiceTotal == 0.0)
                    { }
                    else
                    { model.Detailed.Add(customJob); }

                }
            }
            #endregion
            else if(model.radioButtons == "Dev")
            {
                foreach(var item in invoiceItems)
                {
                    DetailedCustom customJob = new DetailedCustom();
                    Invoice invoice = new Invoice();
                    invoice = myHandler.GetInvoice(item.InvoiceID);
                    customJob.DateIssued = invoice.DateCreated;
                    customJob.InvoiceID = item.InvoiceID;
                    IEnumerable<InvoiceItem> rawItems = myHandler.GetInvoiceItems(item.InvoiceID);
                    foreach (var inv in rawItems)
                    {
                        if (myHandler.CheckProductType(inv.ProductID))
                        {

                        }
                        else
                        {
                            if (customJob.InvoiceTotal == 0.0)
                            { customJob.InvoiceTotal = inv.Price * inv.Quantity; }
                            else { customJob.InvoiceTotal += inv.Price * inv.Quantity; }
                        }
                    }
                    if (customJob.InvoiceTotal == 0.0)
                    { }
                    else
                    { model.Detailed.Add(customJob); }
                }
            }
            model.Detailed.OrderBy(m => m.InvoiceID);
            List<DetailedCustom> cleanList = new System.Collections.Generic.List<DetailedCustom>();
            cleanList = model.Detailed.Distinct().ToList();
            model.Detailed = new List<DetailedCustom>();
            model.Detailed = cleanList;
            return View(model);
        }

        public ActionResult Monthly()
        {
            return View();
        }
    }
}