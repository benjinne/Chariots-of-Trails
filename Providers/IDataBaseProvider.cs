using System.Collections.Generic;
using Chariots_of_Trails.Models;

namespace Chariots_of_Trails.Providers
{
    public interface IDataBaseProvider
    {
        bool userExists(User user); 
        void insertUser(User user);
        void updateUser(User user);
        void suggestRouteByRouteId(string id);
        List<Route> getSuggestedRoutes();
        User getUserById(string token);
    }
}