using System.Collections;
using System.Collections.Generic;
using Chariots_of_Trails.Models;
using LiteDB;
using Microsoft.AspNetCore.Hosting;

namespace Chariots_of_Trails.Providers
{
    public class DataBaseProvider : IDataBaseProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private LiteDatabase db;
        public DataBaseProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            db = new LiteDatabase("Data.db");
        }

        public bool userExists(User user)
        {
            var col = db.GetCollection<User>("users");
            return(col.Exists(Query.EQ("_id", user.id)));
        }

        public void insertUser(User user)
        {
            var users = db.GetCollection<User>("users");
            users.Insert(user);
        }

        public void updateUser(User user)
        {
            var col = db.GetCollection<User>("users");
            col.Update(user);
        }

        public User getUserById(string id)
        {
            var col = db.GetCollection<User>("users");
            User user = col.FindOne(Query.EQ("_id", id));
            return user;
        }

        public void suggestRouteByRouteId(string id)
        {
            var col = db.GetCollection<User>("users");
            var user = col.FindOne(Query.EQ("$.routes[*]._id", id));
            user.routes.Find(x => x.id == id).suggested = true;
            col.Update(user);
        }

        public List<Route> getSuggestedRoutes()
        {
            var col = db.GetCollection<User>("users");
            IEnumerator<User> usersWithSuggestedRoutes = col.Find(Query.EQ("$.routes[*].suggested", true)).GetEnumerator();
            List<Route> suggestedRoutes = new List<Route>();
            while(usersWithSuggestedRoutes.MoveNext())
            {
                if(usersWithSuggestedRoutes.Current.routes != null)
                {
                    suggestedRoutes.AddRange(usersWithSuggestedRoutes.Current.routes.FindAll(x => x.suggested == true));
                }
            }
            return(suggestedRoutes);
        }
    }
}