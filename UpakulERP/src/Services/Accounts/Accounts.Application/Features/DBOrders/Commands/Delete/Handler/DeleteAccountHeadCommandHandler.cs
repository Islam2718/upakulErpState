using Accounts.Application.Contacts.Persistence.Voucher;
using Accounts.Application.Features.DBOrders.Commands.Delete.Command;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteAccountHeadCommandHandler : IRequestHandler<DeleteAccountHeadCommand, CommadResponse>
    {
        IAccountHeadRepository _repository;
        public DeleteAccountHeadCommandHandler(IAccountHeadRepository repository)
        {
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteAccountHeadCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.AccountId);
            _obj.IsActive = false;
            _obj.DeletedBy = request.DeletedBy;
            _obj.DeletedOn = DateTime.Now;
            bool isSuccess = await _repository.UpdateAsync(_obj);
            return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
        }
    }
}
