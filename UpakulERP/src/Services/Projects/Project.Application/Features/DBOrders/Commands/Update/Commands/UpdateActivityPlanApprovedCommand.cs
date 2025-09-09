using MediatR;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Commands.Update.Commands
{
    public class UpdateActivityPlanApprovedCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
