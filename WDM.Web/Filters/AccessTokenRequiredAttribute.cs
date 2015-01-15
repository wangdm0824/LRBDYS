using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WDM.Web.Filters
{
    public class AccessTokenRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["AccessToken"] == null)
            {
                filterContext.Result = new RedirectResult("~/");
            }
        }
    }
}