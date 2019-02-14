using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chariots_of_Trails.Models
{
    public class Route
    {
        [BsonElement("description")]
        public string description { get; set; }

        [BsonElement("distance")]
        public string distance { get; set; }

        [BsonElement("miles")]
        public string miles { get{return (Math.Round(Convert.ToDouble(distance) * 0.000621371, 2) ).ToString();} }

        [BsonElement("elevation_gain")]
        public string elevation_gain { get; set; }

        [BsonElement("elevationFt")]
        public string elevationFt { get{return (Math.Round(Convert.ToDouble(elevation_gain) * 3.28084)).ToString();} }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("map")]
        public Map map { get; set; }

        [BsonElement("Name")]
        public string name  { get; set; }

        [BsonElement("estimated_moving_time")]
        public string estimated_moving_time { get; set; }

        [BsonElement("suggested")]
        public bool suggested { get; set; }

        [BsonElement("votedBy")]
        public List<Athlete> votedBy { get; set; } = new List<Athlete>();

        [BsonElement("suggestedBy")]
        public Athlete suggestedBy { get; set; }
    }

    public class Map
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        
        [BsonElement("summary_polyline")]
        public string summary_polyline { get; set; } 
    }
}