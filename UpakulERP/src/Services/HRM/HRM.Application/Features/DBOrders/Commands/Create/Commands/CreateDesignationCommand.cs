using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateDesignationCommand : IRequest<CommadResponse>
    {
         public string DesignationCode { get; set; }
        public string? DesignationName { get; set; }
        public int OrderNo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }=DateTime.Now;
    }
}
