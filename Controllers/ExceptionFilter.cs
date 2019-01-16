using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chariots_of_Trails.Controllers
{
    //todo redirect to error page
    //copied from here: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.2#exception-filters
    /// <summary>
    /// this is used to catch all exceptions, redirect to login and log the error in the database
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)   
            {
                filterContext.ExceptionHandled = true;
                JsonResult result = new JsonResult(new { error = "an error has occured", location = "login" });
                result.StatusCode = 403;
                filterContext.Result = result;
                //todo log error
                return;
            }  
        }
    }
}
