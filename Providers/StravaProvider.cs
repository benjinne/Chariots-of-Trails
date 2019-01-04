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

namespace Chariots_of_Trails.Providers
{
    public class StravaProvider : IStravaProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public static readonly HttpClient client = new HttpClient();

        public StravaProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<User> getUser(string state, string inCode,string scope){
            //todo save expires_at time and refresh token because user will get access_denied after 6 hours if they haven't re-logged in.
            dynamic config = JsonConvert.DeserializeObject(System.IO.File.ReadAllText("config.json"));
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
            return(JsonConvert.DeserializeObject<User>(responseString));
        }

        public async Task<List<Route>> getUserRoutes(User user){
            string id = user.athlete.id.ToString();
            string token = user.access_token;
            string toGet = $"https://www.strava.com/api/v3/athletes/{id}/routes?page=1&per_page=50&access_token={token}";
            var responseString = await client.GetStringAsync(toGet);
            return(JsonConvert.DeserializeObject<List<Route>>(responseString));
        }


    }
}

        
