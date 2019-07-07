using AutoMapper;
using TestTask.Models;
using TestTask.WebService.ViewModels;

namespace TestTask.WebService.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User.Address, UserViewModel.Address>();
            CreateMap<User.Address.Geo, UserViewModel.Address.Geo>();
            CreateMap<User.Company, UserViewModel.Company>();

        }
    }
}
