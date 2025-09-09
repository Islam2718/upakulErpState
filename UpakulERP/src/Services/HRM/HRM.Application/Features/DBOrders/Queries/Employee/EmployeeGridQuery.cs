using HRM.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Queries.Department
{
    public class EmployeeGridQuery : IRequest<PaginatedResponse<EmployeeVM>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
        public int OfficeId {  get; set; }
    }

}
