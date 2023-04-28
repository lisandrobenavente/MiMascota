using API.ViewModels;
using AutoMapper;
using Data.Dapper.Models;


namespace API.Infrastructure.Extensions
{
    public class AutoMapperConfigurations:Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<UserViewModel, Users>().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
