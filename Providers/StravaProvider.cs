using System;
using System.Collections.Generic;
using System.Linq;
using Chariots_of_Trails.Models;
using Microsoft.AspNetCore.Hosting;
using LiteDB;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Chariots_of_Trails.Providers
{
    public class StravaProvider : IStravaProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private static dynamic stravaAppConfig; //does not exist on github because it contains client_secret
        public static readonly HttpClient client = new HttpClient();

        public StravaProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            stravaAppConfig = JsonConvert.DeserializeObject(System.IO.File.ReadAllText("./Controllers/config.json"));
        }

        public RedirectResult login(Controller controller)
        {
            //     "https://www.strava.com/oauth/authorize?" +
            //     "client_id=" + config.client_id +
            //     "&redirect_uri=" + "http://" + window.location.hostname + ":5050/api/main/login" +
            //     "&response_type=" + config.response_type +
            //     "&approval_prompt=" + config.approval_prompt +
            //     "&scope=" + config.scope
            string authenticationString = 
            "https://www.strava.com/oauth/authorize?" + 
            $"client_id={stravaAppConfig.client_id}&" +
            $"redirect_uri=http://{controller.Request.Host}/api/main/recieveRedirect&" +
            $"response_type={stravaAppConfig.response_type}&" +
            $"approval_prompt={stravaAppConfig.approval_prompt}&" +
            $"scope={stravaAppConfig.scope}&";
            return controller.Redirect(authenticationString);
        }

        public async Task<User> getUser(string state, string inCode,string scope)
        {
            //todo save expires_at time and refresh token because user will get access_denied after 6 hours if they haven't re-logged in.
            var toPost = new Dictionary<string, string>
            {
                { "client_id", (string) stravaAppConfig.client_id },
                { "client_secret", (string) stravaAppConfig.client_secret },
                { "code", inCode },
                { "grant_type", "authorization_code" }
            };
            var content = new FormUrlEncodedContent(toPost);
            var response = await client.PostAsync("https://www.strava.com/oauth/token", content);
            string responseString = await response.Content.ReadAsStringAsync();
            return(JsonConvert.DeserializeObject<User>(responseString));
        }

        public async Task<List<Route>> getUserRoutes(User user)
        {
            string id = user.athlete.id.ToString();
            string token = user.access_token;
            string toGet = $"https://www.strava.com/api/v3/athletes/{id}/routes?page=1&per_page=50&access_token={token}";
            var responseString = await client.GetStringAsync(toGet);
            return(JsonConvert.DeserializeObject<List<Route>>(responseString));
        }
    }
}

        
