using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models.Views;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Queries.HoliDay
{
    public class HolidayGridQueryHandler : IRequestHandler<HolidayGridQuery, PaginatedResponse<VWHoliday>>
    {
        private readonly IHoliDayRepository _repository;

        public HolidayGridQueryHandler(IHoliDayRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<VWHoliday>> Handle(HolidayGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(
                request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}

