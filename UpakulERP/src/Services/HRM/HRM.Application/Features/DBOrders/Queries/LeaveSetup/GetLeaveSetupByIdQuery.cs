using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.LeaveSetup
{
    public class LeaveSetupByIdQuery : IRequest<LeaveSetupVM>
    {
        public int id { get; set; }
        public LeaveSetupByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
