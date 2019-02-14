using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chariots_of_Trails.Providers;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Chariots_of_Trails.Models;
using System;

namespace Chariots_of_Trails.Controllers
{
    [LoginCheckFilter]
    [ServiceFilter(typeof(ExceptionFilter))]
    [Route("api/[controller]")]
    public class MainController : Controller
    {
        private readonly IStravaProvider stravaProvider;
        private readonly IDataBaseProvider dataBaseProvider;

        public MainController(IStravaProvider stravaProvider, IDataBaseProvider dataBaseProvider)
        {
            this.stravaProvider = stravaProvider;
            this.dataBaseProvider = dataBaseProvider;
        }

        [HttpPost("[action]")]
        public IActionResult suggestRoute([FromQuery(Name = "routeId")] string routeId)
        {
            string userId = HttpContext.Session.GetString("user_id");
            dataBaseProvider.suggestRouteByRouteIdAndUserId(routeId, userId);
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult upVote([FromQuery(Name = "routeId")] string routeId)
        {
            string userId = HttpContext.Session.GetString("user_id");
            dataBaseProvider.upVoteByRouteIdAndUserId(routeId, userId);
            return Ok();
        }
        
        [HttpPost("[action]")]
        public IActionResult downVote([FromQuery(Name = "routeId")] string routeId)
        {
            string userId = HttpContext.Session.GetString("user_id");
            dataBaseProvider.downVoteByRouteIdAndUserId(routeId, userId);
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> updateUserRoutes()
        {
            string userId = HttpContext.Session.GetString("user_id");
            
            User user = dataBaseProvider.getUserById(userId);
            Console.WriteLine($"SessionId {userId}");
            Console.WriteLine($"updating {user}'s routes");
            Console.WriteLine($"updating {user.id}'s routes");
            Console.WriteLine($"updating {user.athlete}'s routes");
            Console.WriteLine($"updating {user.athlete.fullname}'s routes");
            user.routes = await stravaProvider.getUserRoutes(user);
            dataBaseProvider.updateUser(user);
            return Ok(user.routes);
        }

        [HttpGet("[action]")]
        public IActionResult sessionTest()
        {
            return Ok(HttpContext.Session.GetString("access_token") != null);
        }

        [HttpGet("[action]")]
        public IActionResult userRoutes()
        {
            string userId = HttpContext.Session.GetString("user_id");
            User user = dataBaseProvider.getUserById(userId);
            Console.WriteLine($"SessionId {userId}");
            Console.WriteLine($"getting {user}'s routes");
            Console.WriteLine($"getting {user.id}'s routes");
            Console.WriteLine($"getting {user.athlete}'s routes");
            Console.WriteLine($"getting {user.athlete.fullname}'s routes");
            return Ok(user.routes);
        }

        [HttpGet("[action]")]
        public IActionResult suggestedRoutes()
        {
            return Ok(dataBaseProvider.getSuggestedRoutes());
        }

        [HttpGet("[action]")]
        public IActionResult login()
        {
            return Ok(stravaProvider.login(Request.Host.ToString()));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> receiveRedirect([FromQuery(Name = "state")] string state, [FromQuery(Name = "code")] string inCode, [FromQuery(Name = "scope")] string scope)
        {
            //todo check on each strava api call if users access token has expired and if so refresh it
            //todo if user has token that's not expired, don't try to get a new one
            try
            {
                User user = await stravaProvider.getUser(state, inCode, scope);
                if(dataBaseProvider.userExists(user))
                {
                    //add existing routes from database to user before updating user in database to prevent deleting routes
                    user.routes = dataBaseProvider.getUserById(user.id).routes;
                    dataBaseProvider.updateUser(user);
                }
                else
                {
                    dataBaseProvider.insertUser(user);
                }
                HttpContext.Session.SetString("access_token", user.access_token);
                HttpContext.Session.SetString("user_id", user.id);
                return Redirect("/");
            }
            catch (Exception ex)
            {
                //exception is handled here because this is a request made by the browser and redirecting can be done here,
                //however all other calls are made by axios where redirecting is handled in ExceptionFilter
                dataBaseProvider.logException(ex);
                return Redirect("/login");
            }
        }
    }
}