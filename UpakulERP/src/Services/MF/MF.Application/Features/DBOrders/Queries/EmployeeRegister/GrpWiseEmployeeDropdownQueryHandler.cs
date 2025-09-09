using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.EmployeeRegister
{

    public class GrpWiseEmployeeDropdownQueryHandler : IRequestHandler<GrpWiseEmployeeDropdownQuery, MultipleDropdownForGrpWiseEmployeeVM>
    {
        public IGroupWiseEmployeeAssignRepository _repository;
        public GrpWiseEmployeeDropdownQueryHandler(IGroupWiseEmployeeAssignRepository repository)
        {
            _repository = repository;
        }

        public async Task<MultipleDropdownForGrpWiseEmployeeVM> Handle(GrpWiseEmployeeDropdownQuery request, CancellationToken cancellationToken)
        {
            return await _repository.AllDropDownForGrpWiseEmployee(request._officeId, request._officeTypeId);
        }
    }


}
