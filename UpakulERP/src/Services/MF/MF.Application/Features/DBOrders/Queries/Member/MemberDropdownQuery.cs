using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Member
{
    public class MemberDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int? officeId { get; set; }
        public int? groupId { get; set; }
        public MemberDropdownQuery(int officeId,int? groupId)
        {
            this.officeId = officeId;
            this.groupId = groupId;
        }
    }
}
