using AutoMapper;
using Project.Application.Features.DBOrders.Commands.Create.Commands;
using Project.Application.Features.DBOrders.Commands.Delete.Commands;
using Project.Application.Features.DBOrders.Commands.Update.Commands;
using Project.Domain.Models;
using Project.Domain.ViewModels;
using roject.Domain.ViewModels;

namespace Project.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {           

            CreateMap<Doner, CreateDonerCommand>().ReverseMap();
            CreateMap<Doner, UpdateDonerCommand>().ReverseMap();
            CreateMap<Doner, DeleteDonerCommand>().ReverseMap();
            CreateMap<Doner, DonerVM>().ReverseMap();


            CreateMap<Projects, CreateProjectCommand>().ReverseMap();
            CreateMap<Projects, UpdateProjectCommand>().ReverseMap();
            CreateMap<Projects, DeleteProjectCommand>().ReverseMap();
            CreateMap<Projects, ProjectVM>().ReverseMap();

            CreateMap<ActivityPlan, ActivityPlanVM>().ReverseMap();
            CreateMap<ActivityPlan, RequestActivityPlan>().ReverseMap();

        }
    }
}
