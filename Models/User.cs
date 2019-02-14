using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chariots_of_Trails.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get{return athlete.id;} set{}}

        [BsonElement("expires_at")]
        public int expires_at { get; set; }

        [BsonElement("expires_in")]
        public int expires_in { get; set; }

        [BsonElement("refresh_token")]
        public string refresh_token { get; set; }

        [BsonElement("access_token")]
        public string access_token { get; set; }

        [BsonElement("athlete")]
        public Athlete athlete { get; set; }

        [BsonElement("routes")]
        public List<Route> routes { get; set; } = new List<Route>();
    }
    public class Athlete
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("firstname")]
        public string firstname { get; set; }

        [BsonElement("lastname")]
        public string lastname{ get; set; }

        [BsonElement("fullname")]
        public string fullname{ get{return $"{firstname} {lastname}";}}
        
        [BsonElement("profile")]
        public string profile { get; set; }
    }
}