using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.ApiSourceClient
{
    public interface IUsersApiClient
    {
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
    }
}