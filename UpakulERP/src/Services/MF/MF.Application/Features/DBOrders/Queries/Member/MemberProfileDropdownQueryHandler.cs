using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.Member
{
    public class MemberProfileDropdownQueryHandler : IRequestHandler<MemberProfileDropdownQuery, MultipleDropdownForMemberProfileVM>
    {
        public IMemberRepository _repository;
        public MemberProfileDropdownQueryHandler(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<MultipleDropdownForMemberProfileVM> Handle(MemberProfileDropdownQuery request, CancellationToken cancellationToken)
        {
           return await _repository.AllMemberProfileDropDown(request._officeId);
        }
    }
}
