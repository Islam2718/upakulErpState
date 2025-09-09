using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateBankAccountMappingCommandHandler : IRequestHandler<UpdateBankAccountMappingCommand, CommadResponse>
    {
        IMapper _mapper;
        IBankAccountMappingRepository _repository;
        public UpdateBankAccountMappingCommandHandler(IMapper mapper, IBankAccountMappingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateBankAccountMappingCommand request, CancellationToken cancellationToken)
        {
            var getByIdObj = _repository.GetById(request.BankAccountMappingId);

            if (getByIdObj != null)
            {
                bool chequeBook = _repository.ChequeBook(request.BankAccountMappingId);
                if (chequeBook)
                    return new CommadResponse(MessageTexts.update_failed_ref, HttpStatusCode.BadRequest);
                else
                {
                    var obj = _mapper.Map<UpdateBankAccountMappingCommand, BankAccountMapping>(request, getByIdObj);
                    bool isSuccess = await _repository.UpdateAsync(obj);
                    return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
                }
            }
            else
                return new CommadResponse(MessageTexts.update_failed_ref, HttpStatusCode.BadRequest);



        }
    }


}
