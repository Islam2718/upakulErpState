
using MediatR;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Delete.Command
{
    public class DeleteAccountHeadCommand : IRequest<CommadResponse>
    {
        public int AccountId { get; set; }
        public int? DeletedBy { get; set; }
    }
}
