using Global.Application.Contacts.Persistence;
using Global.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Queries.Bank
{
    public class BankGridQueryHandler : IRequestHandler<BankGridQuery, PaginatedResponse<BankVM>>
    {
        private readonly IBankRepository _BankRepository;

        public BankGridQueryHandler(IBankRepository BankRepository)
        {
            _BankRepository = BankRepository;
        }

        public async Task<PaginatedResponse<BankVM>> Handle(BankGridQuery request, CancellationToken cancellationToken)
        {
            return await _BankRepository.LoadGrid(
                request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}
