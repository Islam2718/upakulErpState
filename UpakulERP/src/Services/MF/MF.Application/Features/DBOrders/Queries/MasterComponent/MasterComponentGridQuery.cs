using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.MasterComponent
{
    public class MasterComponentGridQuery : IRequest<PaginatedResponse<MasterComponentVM>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}
