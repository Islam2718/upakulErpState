using MediatR;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateAccountHeadCommand : IRequest<CommadResponse>
    {
        public string HeadName { get; set; }
        public bool IsTransactable { get; set; }
        public int? ParentId { get; set; }
        public int? CreatedBy { get; set; }
    }
}
