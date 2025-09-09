using MF.Domain.Models.View;
using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.GraceSchedule

{
    public class GraceScheduleGridQuery : IRequest<PaginatedResponse<VWGraceSchedule>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}

