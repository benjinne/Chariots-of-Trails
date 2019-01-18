using System;
using System.Collections.Generic;
using Chariots_of_Trails.Models;

namespace Chariots_of_Trails.Providers
{
    public interface IDataBaseProvider
    {
        bool userExists(User user); 
        void insertUser(User user);
        void updateUser(User user);
        void suggestRouteByRouteIdAndUserId(string id, string UserId);
        IEnumerator<Route> getSuggestedRoutes();
        User getUserById(string userId);
        Athlete getAthleteById(string athleteId);
        void upVoteByRouteIdAndUserId(string routeId, string userId);
        void downVoteByRouteIdAndUserId(string routeId, string userId);
        void logException(Exception ex);
    }
}