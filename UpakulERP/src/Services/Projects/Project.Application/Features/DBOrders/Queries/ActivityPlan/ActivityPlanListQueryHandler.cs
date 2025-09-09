using AutoMapper;
using MediatR;
using Project.Application.Contacts.Persistence;
using Project.Domain.ViewModels;

namespace Project.Application.Features.DBOrders.Queries.ActivityPlan
{
    public class ActivityPlanListQueryHandler : IRequestHandler<ActivityPlanListQuery, List<RequestActivityPlan>>
    {
        IActivityPlanRepository _repository;
        IMapper _mapper;
        public ActivityPlanListQueryHandler(IActivityPlanRepository repository, IMapper mapper) 
        {
        _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<RequestActivityPlan>> Handle(ActivityPlanListQuery request, CancellationToken cancellationToken)
        {
           var lst= _repository.GetProjectXActivity(request.projectId);
            var rtn_lst=_mapper.Map<List<RequestActivityPlan>>(lst);
            return rtn_lst;
        }
    }
}
