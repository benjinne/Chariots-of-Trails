using System;
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
            var users = db.GetCollection<User>("users");
            return(users.Exists(Query.EQ("_id", user.id)));
        }
        public bool routeExists(Route route)
        {
            var routes = db.GetCollection<Route>("routes");
            return(routes.Exists(Query.EQ("_id", route.id)));
        }

        public void insertUser(User user)
        {
            var users = db.GetCollection<User>("users");
            var athletes = db.GetCollection<Athlete>("athletes");
            users.Insert(user);
            athletes.Insert(user.athlete);
        }

        public void updateUser(User user)
        {
            var users = db.GetCollection<User>("users");
            var routes = db.GetCollection<Route>("routes");
            users.Update(user);
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
            User user = users.Include(x => x.routes)
                             .Include(x => x.athlete)
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
            IEnumerator<Route> suggestedRoutes = routes.Include(x => x.votedBy)
                                                       .Include(x => x.suggestedBy)
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
                stackTrace = ex.StackTrace
            };
            col.Insert(exceptionLog);
        }
    }
}