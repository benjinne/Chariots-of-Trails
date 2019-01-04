using System.Collections.Generic;
using System.Threading.Tasks;
using Chariots_of_Trails.Models;

namespace Chariots_of_Trails.Providers
{
    public interface IStravaProvider
    {
        Task<User> getUser(string state, string inCode,string scope);
        Task<List<Route>> getUserRoutes(User user);
    }
}
