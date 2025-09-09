using Accounts.Application.Contacts.Persistence.Voucher;
using Accounts.Application.Features.DBOrders.Commands.Update.Commands;
using Accounts.Domain.Models.Voucher;
using AutoMapper;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateAccountHeadCommandHandler : IRequestHandler<UpdateAccountHeadCommand, CommadResponse>
    {
        IAccountHeadRepository _repository;
        IMapper _mapper;
        public UpdateAccountHeadCommandHandler(IAccountHeadRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommadResponse> Handle(UpdateAccountHeadCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.AccountId);
            var obj = _mapper.Map<UpdateAccountHeadCommand, AccountHead>(request, _obj);
            obj.UpdatedOn = DateTime.Now;
            bool isSuccess = await _repository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }
}
