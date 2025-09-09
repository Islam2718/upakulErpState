using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Department
{
    public class DepartmentByIdQuery : IRequest<DepartmentVM>
    {
        public int id { get; set; }
        public DepartmentByIdQuery(int id)
        {
            this.id = id;
        }
    }

}
