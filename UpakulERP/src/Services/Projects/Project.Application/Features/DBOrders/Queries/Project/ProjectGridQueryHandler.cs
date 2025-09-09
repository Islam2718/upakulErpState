using MediatR;
using Project.Application.Contacts.Persistence;
using Project.Domain.ViewModels;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Queries.Project
{
    public class ProjectGridQueryHandler : IRequestHandler<ProjectGridQuery, PaginatedResponse<ProjectVM>>
    {
        private readonly IProjectRepository _repository;

        public ProjectGridQueryHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<ProjectVM>> Handle(ProjectGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}
