using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PathSystemServer.DTOs.Auth;
using PathSystemServer.ViewModels.Auth;

namespace PathSystemServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterDTO>();
            CreateMap<LoginSuccessDTO, LoginSuccessViewModel>();
            CreateMap<LoginViewModel, LoginDTO>();
        }
    }
}