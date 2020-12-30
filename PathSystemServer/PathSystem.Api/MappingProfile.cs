using AutoMapper;
using PathSystem.Api.ViewModels.Auth;
using PathSystem.BLL.DTOs.Auth;
using PathSystem.BLL.DTOs.Routing;
using PathSystem.DAL.Models;

namespace PathSystem.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PathPoint, PathPointDTO>()
                .ForMember(dst => dst.Longitude, opt => opt.MapFrom(src => src.Point.X))
                .ForMember(dst => dst.Latitude, opt => opt.MapFrom(src => src.Point.Y));
            CreateMap<UserDTO, UserViewModel>();
            CreateMap<Route, RouteDTO>();
        }
    }
}