using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using urbanbooks.Models;
using System.Web.Mvc;

namespace urbanbooks.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Advanced()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(FormCollection collect)
        {
            #region Get Search Term
            string query = collect.GetValue("query").AttemptedValue;
            #endregion

            #region init search
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel result = new SearchViewModel();
            #endregion

            #region execute search
            result.Query = query;
            result.BookResults = myHandler.BookGlobalSearch(query);
            result.BookCategoryResults = myHandler.BookCategoryGlobalSearch(query);
            result.AuthorResults = myHandler.AuthorGlobalSearch(query);
            result.GadgetResults = myHandler.TechnologyGlobalSearch(query);
            result.GadgetCategoryResults = myHandler.DeviceGlobalSearch(query);
            result.ManufacturerResults = myHandler.ManufacturerGlobalSearch(query);
            result.PublisherResults = myHandler.PublisherGlobalSearch(query);
            #endregion

            return View(result);
        }
        [HttpPost]
        public ActionResult BooksAdvanced(Book_Advanced model)
        {
            #region init Search
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel SModel = new SearchViewModel();
            #endregion

            #region Execute Book Search
            if (model.Author == true)
            {

                if (model.RangeFrom != null && model.RangeTo == null)
                {
                    //DUE TO MULTIPLE AUTHORS NOT WORKING AS EXPECTED !
                }
                else if (model.RangeTo != null && model.RangeFrom == null)
                {
                    //DUE TO MULTIPLE AUTHORS NOT WORKING AS EXPECTED !
                }
                else if (model.RangeFrom != null && model.RangeTo != null)
                {
                    //DUE TO MULTIPLE AUTHORS NOT WORKING AS EXPECTED !
                }
                else
                {
                    //DUE TO MULTIPLE AUTHORS NOT WORKING AS EXPECTED !
                    //SP_AUTHORBOOKSEARCH
                }

            }
            if(model.BookTitle == true)
            {
                if (model.RangeFrom != null && model.RangeTo == null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.BookTitleFromQueryBookSearch("", model.RangeFrom.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.BookTitleFromQueryBookSearch(model.Query, model.RangeFrom.GetValueOrDefault());
                }
                else if (model.RangeTo != null && model.RangeFrom == null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.BookTitleToQueryBookSearch("", model.RangeTo.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.BookTitleToQueryBookSearch(model.Query, model.RangeTo.GetValueOrDefault());
                }
                else if (model.RangeFrom != null && model.RangeTo != null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.BookTitleBETWEENQueryBookSearch("", model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.BookTitleBETWEENQueryBookSearch(model.Query, model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());

                }
                else
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.BookTitleBookSearch("");
                    else
                        SModel.BookResults = myHandler.BookTitleBookSearch(model.Query);
                }
            }
            if (model.Category == true)
            {
                if (model.RangeFrom != null && model.RangeTo == null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.CategoryFromQueryBookSearch("", model.RangeFrom.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.CategoryFromQueryBookSearch(model.Query, model.RangeFrom.GetValueOrDefault());
                }
                else if (model.RangeTo != null && model.RangeFrom == null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.CategoryToQueryBookSearch("", model.RangeTo.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.CategoryToQueryBookSearch(model.Query, model.RangeTo.GetValueOrDefault());
                }
                else if (model.RangeFrom != null && model.RangeTo != null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.CategoryBETWEENQueryBookSeach("", model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.CategoryBETWEENQueryBookSeach(model.Query, model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                }
                else
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.CategoryBookSearch("");
                    else
                        SModel.BookResults = myHandler.BookTitleBookSearch(model.Query);
                }
            }
            if(model.ISBN == true)
            {
                if (model.RangeFrom != null && model.RangeTo == null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.ISBNFromQueryBookSearch("", model.RangeFrom.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.ISBNFromQueryBookSearch(model.Query, model.RangeFrom.GetValueOrDefault());
                }
                else if (model.RangeTo != null && model.RangeFrom == null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.ISBNToQueryBookSearch("", model.RangeTo.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.ISBNToQueryBookSearch(model.Query, model.RangeTo.GetValueOrDefault());
                }
                else if (model.RangeFrom != null && model.RangeTo != null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.ISBNBETWEENQueryBookSearch("", model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.ISBNBETWEENQueryBookSearch(model.Query, model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                }
                else
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.ISBNBookSearch("");
                    else
                        SModel.BookResults = myHandler.ISBNBookSearch(model.Query);
                }
            }
            if (model.Publisher == true)
            {
                if (model.RangeFrom != null && model.RangeTo == null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.PublisherFromQueryBookSearch("", model.RangeFrom.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.PublisherFromQueryBookSearch(model.Query, model.RangeFrom.GetValueOrDefault());
                }
                else if (model.RangeTo != null && model.RangeFrom == null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.PublisherToQueryBookSearch("", model.RangeTo.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.PublisherToQueryBookSearch(model.Query, model.RangeTo.GetValueOrDefault());
                }
                else if (model.RangeFrom != null && model.RangeTo != null)
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.PublisherBETWEENQueryBookSearch("", model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                    else
                        SModel.BookResults = myHandler.PublisherBETWEENQueryBookSearch(model.Query, model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                }
                else
                {
                    if (model.Query == null)
                        SModel.BookResults = myHandler.PublisherBookSearch("");
                    else
                        SModel.BookResults = myHandler.PublisherBookSearch(model.Query);
                }
            }
            if (model.RangeFrom != null && model.RangeTo == null && model.ISBN == false && model.Publisher == false && model.Author == false && model.BookTitle == false && model.Category == false && model.Query == null)
            {
                SModel.BookResults = myHandler.BookTitleFromQueryBookSearch("", model.RangeFrom.GetValueOrDefault());
            }
            if (model.RangeTo != null && model.RangeFrom == null && model.ISBN == false && model.Publisher == false && model.Author == false && model.BookTitle == false && model.Category == false && model.Query == null)
            {
                SModel.BookResults = myHandler.BookTitleToQueryBookSearch("", model.RangeTo.GetValueOrDefault());
            }
            if (model.RangeFrom != null && model.RangeTo != null && model.ISBN == false && model.Publisher == false && model.Author == false && model.BookTitle == false && model.Category == false && model.Query == null)
            {
                SModel.BookResults = myHandler.BookTitleBETWEENQueryBookSearch("", model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
            }
            #endregion

            return View(SModel);
        }

        public ActionResult DevicesAdvanced(Device_Advanced model)
        {
            #region init Search
            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel SModel = new SearchViewModel();
            #endregion

            #region Execute Device Search

            if(model.Category == true)
            {
                if (model.RangeFrom != null && model.RangeTo == null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.CategoryFromQueryDeviceSeach("", model.RangeFrom.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.CategoryFromQueryDeviceSeach(model.Query, model.RangeFrom.GetValueOrDefault());
                }
                if (model.RangeTo != null && model.RangeFrom == null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.CategoryToQueryDeviceSearch("", model.RangeTo.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.CategoryToQueryDeviceSearch(model.Query, model.RangeTo.GetValueOrDefault());
                }
                if (model.RangeFrom != null && model.RangeTo != null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.CategoryBETWEENQueryDeviceSearch("", model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.CategoryBETWEENQueryDeviceSearch(model.Query, model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                }
                else
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.CategoryDeviceSearch("");
                    else
                        SModel.GadgetResults = myHandler.CategoryDeviceSearch(model.Query);
                }
            }
            if (model.Manufacturer == true)
            {
                if (model.RangeFrom != null && model.RangeTo == null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ManufacturerFromQueryDeviceSearch("", model.RangeFrom.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.ManufacturerFromQueryDeviceSearch(model.Query, model.RangeFrom.GetValueOrDefault());
                }
                if (model.RangeTo != null && model.RangeFrom == null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ManufacturerToQueryDeviceSearch("", model.RangeTo.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.ManufacturerToQueryDeviceSearch(model.Query, model.RangeTo.GetValueOrDefault());
                }
                if (model.RangeFrom != null && model.RangeTo != null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ManufacturerBETWEENQueryDeviceSearch("", model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.ManufacturerBETWEENQueryDeviceSearch(model.Query, model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                }
                else
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ManufacturerDeviceSearch("");
                    else
                        SModel.GadgetResults = myHandler.ManufacturerDeviceSearch(model.Query);
                }

            }
            if(model.ModelName == true)
            {
                if (model.RangeFrom != null && model.RangeTo == null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ModelNameFromQueryDeviceSearch("", model.RangeFrom.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.ModelNameFromQueryDeviceSearch(model.Query, model.RangeFrom.GetValueOrDefault());
                }
                if (model.RangeTo != null && model.RangeFrom == null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ModelNameToQueryDeviceSearch("", model.RangeTo.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.ModelNameToQueryDeviceSearch(model.Query, model.RangeTo.GetValueOrDefault());
                }
                if (model.RangeFrom != null && model.RangeTo != null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ModelNameBETWEENQueryDeviceSearch("", model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.ModelNameBETWEENQueryDeviceSearch(model.Query, model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                }
                else
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ModelNameDeviceSearch("");
                    else
                        SModel.GadgetResults = myHandler.ModelNameDeviceSearch(model.Query);
                }

            }
            if(model.ModelNumber == true)
            {
                if (model.RangeFrom != null && model.RangeTo == null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ModelNumberFromQueryDeviceSearch("", model.RangeFrom.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.ModelNumberFromQueryDeviceSearch(model.Query, model.RangeFrom.GetValueOrDefault());
                }
                if (model.RangeTo != null && model.RangeFrom == null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ModelNumberToQueryDeviceSearch("", model.RangeTo.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.ModelNumberToQueryDeviceSearch(model.Query, model.RangeTo.GetValueOrDefault());
                }
                if (model.RangeFrom != null && model.RangeTo != null)
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ModelNumberBETWEENQueryDeviceSearch("", model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                    else
                        SModel.GadgetResults = myHandler.ModelNumberBETWEENQueryDeviceSearch(model.Query, model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
                }
                else
                {
                    if (model.Query == null)
                        SModel.GadgetResults = myHandler.ModelNumberSearch("");
                    else
                        SModel.GadgetResults = myHandler.ModelNumberSearch(model.Query);
                }

            }
            if(model.RangeFrom != null && model.RangeTo == null && model.ModelNumber == false && model.Manufacturer == false && model.ModelName == false && model.Category == false && model.Query == null)
            {
                SModel.GadgetResults = myHandler.ModelNumberFromQueryDeviceSearch("", model.RangeFrom.GetValueOrDefault());
            }
            if (model.RangeTo != null && model.RangeFrom == null && model.ModelNumber == false && model.Manufacturer == false && model.ModelName == false && model.Category == false && model.Query == null)
            {
                SModel.GadgetResults = myHandler.ModelNumberToQueryDeviceSearch("", model.RangeTo.GetValueOrDefault());
            }
            if (model.RangeFrom != null && model.RangeTo != null && model.ModelNumber == false && model.Manufacturer == false && model.ModelName == false && model.Category == false && model.Query == null)
            {
                SModel.GadgetResults = myHandler.ModelNumberBETWEENQueryDeviceSearch("", model.RangeFrom.GetValueOrDefault(), model.RangeTo.GetValueOrDefault());
            }

            #endregion

            return View(SModel);
        }
        [Authorize(Roles="admin")]
        [HttpPost]
        public ActionResult AdminBookSearch(FormCollection collection)
        {
            #region Get Search Query

            string query = collection.GetValue("query").AttemptedValue;

            #endregion

            #region Prep Utilities

            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel model = new SearchViewModel();

            #endregion

            #region Execute Search

            model.Query = query;
            model.BookResults = myHandler.BookGlobalSearch(query);
            model.BookCategoryResults = myHandler.BookCategoryGlobalSearch(query);
            model.AuthorResults = myHandler.AuthorGlobalSearch(query);
            model.PublisherResults = myHandler.PublisherGlobalSearch(query);

            #endregion

            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AdminDeviceSearch(FormCollection collection)
        {
            #region Get Search Query

            string query = collection.GetValue("query").AttemptedValue;

            #endregion

            #region Prep Utilities

            BusinessLogicHandler myHandler = new BusinessLogicHandler();
            SearchViewModel model = new SearchViewModel();

            #endregion

            #region Execute Search

            model.Query = query;
            model.GadgetResults = myHandler.TechnologyGlobalSearch(query);
            model.GadgetCategoryResults = myHandler.DeviceGlobalSearch(query);
            model.ManufacturerResults = myHandler.ManufacturerGlobalSearch(query);

            #endregion

            return View(model);
        }
    }
}