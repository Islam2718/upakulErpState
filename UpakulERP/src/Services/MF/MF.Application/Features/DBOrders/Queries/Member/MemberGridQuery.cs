using MediatR;
using MF.Domain.Models.View;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.Member
{
    public class MemberGridQuery : IRequest<PaginatedResponse<VWmemberCommonData>>
    {
        public int logedInOfficeId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }

}
