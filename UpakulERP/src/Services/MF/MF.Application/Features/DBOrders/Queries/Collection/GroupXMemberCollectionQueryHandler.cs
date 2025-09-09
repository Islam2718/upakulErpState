using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels.Collection;

namespace MF.Application.Features.DBOrders.Queries.Component
{
    public class GroupXMemberCollectionQueryHandler : IRequestHandler<GroupXMemberCollectionQuery, List<GroupXMemberCollectionVM>>
    {
        ICollectionRepository _repository;
        public GroupXMemberCollectionQueryHandler(ICollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GroupXMemberCollectionVM>> Handle(GroupXMemberCollectionQuery request, CancellationToken cancellationToken)
        {
            var obj = await _repository.GroupXMemberCollectionInfo(request.officeId, request.employeeId, request.transactionDate, request.groupId);
            return obj;
        }
    }
}
