using AutoMapper;
using TestTask.Models;
using TestTask.WebService.ViewModels;

namespace TestTask.WebService.Mappers
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Album, AlbumViewModel>();
        }
    }
}

