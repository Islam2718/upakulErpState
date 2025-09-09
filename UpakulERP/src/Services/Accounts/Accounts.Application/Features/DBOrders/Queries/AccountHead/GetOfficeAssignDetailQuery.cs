using Accounts.Domain.ViewModel;
using MediatR;

namespace Accounts.Application.Features.DBOrders.Queries.AccountHead
{
    public class GetOfficeAssignDetailQuery: IRequest<List<HeadXOfficeAssignVM>>
    {
        public int? officeid { get; set; }
        public int? accountId { get; set; }
        public GetOfficeAssignDetailQuery(int? loggedinOfficeId, int? accountId)
        {
            officeid = loggedinOfficeId;
            this.accountId = accountId;
        }
    }
}
