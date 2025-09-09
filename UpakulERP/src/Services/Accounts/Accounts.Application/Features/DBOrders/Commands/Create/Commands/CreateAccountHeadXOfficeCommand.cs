using Accounts.Domain.ViewModel;
using MediatR;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateAccountHeadXOfficeCommand : IRequest<CommadResponse>
    {
        public List<HeadXOfficeAssignMapVM> lst {  get; set; }
        public int? loggedinEmployeeId { get; set; }
    }
}
