using MediatR;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.EmployeeStatus
{
    public class EmployeeStatusQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public EmployeeStatusQuery(int? id)
        {
            this.id = id ?? 0;
        }
    }
}
