namespace Chariots_of_Trails.Models
{
    public class Route
    {
        public string description { get; set; }
        public string distance { get; set; }
        public string elevation_gain { get; set; }
        public string id { get; set; }
        public Map map { get; set; }
        public string name  { get; set; }
        public string estimated_moving_time { get; set; }
        public bool suggested { get; set; }
    }

    public class Map
    {
        public string id { get; set; }
        public string summary_polyline { get; set; } 
    }
}