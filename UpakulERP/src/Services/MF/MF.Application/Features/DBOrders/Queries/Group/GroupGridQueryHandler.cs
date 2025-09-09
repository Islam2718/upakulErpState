using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models.View;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.Group
{
    public class GroupGridQueryHandler : IRequestHandler<GroupGridQuery, PaginatedResponse<VwGroup>>
    {
        private readonly IGroupRepository _repository;

        public GroupGridQueryHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<VwGroup>> Handle(GroupGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder, request.OfficeId);
        }
    }

}
