using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Designation
{
    public class DesignationByIdQuery : IRequest<DesignationVM>
    {
        public int id { get; set; }
        public DesignationByIdQuery(int id)
        {
            this.id = id;
        }
    }

}
