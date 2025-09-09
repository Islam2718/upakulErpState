using MediatR;
using Project.Domain.ViewModels;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Queries.Project
{

    public class ProjectGridQuery : IRequest<PaginatedResponse<ProjectVM>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}
