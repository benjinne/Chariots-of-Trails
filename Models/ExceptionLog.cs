using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chariots_of_Trails.Models
{
    public class ExceptionLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string time { get{return DateTime.Now.ToString();} }
        
        [BsonElement("stackTrace")]
        public string stackTrace { get; set; }


    }
}