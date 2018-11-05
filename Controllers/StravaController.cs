using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chariots_of_Trails.Providers;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;

using Strava.Authentication;
using Strava.Clients;
using Strava.Athletes;

namespace Chariots_of_Trails.Controllers
{
    [Route("api/[controller]")]
    public class StravaController : Controller
    {
        public static readonly HttpClient client = new HttpClient();
        private readonly IStravaProvider stravaProvider;

        public StravaController(IStravaProvider stravaProvider)
        {
            this.stravaProvider = stravaProvider;
        }

        //test for token, returns true if token exists
        [HttpGet("[action]")]
        public IActionResult sessionTest()
        {
            return Ok(HttpContext.Session.GetString("access_token") != null);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> users()
        {
            //todo handle token expired exception
            string token = HttpContext.Session.GetString("access_token");
            StaticAuthentication auth = new StaticAuthentication(token);
            StravaClient client = new StravaClient(auth);
            Athlete athlete = await client.Athletes.GetAthleteAsync();

            var data = new 
            {
                name = athlete.FirstName,
                pic = athlete.Profile
            };
            return Ok(data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> stravaCallback([FromQuery(Name = "state")] string state, [FromQuery(Name = "code")] string inCode, [FromQuery(Name = "scope")] string scope)
        {
            //todo use config file instead of hardcoded values
            var toPost = new Dictionary<string, string>
            {
               { "client_id", "25050" },
               { "client_secret", "7e75ba6e25b536520528d415c9dd1b5d735a5549" },
               { "code", inCode },
               { "grant_type", "authorization_code" }
            };
            
            var content = new FormUrlEncodedContent(toPost);

            var response = await client.PostAsync("https://www.strava.com/oauth/token", content);
            string responseString = await response.Content.ReadAsStringAsync();

            string access_token = responseString.Substring(122, 40);
            string userID = responseString.Substring(180,8);

            HttpContext.Session.SetString("access_token", access_token);


            return Redirect("/");

        }

    }
}
