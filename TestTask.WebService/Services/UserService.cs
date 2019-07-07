using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.ApiSourceClient;
using TestTask.Models;

namespace TestTask.WebService.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersApiClient apiClient;
        public UserService(IUsersApiClient apiClient)
        {
            this.apiClient = apiClient;
        }


        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await apiClient.GetUsers();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await apiClient.GetUserById(id);
        }
    }
}
