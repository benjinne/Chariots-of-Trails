using Chariots_of_Trails.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chariots_of_Trails.Controllers
{
    //todo redirect to error page
    //learn more here: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.2#exception-filters
    // and here: https://stackoverflow.com/questions/36109052/inject-service-into-action-filter/36109690
    /// <summary>
    /// this is used to catch all exceptions, redirect to login and log the error in the database
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDataBaseProvider dataBaseProvider;

        public ExceptionFilter(IDataBaseProvider dataBaseProvider)
        {
            this.dataBaseProvider = dataBaseProvider;
        }
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)   
            {
                JsonResult result = new JsonResult(new { error = "an error has occured", location = "login" });
                result.StatusCode = 403;
                filterContext.Result = result;
                filterContext.ExceptionHandled = true;
                dataBaseProvider.logException(filterContext.Exception);
                return;
            }  
        }
    }
}
