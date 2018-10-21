using System;
using System.Collections.Generic;
using System.Linq;
using Chariots_of_Trails.Models;
using Strava.Authentication;
using Microsoft.AspNetCore.Hosting;
using LiteDB;


namespace Chariots_of_Trails.Providers
{
    public class StravaProvider : IStravaProvider
    {
        private String user { get; set; }

        private readonly IHostingEnvironment _hostingEnvironment;

        public StravaProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

            // initDB();

            user = "Michael Scott";
        }

        public string getUser()
        {
            return user;
        }

        public string getTrails() 
        {
            return "";
        }

        // public string getImage(string userName)
        // {
        //     string imagePath = "";

        //     // Open database (or create if doesn't exist)
        //     using(var db = new LiteDatabase("Data.db"))
        //     {
        //         // Get a collection (or create, if doesn't exist)
        //         var col = db.GetCollection<User>("team");

        //         var resultNum = col.Find(Query.EQ("Name", userName)).Count();

        //         if( resultNum == 1 )
        //         {
        //             foreach(var x in col.Find(Query.EQ("Name", userName)))
        //             {
        //                 imagePath = x.ProfilePicture;
        //             }
        //         }
        //     }
            
        //     return imagePath;
        // }


        public void initDB()
        {
            string michaelPicture = _hostingEnvironment.ContentRootPath + "/images/michael-scott.jpg";

            // Open database (or create if doesn't exist)
            using(var db = new LiteDatabase("Data.db"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<User>("team");

                // Create your new runner instance
                var runner = new User
                { 
                    Name = "Michael Scott", 
                    ProfilePicture = michaelPicture
                };

                // Insert new runner document (Id will be auto-incremented)
                col.Insert(runner);
            }
        }
    }
}

        
