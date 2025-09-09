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
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteBankAccountMappingCommandHandler : IRequestHandler<DeleteBankAccountMappingCommand, CommadResponse>
    {
        IMapper _mapper;
        IBankAccountMappingRepository _repository;

        public DeleteBankAccountMappingCommandHandler(IMapper mapper, IBankAccountMappingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteBankAccountMappingCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.BankAccountMappingId);
            if (_obj != null)
            {
                bool chequeBook = _repository.ChequeBook(request.BankAccountMappingId);
                if (chequeBook == true)
                {
                    return (false ? new CommadResponse(MessageTexts.delete_failed_ref, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed_ref, HttpStatusCode.BadRequest));
                }
                else
                {
                    var obj = _mapper.Map<DeleteBankAccountMappingCommand, BankAccountMapping>(request, _obj);
                    bool isSuccess = await _repository.UpdateAsync(obj);
                    return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));

                }
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }



}
