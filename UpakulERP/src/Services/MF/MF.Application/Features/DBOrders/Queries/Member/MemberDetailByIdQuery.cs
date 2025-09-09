using MediatR;
using MF.Domain.Models.View;

namespace MF.Application.Features.DBOrders.Queries.Member
{
    public class MemberDetailByIdQuery : IRequest<VWMember>
    {
        public int id { get; set; }
        public MemberDetailByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
