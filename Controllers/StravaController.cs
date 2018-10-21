using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chariots_of_Trails.Providers;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("[action]")]
        public IActionResult Users()
        {
            var test = stravaProvider.getUser();

            var result = new
            {
                thing = test
            };
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> StravaCallback([FromQuery(Name = "state")] string state, [FromQuery(Name = "code")] string inCode, [FromQuery(Name = "scope")] string scope)
        {

            var toPost = new Dictionary<string, string>
            {
               { "client_id", "25050" },
               { "client_secret", "7e75ba6e25b536520528d415c9dd1b5d735a5549" },
               { "code", inCode },
               { "grant_type", "authorization_code" }
            };
            
            var content = new FormUrlEncodedContent(toPost);

            var response = await client.PostAsync("https://www.strava.com/oauth/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            

            return Redirect("/");

        }

    }
}
