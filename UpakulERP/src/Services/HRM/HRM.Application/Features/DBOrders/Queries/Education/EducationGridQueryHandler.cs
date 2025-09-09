using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Queries.Education
{
    public class EducationGridQueryHandler : IRequestHandler<EducationGirdQuery, PaginatedResponse<EducationVM>>
    {
        private readonly IEducationRepository _repository;

        public EducationGridQueryHandler(IEducationRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<EducationVM>> Handle(EducationGirdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(
                request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }

}
