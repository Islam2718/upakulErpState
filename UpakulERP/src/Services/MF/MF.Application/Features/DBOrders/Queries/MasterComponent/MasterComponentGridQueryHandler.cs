using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.MasterComponent
{
  public  class MasterComponentGridQueryHandler : IRequestHandler<MasterComponentGridQuery, PaginatedResponse<MasterComponentVM>>
    {
        private readonly IMasterComponentRepository _repository;

        public MasterComponentGridQueryHandler(IMasterComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<MasterComponentVM>> Handle(MasterComponentGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}
