using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Create.Commands;
using Global.Domain.Models;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IBankRepository _repository;
        public CreateBankCommandHandler(IMapper mapper, IBankRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
            if(_repository.GetMany(c=>c.BankName==request.BankName || c.BankShortCode==request.BankShortCode).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Bank name or Short code"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<Bank>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
           
        }
    }
}
