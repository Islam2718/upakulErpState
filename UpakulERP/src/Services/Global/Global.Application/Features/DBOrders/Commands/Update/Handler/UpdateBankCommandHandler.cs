using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Global.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, CommadResponse>
    {
        IMapper _mapper;
        IBankRepository _bankRepository;
        public UpdateBankCommandHandler(IMapper mapper, IBankRepository bankRepository)
        {
            _mapper = mapper;
            _bankRepository = bankRepository;
        }

        public async Task<CommadResponse> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
        {
            if(_bankRepository.GetMany(c => (c.BankName == request.BankName || c.BankShortCode == request.BankShortCode) && c.BankId!=request.BankId).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Bank name or Short code"), HttpStatusCode.NotAcceptable);
            else
            {
                var bankObj = _bankRepository.GetById(request.BankId);
                var obj = _mapper.Map<UpdateBankCommand, Bank>(request, bankObj);
                bool isSuccess = await _bankRepository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
