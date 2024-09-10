using AutoMapper;
using dashboardManger.Models;
using dashboardManger.DTOs;

namespace dashboardManger.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(l => l.UserRole, opt => opt.MapFrom(src => ((UserRole)src.UserRoleId).ToString()))
                .ForMember(l => l.State, opt => opt.MapFrom(src => ((State)src.StateId).ToString()))
                .ForMember(l => l.Department, opt => opt.MapFrom(src => ((Department)src.DepartmentId).ToString()));
        }
    }
}
