using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Employee
{
    public class EmployeeByIdQuery : IRequest<HRM.Domain.Models.Employee>
    {
        public int id { get; set; }
        public EmployeeByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
