using Accounts.Application.Contacts.Persistence.Voucher;
using Accounts.Application.Features.DBOrders.Commands.Create.Commands;
using Accounts.Domain.Models.Voucher;
using Accounts.Domain.ViewModel;
using AutoMapper;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateAccountHeadCommandHandler : IRequestHandler<CreateAccountHeadCommand, CommadResponse>
    {
        IAccountHeadRepository _repository;
        IMapper _mapper;
        public CreateAccountHeadCommandHandler(IAccountHeadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommadResponse> Handle(CreateAccountHeadCommand request, CancellationToken cancellationToken)
        {
            //if (_repository.GetMany(c => c.BankName == request.BankName || c.BankShortCode == request.BankShortCode).Any())
            //    return new CommadResponse(MessageTexts.duplicate_entry("Bank name or Short code"), HttpStatusCode.NotAcceptable);
            //else
            //{
            var obj = _mapper.Map<AccountHead>(request);
            obj.HeadCode = "0";
            obj.CreatedOn = DateTime.Now;
            bool isSuccess = await _repository.AddAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created
                , Returnobj:new AccountHeadXChildVM {AccountId= obj.AccountId, HeadCode=obj.HeadCode,HeadName = obj.HeadName,ParentId = obj.ParentId ,IsTransactable = obj.IsTransactable }) 
                : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            //}
        }
    }
}
