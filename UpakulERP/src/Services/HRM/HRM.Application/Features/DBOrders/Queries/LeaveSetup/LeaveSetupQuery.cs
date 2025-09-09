using MediatR;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.LeaveSetup
{
    public class LeaveSetupQuery : IRequest<List<CustomSelectListItem>>
    {
        public int pid { get; set; }
        public LeaveSetupQuery(int? pid)
        {
            this.pid = pid ?? 0;
        }
    }
}