using MediatR;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.Designation
{
    public class DesignationQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public DesignationQuery(int? id)
        {
            this.id = id ?? 0;
        }
    }

}
