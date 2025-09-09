using Auth.API.DTO.Request;
using Auth.API.DTO.Response;
using Auth.API.Models;
using AutoMapper;

namespace Auth.API.Mapper
{
    public class MappingTheProfile : Profile
    {
        public MappingTheProfile()
        {
            CreateMap<ApplicationUser, UserDtoResponse>().ReverseMap();
            CreateMap<ApplicationUser, LoginDtoResponse>().ReverseMap();

            CreateMap<ApplicationRole, RoleDtoResponse>().ReverseMap();
            CreateMap<ApplicationRole, RoleDtoResponse>().ReverseMap();
        }
    }
}
