using Accounts.Application.Contacts.Persistence.Voucher;
using Accounts.Application.Features.DBOrders.Commands.Create.Commands;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateAccountHeadXOfficeCommandHandler : IRequestHandler<CreateAccountHeadXOfficeCommand, CommadResponse>
    {
        IAccountHeadRepository _repository;
        public CreateAccountHeadXOfficeCommandHandler(IAccountHeadRepository repository)
        {
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(CreateAccountHeadXOfficeCommand request, CancellationToken cancellationToken)
        {
            bool isSuccess = await _repository.OffficeAssignPost(request.lst, request.loggedinEmployeeId??0);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }
    }
}
