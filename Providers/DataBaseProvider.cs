using System;
using System.Collections;
using System.Collections.Generic;
using Chariots_of_Trails.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Chariots_of_Trails.Providers
{
    public class DataBaseProvider : IDataBaseProvider
    {
        private readonly IMongoCollection<User> users;
        private readonly IMongoCollection<Athlete> athletes;
        private readonly IMongoCollection<Route> routes;
        private readonly IMongoCollection<Map> maps;

        public DataBaseProvider(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("ycproutes"));
            var database = client.GetDatabase("ycproutes");
            users = database.GetCollection<User>("Users");
            athletes = database.GetCollection<Athlete>("Athletes");
            routes = database.GetCollection<Route>("Routes");
            maps = database.GetCollection<Map>("Maps");
        }

        public bool userExists(User user)
        {
            return users.Find(test => test.id == user.id).CountDocuments() >= 1;
        }
        
        public bool routeExists(Route route)
        {
            return routes.Find(test => test.id == route.id).CountDocuments() >= 1;
        }

        public void insertUser(User user)
        {
            users.InsertOne(user);
            athletes.InsertOne(user.athlete);
        }

        public void updateUser(User user)
        {
            users.ReplaceOne(test => test.id == user.id, user);
            foreach(Route route in user.routes)
            {
                if(routeExists(route))
                {
                    routes.Update(route);
                }
                else
                {
                    routes.Insert(route);
                }
            }
        }
        public User getUserById(string userId)
        {
            var users = db.GetCollection<User>("users");
            User user = users.IncludeAll()
                             .FindOne(Query.EQ("_id", userId));
            return user;
        }
        public Athlete getAthleteById(string athleteId)
        {
            var athletes = db.GetCollection<Athlete>("athletes");
            Athlete athlete = athletes.FindOne(Query.EQ("_id", athleteId));
            return athlete;
        }

        public void suggestRouteByRouteIdAndUserId(string routeId, string UserId)
        {
            var routes = db.GetCollection<Route>("routes");
            var route = routes.FindOne(Query.EQ("_id", routeId));
            route.suggested = true;
            route.suggestedBy = getAthleteById(UserId);
            routes.Update(route);
        }

        public IEnumerator<Route> getSuggestedRoutes()
        {
            var routes = db.GetCollection<Route>("routes");
            IEnumerator<Route> suggestedRoutes = routes.IncludeAll()
                                                       .Find(Query.EQ("suggested", true)).GetEnumerator();
            return(suggestedRoutes);
        }

        /// <summary>
        /// only allows the voter to vote once
        /// </summary>
        /// <param name="routeId">strava's routeId</param>
        /// <param name="userId">strava's userId or athleteId</param>
        public void upVoteByRouteIdAndUserId(string routeId, string userId)
        {
            var routes = db.GetCollection<Route>("routes");
            Route votedRoute = routes.FindOne(Query.EQ("_id", routeId));
            //this prevents a user from voting twice on a route
            if(!votedRoute.votedBy.Exists(x => x.id == userId))
            {
                votedRoute.votedBy.Add(getAthleteById(userId));
            }
            routes.Update(votedRoute);
        }

        /// <summary>
        /// only allows the voter to downvote if they already voted
        /// </summary>
        /// <param name="routeId">strava's routeId</param>
        /// <param name="userId">strava's userId or athleteId</param>
        public void downVoteByRouteIdAndUserId(string routeId, string userId)
        {
            var routes = db.GetCollection<Route>("routes");
            Route votedRoute = routes.Include(x => x.votedBy)
                                     .FindOne(Query.EQ("_id", routeId));
            //this prevents a user from downvoting twice
            if(votedRoute.votedBy.Exists(x => x.id == userId))
            {
                votedRoute.votedBy.Remove(votedRoute.votedBy.Find(x => x.id == userId));
            }
            routes.Update(votedRoute);
        }

        public void logException(Exception ex)
        {
            var col = db.GetCollection<ExceptionLog>("log");
            ExceptionLog exceptionLog = new ExceptionLog
            {
                stackTrace = $"{ex.Message}\n{ex.StackTrace}"
            };
            col.Insert(exceptionLog);
        }

        public void log(String message)
        {
            var col = db.GetCollection<ExceptionLog>("log");
            ExceptionLog exceptionLog = new ExceptionLog
            {
                stackTrace = message
            };
            col.Insert(exceptionLog);
        }
    }
}