using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chariots_of_Trails.Providers;

namespace Chariots_of_Trails.Controllers
{
    [Route("api/[controller]")]
    public class StravaController : Controller
    {
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

    }
}
