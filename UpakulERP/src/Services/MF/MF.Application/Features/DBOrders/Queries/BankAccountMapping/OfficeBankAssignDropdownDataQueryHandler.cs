using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{

    public class OfficeBankAssignDropdownDataQueryHandler : IRequestHandler<OfficeBankAssignDropdownDataQuery, OfficeBankAssignDropdownVM>
    {
        public IBankAccountMappingRepository _repository;
        public OfficeBankAssignDropdownDataQueryHandler(IBankAccountMappingRepository repository)
        {
            _repository = repository;
        }

        public async Task<OfficeBankAssignDropdownVM> Handle(OfficeBankAssignDropdownDataQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetOfficeBankAssignDropdownData(request._officeId);
        }
    }


}
