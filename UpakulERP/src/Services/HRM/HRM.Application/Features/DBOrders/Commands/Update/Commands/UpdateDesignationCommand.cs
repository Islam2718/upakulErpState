using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Commands
{
    public class UpdateDesignationCommand : IRequest<CommadResponse>
    {
        public int DesignationId { get; set; }
        public string? DesignationCode { get; set; }
        public string DesignationName { get; set; }
        public int OrderNo { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    }
}
