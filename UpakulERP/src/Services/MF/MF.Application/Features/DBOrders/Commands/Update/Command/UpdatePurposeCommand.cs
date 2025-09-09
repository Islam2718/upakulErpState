using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdatePurposeCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? MRAPurposeId { get; set; } 
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }=DateTime.Now;
    }
}
