using Auth.API.Mapper;
using System.Reflection;

namespace Auth.API.Extensions
{
    public static class MappingProfileDependencyInjection
    {
        public static void AddMappingProfileDependencyInjection(this IServiceCollection service) =>
        service.AddAutoMapper(cfg=> { },typeof(MappingTheProfile).Assembly);
    }
}
