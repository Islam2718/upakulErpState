using MediatR;
using Project.Domain.ViewModels;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateActivityPlanCommand : IRequest<CommadResponse>
    {
        public int ProjectId {  get; set; }
        public List<RequestActivityPlan> lst { get; set; }
        public int loggedinEmpId { get; set; }
    }
}
