using Accounts.Application.Contacts.Persistence.Voucher;
using Accounts.Domain.ViewModel;
using MediatR;

namespace Accounts.Application.Features.DBOrders.Queries.AccountHead
{
    public class GetAccountHeadQueryHandler : IRequestHandler<GetAccountHeadQuery, List<AccountHeadXChildVM>>
    {
        IAccountHeadRepository _repository;
        public GetAccountHeadQueryHandler(IAccountHeadRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<AccountHeadXChildVM>> Handle(GetAccountHeadQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAccountHeadDetails(request.pid, request.requestType);
        }
    }
}
