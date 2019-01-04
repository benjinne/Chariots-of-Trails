using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chariots_of_Trails.Providers;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Chariots_of_Trails.Models;

namespace Chariots_of_Trails.Controllers
{
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
            dataBaseProvider.suggestRouteByRouteId(routeId);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> updateUserRoutes()
        {
            string token = HttpContext.Session.GetString("user_id");
            User user = dataBaseProvider.getUserById(token);
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
            string token = HttpContext.Session.GetString("user_id");
            return Ok(dataBaseProvider.getUserById(token).routes);
        }

        [HttpGet("[action]")]
        public IActionResult suggestedRoutes()
        {
            return Ok(dataBaseProvider.getSuggestedRoutes());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> login([FromQuery(Name = "state")] string state, [FromQuery(Name = "code")] string inCode, [FromQuery(Name = "scope")] string scope)
        {
            User user = await stravaProvider.getUser(state, inCode, scope);
            if(dataBaseProvider.userExists(user))
            {
                //add existing routes from database to user before updating user to prevent deleting routes
                User savedUser = dataBaseProvider.getUserById(user.id);
                if(savedUser.hasRoutes)
                {
                    user.routes = savedUser.routes;
                }
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
    }
}