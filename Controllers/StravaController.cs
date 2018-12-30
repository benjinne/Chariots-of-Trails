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
        public async Task<IActionResult> routes()
        {
            //todo handle token expired exception
            string token = HttpContext.Session.GetString("access_token");
            StaticAuthentication auth = new StaticAuthentication(token);
            StravaClient client = new StravaClient(auth);
            
            Athlete athlete = await client.Athletes.GetAthleteAsync();
            List<Strava.Routes.Route> routes = client.Routes.GetRoutes(athlete.Id);

            System.Collections.ArrayList polylines = new System.Collections.ArrayList();
            foreach(Strava.Routes.Route _route in routes){
                polylines.Add(new {
                    name = _route.Name,
                    route = _route.Map.SummaryPolyline
                });
            }
            return Ok(polylines);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> stravaCallback([FromQuery(Name = "state")] string state, [FromQuery(Name = "code")] string inCode, [FromQuery(Name = "scope")] string scope)
        {
            //save expires_at time and refresh token because user will get access_denied after 6 hours if they haven't re-logged in.
            using (StreamReader r = new StreamReader("config.json"))
            {
                string json = r.ReadToEnd();
                dynamic config = JsonConvert.DeserializeObject(json);
                var toPost = new Dictionary<string, string>
                {
                    { "client_id", (string) config.client_id },
                    { "client_secret", (string) config.client_secret },
                    { "code", inCode },
                    { "grant_type", "authorization_code" }
                };
                var content = new FormUrlEncodedContent(toPost);
                var response = await client.PostAsync("https://www.strava.com/oauth/token", content);
                string responseString = await response.Content.ReadAsStringAsync();
                dynamic responseObj = JsonConvert.DeserializeObject(responseString);
                string access_token = responseObj.access_token;
                HttpContext.Session.SetString("access_token", access_token);
            }
            return Redirect("/");
        }
    }
}