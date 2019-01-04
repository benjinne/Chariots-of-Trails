using System.Collections.Generic;
using LiteDB;

namespace Chariots_of_Trails.Models
{
    public class User
    {
        public string id { get{return athlete.id;} set{}}
        public int expires_at { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string access_token { get; set; }
        public Athlete athlete { get; set; }
        public List<Route> routes { get; set; }
    }
    public class Athlete
    {
        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname{ get; set; }
        public string profile { get; set; }
    }
}