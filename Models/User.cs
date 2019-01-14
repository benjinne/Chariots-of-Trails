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
        [BsonRef("athletes")]
        public Athlete athlete { get; set; }
        [BsonRef("routes")]
        public List<Route> routes { get; set; } = new List<Route>();
    }
    public class Athlete
    {
        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname{ get; set; }
        public string fullname{ get{return $"{firstname} {lastname}";}}
        public string profile { get; set; }
    }
}