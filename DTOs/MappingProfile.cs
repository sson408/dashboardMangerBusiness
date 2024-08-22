using AutoMapper;
using dashboardManger.Models;
using dashboardManger.DTOs;

namespace dashboardManger.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
