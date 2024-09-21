using AutoMapper;
using dashboardManger.Models;
using dashboardManger.DTOs;

namespace dashboardManger.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //user
            CreateMap<User, UserDTO>()
                .ForMember(l => l.UserRole, opt => opt.MapFrom(src => ((UserRole)src.UserRoleId).ToString().Replace('_', ' ')))
                .ForMember(l => l.State, opt => opt.MapFrom(src => ((State)src.StateId).ToString()))
                .ForMember(l => l.Department, opt => opt.MapFrom(src => src.DepartmentId.HasValue
                    ? ((Department)src.DepartmentId.Value).ToString()
                    : string.Empty))
                .ForMember(l => l.FilterWord, opt => opt.MapFrom(src =>
                    $"{src.Username} {src.Email} {((UserRole)src.UserRoleId).ToString().Replace('_', ' ')} {((State)src.StateId).ToString()} ".ToLower() +
                    $"{(src.DepartmentId.HasValue ? ((Department)src.DepartmentId.Value).ToString() : string.Empty)}".ToLower()
                ));

            //property
            CreateMap<Property, PropertyDTO>()
                .ForMember(l => l.Type, opt => opt.MapFrom(src => Enum.GetName(typeof(PropertyType), src.TypeId)))
                .ForMember(l => l.StatusId, opt => opt.MapFrom(src => Enum.GetName(typeof(ProperyStatus), src.StatusId)))
                .ForMember(l => l.FilterWord, opt => opt.MapFrom(src =>
                   $"{src.Address} {(PropertyType)src.TypeId} {(ProperyStatus)src.StatusId}".ToLower()
                ));


        }
    }
}
