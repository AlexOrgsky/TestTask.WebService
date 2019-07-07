using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.LoggerService;
using TestTask.Models;

namespace TestTask.ApiSourceClient
{
    public class ApiClient : IUsersApiClient, IAlbumsApiClient
    {
        public ApiClient(ILoggerManager logger)
        {
            this.logger = logger;
            restClient = new RestClient("http://jsonplaceholder.typicode.com/");
        }

        ILoggerManager logger;
        RestClient restClient;

        public async Task<IEnumerable<User>> GetUsers()
        {
            var request = new RestRequest("users", Method.GET, DataFormat.Json);
            var response = await restClient.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                logger?.LogError($"External API: Users not found. StatusCode:" + response.StatusCode + " Description: " + response.StatusDescription);
                return null;
            }

            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(response.Content);
            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            var request = new RestRequest($"users/{id}", Method.GET, DataFormat.Json);
            var response = await restClient.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                logger?.LogError($"External API: User with id: {id} not found. StatusCode:" + response.StatusCode + " Description: " + response.StatusDescription);
                return null;
            }

            var user = JsonConvert.DeserializeObject<User>(response.Content);
            return user;
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            var request = new RestRequest("albums", Method.GET, DataFormat.Json);
            var response = await restClient.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                logger?.LogError($"External API: Albums not found. StatusCode:" + response.StatusCode + " Description: " + response.StatusDescription);
                return null;
            }

            var albums = JsonConvert.DeserializeObject<IEnumerable<Album>>(response.Content);
            return albums;
        }

        public async Task<Album> GetAlbumById(int id)
        {
            var request = new RestRequest($"albums/{id}", Method.GET, DataFormat.Json);
            var response = await restClient.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                logger?.LogError($"External API: Album with id: {id} not found. StatusCode:" + response.StatusCode + " Description: " + response.StatusDescription);
                return null;
            }

            var album = JsonConvert.DeserializeObject<Album>(response.Content);
            return album;
        }

        public async Task<IEnumerable<Album>> GetAlbumsByUserId(int userId)
        {
            var request = new RestRequest($"albums?userId={userId}", Method.GET, DataFormat.Json);
            var response = await restClient.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                logger?.LogError($"External API: Albums with userId: {userId} not found. StatusCode:" + response.StatusCode + " Description: " + response.StatusDescription);
                return null;
            }

            var albums = JsonConvert.DeserializeObject<IEnumerable<Album>>(response.Content);
            return albums;
        }
    }
}
