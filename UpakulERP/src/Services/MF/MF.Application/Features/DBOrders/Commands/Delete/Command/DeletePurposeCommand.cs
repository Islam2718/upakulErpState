using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Command
{
    public class DeletePurposeCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = false;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }=DateTime.Now;
    }
}
