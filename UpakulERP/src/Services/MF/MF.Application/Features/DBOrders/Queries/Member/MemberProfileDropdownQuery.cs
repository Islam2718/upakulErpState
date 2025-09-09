using MediatR;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.Member
{
    public class MemberProfileDropdownQuery : IRequest<MultipleDropdownForMemberProfileVM>
    {
        public int _officeId { get; set; }
        public MemberProfileDropdownQuery(int officeId)
        {
            this._officeId = officeId;
        }
    }
}
