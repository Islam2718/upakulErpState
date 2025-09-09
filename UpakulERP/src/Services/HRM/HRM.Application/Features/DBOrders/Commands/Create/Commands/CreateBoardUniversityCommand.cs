using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateBoardUniversityCommand : IRequest<CommadResponse>
    {
        public string BUName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }= DateTime.Now;
    }
}
