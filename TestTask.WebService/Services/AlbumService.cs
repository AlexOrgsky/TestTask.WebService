using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;
using TestTask.ApiSourceClient;

namespace TestTask.WebService.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumsApiClient apiClient;

        public AlbumService(IAlbumsApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        
        public async Task<IEnumerable<Album>> GetAllAlbumsAsync()
        {
            return await apiClient.GetAlbums();
        }

        public async Task<Album> GetAlbumByIdAsync(int id)
        {
            return await apiClient.GetAlbumById(id);
        }

        public async Task<IEnumerable<Album>> GetAlbumsByUserIdAsync(int userId)
        {
            return await apiClient.GetAlbumsByUserId(userId);
        }

        
    }
}
