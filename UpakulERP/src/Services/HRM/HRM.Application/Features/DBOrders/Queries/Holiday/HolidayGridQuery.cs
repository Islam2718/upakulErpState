using HRM.Domain.Models.Views;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Queries.HoliDay
{
    public class HolidayGridQuery : IRequest<PaginatedResponse<VWHoliday>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}

