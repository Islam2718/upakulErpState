using HRM.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Queries.Education
{
    public class EducationGirdQuery : IRequest<PaginatedResponse<EducationVM>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }

}
