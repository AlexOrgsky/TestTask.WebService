using AutoMapper;
using TestTask.Models;
using TestTask.WebService.ViewModels;

namespace TestTask.WebService.Mappers
{
    public class UserAuthorizedProfile : Profile
    {
        public UserAuthorizedProfile()
        {
            CreateMap<User, UserAuthorizedViewModel>();
        }
    }
}
