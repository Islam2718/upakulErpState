using MediatR;
using roject.Domain.ViewModels;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Queries.Doner
{
    public class DonerGridQuery : IRequest<PaginatedResponse<DonerVM>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}
