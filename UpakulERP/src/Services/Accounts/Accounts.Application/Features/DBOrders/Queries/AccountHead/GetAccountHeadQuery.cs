using Accounts.Domain.ViewModel;
using MediatR;
namespace Accounts.Application.Features.DBOrders.Queries.AccountHead
{
    public class GetAccountHeadQuery : IRequest<List<AccountHeadXChildVM>>
    {
        public int? pid { get; set; }
        public string requestType { get; set; }
        public GetAccountHeadQuery(int? pid, string requestType)
        {
            this.pid = pid;
            this.requestType = requestType;
        }
    }
}
