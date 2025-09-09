using MF.Application.Contacts.Persistence;
using MF.Domain.Models.View;
using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.GraceSchedule
{
    public class GraceScheduleGridQueryHandler : IRequestHandler<GraceScheduleGridQuery, PaginatedResponse<VWGraceSchedule>>
    {
        private readonly IGraceScheduleRepository _repository;

        public GraceScheduleGridQueryHandler(IGraceScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<VWGraceSchedule>> Handle(GraceScheduleGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(
                request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}

