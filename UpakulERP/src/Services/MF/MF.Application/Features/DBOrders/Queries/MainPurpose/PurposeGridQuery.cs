using MediatR;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.MainPurpose
{
    public class PurposeGridQuery : IRequest<PaginatedResponse<PurposeForGridVM>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}
