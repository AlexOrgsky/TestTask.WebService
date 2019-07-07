using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.WebService.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
    }
}