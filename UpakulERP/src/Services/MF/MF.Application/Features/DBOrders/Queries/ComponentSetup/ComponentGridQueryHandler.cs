using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Queries.ComponentSetup;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.Component
{
  public  class ComponentGridQueryHandler : IRequestHandler<ComponentGridQuery, PaginatedResponse<ComponentVM>>
    {
        private readonly IComponentRepository _repository;

        public ComponentGridQueryHandler(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<ComponentVM>> Handle(ComponentGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}
