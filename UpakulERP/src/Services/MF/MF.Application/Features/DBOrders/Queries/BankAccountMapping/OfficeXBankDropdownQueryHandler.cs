using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{

    public class OfficeXBankDropdownQueryHandler : IRequestHandler<OfficeXBankDropdownQuery, List<CustomSelectListItem>>
    {
        public IBankAccountMappingRepository _repository;
        public OfficeXBankDropdownQueryHandler(IBankAccountMappingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomSelectListItem>> Handle(OfficeXBankDropdownQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetOfficeXBankDropdown(request._officeId);
        }
    }


}
