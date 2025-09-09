using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Queries.Education;
using HRM.Domain.ViewModels;
using MediatR;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.Employee
{
    public class AllEmployeeProfileDropdownQueryHandler: IRequestHandler<AllEmployeeProfileDropdownQuery, MultipleDropdownForEmployeeProfileVM>
    {
        public IEmployeeRepository _repository;
        public AllEmployeeProfileDropdownQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<MultipleDropdownForEmployeeProfileVM> Handle(AllEmployeeProfileDropdownQuery request, CancellationToken cancellationToken)
        {
           return await _repository.AllEmployeeProfilesDropDown(request._officeId);
        }
    }
}
