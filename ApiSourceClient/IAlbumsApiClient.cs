using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.ApiSourceClient
{
    public interface IAlbumsApiClient
    {
        Task<Album> GetAlbumById(int id);
        Task<IEnumerable<Album>> GetAlbums();
        Task<IEnumerable<Album>> GetAlbumsByUserId(int userId);
    }
}