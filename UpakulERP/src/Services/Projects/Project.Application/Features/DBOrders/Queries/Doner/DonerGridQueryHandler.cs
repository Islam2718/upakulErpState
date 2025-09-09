using MediatR;
using Project.Application.Contacts.Persistence;
using roject.Domain.ViewModels;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Queries.Doner
{
    public class DonerGridQueryHandler : IRequestHandler<DonerGridQuery, PaginatedResponse<DonerVM>>
    {
        private readonly IDonerRepository _repository;

        public DonerGridQueryHandler(IDonerRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<DonerVM>> Handle(DonerGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}
