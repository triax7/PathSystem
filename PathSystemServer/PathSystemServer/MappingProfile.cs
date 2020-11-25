using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PathSystemServer.DTOs.Auth;
using PathSystemServer.DTOs.Routing;
using PathSystemServer.Models;
using PathSystemServer.ViewModels.Auth;
using PathSystemServer.ViewModels.Routing;

namespace PathSystemServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterDTO>();
            CreateMap<LoginSuccessDTO, LoginSuccessViewModel>();
            CreateMap<LoginViewModel, LoginDTO>();
            CreateMap<RouteCreateViewModel, RouteDTO>().ReverseMap();
            CreateMap<RouteViewModel, RouteDTO>().ReverseMap();
            CreateMap<Route, RouteDTO>();
            CreateMap<AddPointViewModel, PathPointDTO>().ReverseMap();
            CreateMap<PathPoint, PathPointDTO>()
                .ForMember(dst => dst.Longitude, opt => opt.MapFrom(src => src.Point.X))
                .ForMember(dst => dst.Latitude, opt => opt.MapFrom(src => src.Point.Y));
            CreateMap<PathPointViewModel, PathPointDTO>().ReverseMap();
        }
    }
}