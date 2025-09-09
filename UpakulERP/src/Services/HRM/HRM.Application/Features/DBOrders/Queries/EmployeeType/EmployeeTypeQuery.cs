using MediatR;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.EmployeeType
{
   public class EmployeeTypeQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public EmployeeTypeQuery(int? id)
        {
            this.id = id ?? 0;
        }
    }
}
