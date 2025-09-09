using AutoMapper;
using MediatR;
using Project.Application.Contacts.Persistence;
using Project.Domain.ViewModels;

namespace Project.Application.Features.DBOrders.Queries.Project
{
    public class ProjectByIdHandler : IRequestHandler<ProjectByIdQuery, ProjectVM>
    {
        IProjectRepository _repository;
        IMapper _mapper;
        public ProjectByIdHandler(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProjectVM> Handle(ProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<ProjectVM>(obj);
        }
    }
}
