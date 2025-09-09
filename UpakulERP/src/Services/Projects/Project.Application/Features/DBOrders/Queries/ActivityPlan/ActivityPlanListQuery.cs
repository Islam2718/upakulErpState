using MediatR;
using Project.Domain.ViewModels;

namespace Project.Application.Features.DBOrders.Queries.ActivityPlan
{
    public class ActivityPlanListQuery:IRequest<List<RequestActivityPlan>>
    {
        public int projectId { get; set; }
        public ActivityPlanListQuery(int projectId)
        {
            this.projectId = projectId;
        }
    }
}
