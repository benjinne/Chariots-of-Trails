using System;
using LiteDB;

namespace Chariots_of_Trails.Models
{
    public class ExceptionLog
    {
        public string stackTrace { get; set; }
        [BsonId(false)]
        public string time { get{return DateTime.Now.ToString();} }

    }
}