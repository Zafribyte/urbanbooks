using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using urbanbooks.Models;
using System.Collections;
using Rotativa;
using System.Web.Mvc;
using System.Globalization;

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
            string[] date = collector.GetValue("myRange.From").AttemptedValue.Split('/');
            string dateFrom = date[1] + "/" + date[0] + "/" + date[2];
            DateTime from = Convert.ToDateTime(dateFrom);
            date = collector.GetValue("myRange.To").AttemptedValue.Split('/');
            dateFrom = date[1] + "/" + date[0] + "/" + date[2];
            DateTime to = Convert.ToDateTime(dateFrom);

            #endregion

            try
            {

                #region Get The Data

                invoiceItems = myHandler.SalesGroupedByInvoiceID(from, to);
                invoiceItems.OrderBy(m => m.InvoiceID);

                #endregion

                #region All

                if (model.radioButtons == "All" || model.radioButtons == null)
                {
                    int inv = 0;
                    foreach (var item in invoiceItems)
                    {
                        inv = item.InvoiceID;
                        DetailedCustom customJob = new DetailedCustom();
                        Invoice invoice = new Invoice();
                        invoice = myHandler.GetInvoice(item.InvoiceID);
                        string xdate = invoice.DateCreated.ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        customJob.DateIssued = xdate;
                        customJob.InvoiceID = item.InvoiceID;
                        customJob.InvoiceTotal = item.Price;
                        model.Detailed.Add(customJob);
                    }
                }
                #endregion

                #region Books

                else if (model.radioButtons == "Book")
                {
                    foreach (var item in invoiceItems)
                    {

                        DetailedCustom customJob = new DetailedCustom();
                        Invoice invoice = new Invoice();
                        invoice = myHandler.GetInvoice(item.InvoiceID);
                        string xdate = invoice.DateCreated.ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        customJob.DateIssued = xdate;
                        customJob.InvoiceID = item.InvoiceID;
                        IEnumerable<InvoiceItem> rawItems = myHandler.GetInvoiceItems(item.InvoiceID);
                        foreach (var inv in rawItems)
                        {
                            if (myHandler.CheckProductType(inv.ProductID))
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
                #endregion

                #region Devices
                else if (model.radioButtons == "Dev")
                {
                    foreach (var item in invoiceItems)
                    {
                        DetailedCustom customJob = new DetailedCustom();
                        Invoice invoice = new Invoice();
                        invoice = myHandler.GetInvoice(item.InvoiceID);
                        string xdate = invoice.DateCreated.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        customJob.DateIssued = xdate;
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
                #endregion

                #region Clean Up List

                model.Detailed.OrderBy(m => m.InvoiceID);
                List<DetailedCustom> cleanList = new System.Collections.Generic.List<DetailedCustom>();
                cleanList = model.Detailed.Distinct().ToList();
                model.Detailed = new List<DetailedCustom>();
                model.Detailed = cleanList;

                #endregion

                #region Calc Total

                model.Total = new TotalClass();
                foreach (var item in model.Detailed)
                {
                    model.Total.Total += item.InvoiceTotal;
                }

                #endregion
            }
            catch
            { model.Total = new TotalClass(); }
            

            return View(model);
        }

        public ActionResult Monthly()
        {
            RangeViewModel model = new RangeViewModel();
            model.Total = new TotalClass();
            return View(model);
        }
        [HttpPost]
        public ActionResult Monthly(RangeViewModel model, FormCollection collector)
        {
            #region Prep Utilities

            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            model.MonthlySales = new List<Monthly>();
            string[] date = collector.GetValue("Range.From").AttemptedValue.Split(' ');
            string dateFrom = date[0] + "/" + 01 + "/" + date[1];
            DateTime from = Convert.ToDateTime(dateFrom);
            date = collector.GetValue("Range.To").AttemptedValue.Split(' ');
            dateFrom = date[0] + "/" + 01 + "/" + date[1];
            DateTime to = Convert.ToDateTime(dateFrom);

            #endregion

            try
            {

                #region Get init Data
                IEnumerable<InvoiceItem> rawInvoiceData = myHandler.SalesGroupedByInvoiceID(from, to);
                rawInvoiceData.OrderBy(m => m.InvoiceID);
                #endregion

                #region All

                if (model.radioButtons == "All" || model.radioButtons == null)
                {
                    foreach (var item in rawInvoiceData)
                    {
                        Monthly customJob = new Monthly();
                        Invoice invoice = new Invoice();
                        invoice = myHandler.GetInvoice(item.InvoiceID);
                        string xdate = invoice.DateCreated.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        customJob.Month = xdate;
                        customJob.TotalSales = item.Price;
                        model.MonthlySales.Add(customJob);
                    }
                }
                #endregion

                #region Books

                else if (model.radioButtons == "Book")
                {
                    foreach (var item in rawInvoiceData)
                    {

                        Monthly customJob = new Monthly();
                        Invoice invoice = new Invoice();
                        invoice = myHandler.GetInvoice(item.InvoiceID);
                        string xdate = invoice.DateCreated.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        customJob.Month = xdate;
                        IEnumerable<InvoiceItem> rawItems = myHandler.GetInvoiceItems(item.InvoiceID);
                        foreach (var inv in rawItems)
                        {
                            if (myHandler.CheckProductType(inv.ProductID))
                            {
                                if (customJob.TotalSales == 0.0)
                                { customJob.TotalSales = inv.Price * inv.Quantity; }
                                else { customJob.TotalSales += inv.Price * inv.Quantity; }
                            }
                        }
                        if (customJob.TotalSales == 0.0)
                        { }
                        else
                        { model.MonthlySales.Add(customJob); }

                    }
                }
                #endregion

                #region Devices
                else if (model.radioButtons == "Dev")
                {
                    foreach (var item in rawInvoiceData)
                    {
                        Monthly customJob = new Monthly();
                        Invoice invoice = new Invoice();
                        invoice = myHandler.GetInvoice(item.InvoiceID);
                        string xdate = invoice.DateCreated.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        customJob.Month = xdate;
                        IEnumerable<InvoiceItem> rawItems = myHandler.GetInvoiceItems(item.InvoiceID);
                        foreach (var inv in rawItems)
                        {
                            if (myHandler.CheckProductType(inv.ProductID))
                            {

                            }
                            else
                            {
                                if (customJob.TotalSales == 0.0)
                                { customJob.TotalSales = inv.Price * inv.Quantity; }
                                else { customJob.TotalSales += inv.Price * inv.Quantity; }
                            }
                        }
                        if (customJob.TotalSales == 0.0)
                        { }
                        else
                        { model.MonthlySales.Add(customJob); }
                    }
                }
                #endregion

                #region Clean Up My List

                IEnumerable<Monthly> rawList = model.MonthlySales.Distinct();
                rawList.OrderBy(m => m.Month);
                List<string> Date = new List<string>();
                List<double> metaPrice = new List<double>();
                model.MonthlySales = new List<Monthly>();
                foreach (var item in rawList)
                {
                    if (Date.Contains(item.Month))
                    {
                        int y = Date.IndexOf(item.Month);
                        metaPrice[y] += item.TotalSales;
                    }
                    else
                    {
                        Date.Add(item.Month);
                        metaPrice.Add(item.TotalSales);
                    }
                }
                int zed = 0;
                foreach (var item in Date)
                {
                    Monthly month = new Monthly();
                    month.Month = item;
                    month.TotalSales = metaPrice[zed];
                    model.MonthlySales.Add(month);
                    zed++;
                }
                #endregion

                #region Calc Total

                model.Total = new TotalClass();
                foreach (var item in model.MonthlySales)
                {
                    model.Total.Total += item.TotalSales;
                }

                #endregion
            }
            catch
            { model.Total = new TotalClass(); }

            return View(model);
        }

        public ActionResult Yearly() 
        {
            RangeViewModel model = new RangeViewModel();
            model.Total = new TotalClass();

            return View(model);
        }
        [HttpPost]
        public ActionResult Yearly(RangeViewModel model, FormCollection collector)
        {

            #region Prep Utilities

            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            model.MonthlySales = new List<Monthly>();
            string date = collector.GetValue("Range.From").AttemptedValue;
            string dateFrom = 01 + "/" + 01 + "/" + date;
            DateTime from = Convert.ToDateTime(dateFrom);
            date = collector.GetValue("Range.To").AttemptedValue;
            dateFrom = 01 + "/" + 01 + "/" + date;
            DateTime to = Convert.ToDateTime(dateFrom);

            #endregion

            try
            {

                #region Get init Data
                IEnumerable<InvoiceItem> rawInvoiceData = myHandler.SalesGroupedByInvoiceID(from, to);
                rawInvoiceData.OrderBy(m => m.InvoiceID);
                #endregion

                #region All

                if (model.radioButtons == "All" || model.radioButtons == null)
                {
                    foreach (var item in rawInvoiceData)
                    {
                        Monthly customJob = new Monthly();
                        Invoice invoice = new Invoice();
                        invoice = myHandler.GetInvoice(item.InvoiceID);
                        string xdate = invoice.DateCreated.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        customJob.Month = xdate;
                        customJob.TotalSales = item.Price;
                        model.MonthlySales.Add(customJob);
                    }
                }
                #endregion

                #region Books

                else if (model.radioButtons == "Book")
                {
                    foreach (var item in rawInvoiceData)
                    {

                        Monthly customJob = new Monthly();
                        Invoice invoice = new Invoice();
                        invoice = myHandler.GetInvoice(item.InvoiceID);
                        string xdate = invoice.DateCreated.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        customJob.Month = xdate;
                        IEnumerable<InvoiceItem> rawItems = myHandler.GetInvoiceItems(item.InvoiceID);
                        foreach (var inv in rawItems)
                        {
                            if (myHandler.CheckProductType(inv.ProductID))
                            {
                                if (customJob.TotalSales == 0.0)
                                { customJob.TotalSales = inv.Price * inv.Quantity; }
                                else { customJob.TotalSales += inv.Price * inv.Quantity; }
                            }
                        }
                        if (customJob.TotalSales == 0.0)
                        { }
                        else
                        { model.MonthlySales.Add(customJob); }

                    }
                }
                #endregion

                #region Devices
                else if (model.radioButtons == "Dev")
                {
                    foreach (var item in rawInvoiceData)
                    {
                        Monthly customJob = new Monthly();
                        Invoice invoice = new Invoice();
                        invoice = myHandler.GetInvoice(item.InvoiceID);
                        string xdate = invoice.DateCreated.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        customJob.Month = xdate;
                        IEnumerable<InvoiceItem> rawItems = myHandler.GetInvoiceItems(item.InvoiceID);
                        foreach (var inv in rawItems)
                        {
                            if (myHandler.CheckProductType(inv.ProductID))
                            {

                            }
                            else
                            {
                                if (customJob.TotalSales == 0.0)
                                { customJob.TotalSales = inv.Price * inv.Quantity; }
                                else { customJob.TotalSales += inv.Price * inv.Quantity; }
                            }
                        }
                        if (customJob.TotalSales == 0.0)
                        { }
                        else
                        { model.MonthlySales.Add(customJob); }
                    }
                }
                #endregion

                #region Clean Up My List

                IEnumerable<Monthly> rawList = model.MonthlySales.Distinct();
                rawList.OrderBy(m => m.Month);
                List<string> Date = new List<string>();
                List<double> metaPrice = new List<double>();
                model.MonthlySales = new List<Monthly>();
                foreach (var item in rawList)
                {
                    if (Date.Contains(item.Month))
                    {
                        int y = Date.IndexOf(item.Month);
                        metaPrice[y] += item.TotalSales;
                    }
                    else
                    {
                        Date.Add(item.Month);
                        metaPrice.Add(item.TotalSales);
                    }
                }
                int zed = 0;
                foreach (var item in Date)
                {
                    Monthly month = new Monthly();
                    month.Month = item;
                    month.TotalSales = metaPrice[zed];
                    model.MonthlySales.Add(month);
                    zed++;
                }
                #endregion

                #region Calc Total

                model.Total = new TotalClass();
                foreach (var item in model.MonthlySales)
                {
                    model.Total.Total += item.TotalSales;
                }

                #endregion
            }
            catch
            {
                model.Total = new TotalClass();
            }

            return View(model);
        }
    }
}