using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Models
{
    public class RestoreModelStateFromTempDataAttribute :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.Controller.TempData.ContainsKey("ModelState"))
            {
                filterContext.Controller.ViewData.ModelState.Merge(
                    (ModelStateDictionary)filterContext.Controller.TempData["ModelState"]);
            }
        }
    }
}