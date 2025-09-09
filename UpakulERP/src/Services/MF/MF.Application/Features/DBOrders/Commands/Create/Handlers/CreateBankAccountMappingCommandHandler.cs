using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateBankAccountMappingCommandHandler : IRequestHandler<CreateBankAccountMappingCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IBankAccountMappingRepository _repository;
        public CreateBankAccountMappingCommandHandler(IMapper mapper, IBankAccountMappingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateBankAccountMappingCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.BankAccountNumber == request.BankAccountNumber).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Duplicate Account Number"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<BankAccountMapping>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
