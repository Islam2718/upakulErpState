using MediatR;
using MF.Domain.ViewModels.Collection;

namespace MF.Application.Features.DBOrders.Queries.Component
{
   public class GroupXMemberCollectionQuery : IRequest<List<GroupXMemberCollectionVM>>
    {
        public int officeId { get; set; }
        public int employeeId { get; set; }
        public DateTime transactionDate { get; set; }
        public int groupId { get; set; }

        public GroupXMemberCollectionQuery(int officeId, int employeeId, DateTime transactionDate, int groupId)
        {
            this.officeId = officeId;
            this.employeeId = employeeId;
            this.transactionDate = transactionDate;
            this.groupId = groupId;
        }
    }
}
