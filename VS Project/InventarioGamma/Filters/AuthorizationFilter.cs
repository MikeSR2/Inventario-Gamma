using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InventarioGamma.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //do not execute the filter logic for Login/Index
            if (filterContext.RouteData.GetRequiredString("controller").Equals("Login", StringComparison.CurrentCultureIgnoreCase)
                && filterContext.RouteData.GetRequiredString("action").Equals("Login", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            if (filterContext.RouteData.GetRequiredString("controller").Equals("Login", StringComparison.CurrentCultureIgnoreCase)
                && filterContext.RouteData.GetRequiredString("action").Equals("IniciaSesion", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            //if (HttpContext.Current.Session["usuario"] != "Admin" && filterContext.RouteData.GetRequiredString("controller").Equals("Admin", StringComparison.CurrentCultureIgnoreCase)
            //    && filterContext.RouteData.GetRequiredString("action").Equals("Admin", StringComparison.CurrentCultureIgnoreCase))
            //{
            //    filterContext.Result = new RedirectToRouteResult(
            //    new RouteValueDictionary 
            //    { 
            //        { "controller", "Consultas" }, 
            //        { "action", "Consultas" } 
            //    });
            //}
            if (HttpContext.Current.Session["usuario"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary 
                { 
                    { "controller", "Login" }, 
                    { "action", "Login" } 
                });
            }
        }
    }
}