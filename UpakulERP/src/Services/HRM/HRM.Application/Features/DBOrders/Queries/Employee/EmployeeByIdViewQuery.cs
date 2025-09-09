using HRM.Domain.Models.Views;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Employee
{
    public class EmployeeByIdViewQuery : IRequest<VWEmployee>
    {
        public int id { get; set; }
        public EmployeeByIdViewQuery(int id)
        {
            this.id = id;
        }
    }
}
