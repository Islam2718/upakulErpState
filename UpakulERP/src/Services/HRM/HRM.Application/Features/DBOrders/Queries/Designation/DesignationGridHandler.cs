using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Queries.Designation
{
    public class DesignationGridHandler : IRequestHandler<DesignationGridQuery, PaginatedResponse<DesignationVM>>
    {
        private readonly IDesignationRepository _repository;

        public DesignationGridHandler(IDesignationRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<DesignationVM>> Handle(DesignationGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(
                request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}
