using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreatePurposeCommand : IRequest<CommadResponse>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? MRAPurposeId { get; set; } // Add this
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }=DateTime.Now;
    }


}
