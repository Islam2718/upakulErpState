using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Employee
{
    public class EmployeeDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int employeeId {  get; set; }
        public int officeId {  get; set; }

        public EmployeeDropdownQuery(int? employeeId,int? officeId)
        {
            this.employeeId = employeeId ?? 0;
            this.officeId = officeId ?? 0;
        }
    }
}
