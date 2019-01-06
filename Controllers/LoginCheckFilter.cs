using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Chariots_of_Trails.Controllers
{
    //this is used when the server restarts, but a user is still on a page and tries to issue an api call
    // their session cookie invalidates and this redirects them to login
    public class LoginCheckFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ICollection<string> RouteValues = filterContext.ActionDescriptor.RouteValues.Values;
            //check prevents infinite checking
            if( !(RouteValues.Contains("sessionTest") || RouteValues.Contains("login") || RouteValues.Contains("receiveRedirect")) )
            {
                if (filterContext.HttpContext.Session.GetString("user_id") == null) 
                {
                    JsonResult result = new JsonResult(new { error = "user has been logged out", location = "login" });
                    result.StatusCode = 403;
                    filterContext.Result = result;
                    return;
                }
            }
        }
    }
}
