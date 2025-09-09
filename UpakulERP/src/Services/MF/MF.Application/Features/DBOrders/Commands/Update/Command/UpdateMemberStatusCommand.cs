using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateMemberStatusCommand : IRequest<CommadResponse>
    {
        public int MemberId { get; set; }
        public bool IsApproved {  get; set; }
        public string? Note { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? RejectBy { get; set; }
        public DateTime? RejectDate { get; set; }

    }
}
