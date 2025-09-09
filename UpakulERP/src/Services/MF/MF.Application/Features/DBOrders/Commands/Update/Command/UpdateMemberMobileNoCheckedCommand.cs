using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateMemberMobileNoCheckedCommand : IRequest<CommadResponse>
    {
        public int MemberId { get; set; }
       
        public string? OTPNo { get; set; }
        public bool? IsChecked { get; set; } = true;
        public int? CheckedBy { get; set; }
        public DateTime? CheckedDate { get; set; }=DateTime.Now;
    }
}
