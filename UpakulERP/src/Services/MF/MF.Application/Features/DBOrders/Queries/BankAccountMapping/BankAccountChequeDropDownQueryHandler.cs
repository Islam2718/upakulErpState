using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class BankAccountChequeDropDownQueryHandler : IRequestHandler<BankAccountChequeDropDownQuery, List<CustomSelectListItem>>
    {
        private readonly IBankAccountMappingRepository _repository;
        public BankAccountChequeDropDownQueryHandler(IBankAccountMappingRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CustomSelectListItem>> Handle(BankAccountChequeDropDownQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ChequeDetailsDropdown(request.id);
        }
    }
}
