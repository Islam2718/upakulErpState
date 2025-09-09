using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Commands
{
    public class UpdateDepartmentCommand : IRequest<CommadResponse>
    {
        public int DepartmentId { get; set; }
        public string? DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int OrderNo { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }=DateTime.Now;
    }


}
