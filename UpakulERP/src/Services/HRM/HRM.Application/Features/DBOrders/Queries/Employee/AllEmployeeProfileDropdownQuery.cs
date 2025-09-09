using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Employee
{
    public class AllEmployeeProfileDropdownQuery : IRequest<MultipleDropdownForEmployeeProfileVM>
    {
        public int _officeId { get; set; }
        public AllEmployeeProfileDropdownQuery(int officeId)
        {
            this._officeId = officeId;
        }
    }
}
