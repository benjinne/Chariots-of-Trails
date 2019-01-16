using System;
using System.Collections.Generic;
using LiteDB;

namespace Chariots_of_Trails.Models
{
    public class Route
    {
        public string description { get; set; }
        public string distance { get; set; }
        public string miles { get{return (Math.Round(Convert.ToDouble(distance) * 0.000621371, 2) ).ToString();} }
        public string elevation_gain { get; set; }
        public string elevationFt { get{return (Math.Round(Convert.ToDouble(elevation_gain) * 3.28084)).ToString();} }
        public string id { get; set; }
        public Map map { get; set; }
        public string name  { get; set; }
        public string estimated_moving_time { get; set; }
        public bool suggested { get; set; }
        [BsonRef("athletes")]
        public List<Athlete> votedBy { get; set; } = new List<Athlete>();
        [BsonRef("athletes")]
        public Athlete suggestedBy { get; set; }
    }

    public class Map
    {
        public string id { get; set; }
        public string summary_polyline { get; set; } 
    }
}