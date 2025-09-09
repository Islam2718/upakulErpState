using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Command
{
    public class CreateDepartmentCommand : IRequest<CommadResponse>
    {
        public  string? DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int OrderNo { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }=DateTime.Now;
    }
}
