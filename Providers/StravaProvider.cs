using System;
using System.Collections.Generic;
using System.Linq;
using Chariots_of_Trails.Models;
using Strava.Authentication;


namespace Chariots_of_Trails.Providers
{
    public class StravaProvider : IStravaProvider
    {
        private String user { get; set; }

        public StravaProvider()
        {
            user = "TEST";
        }

        public string getUser()
        {
            return user;
        }
    }
}
