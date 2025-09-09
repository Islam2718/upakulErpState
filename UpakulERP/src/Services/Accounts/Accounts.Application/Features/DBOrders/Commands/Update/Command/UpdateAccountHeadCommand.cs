using MediatR;
using Utility.Response;
namespace Accounts.Application.Features.DBOrders.Commands.Update.Commands
{
    public class UpdateAccountHeadCommand : IRequest<CommadResponse>
    {
        public int AccountId { get; set; }
        public string HeadName { get; set; }
        public bool IsTransactable { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
