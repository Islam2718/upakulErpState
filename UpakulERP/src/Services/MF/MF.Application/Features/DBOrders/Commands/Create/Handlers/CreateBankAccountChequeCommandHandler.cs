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

    public class CreateBankAccountChequeCommandHandler : IRequestHandler<CreateBankAccountChequeCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IBankAccountMappingRepository _repository;
        public CreateBankAccountChequeCommandHandler(IMapper mapper, IBankAccountMappingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateBankAccountChequeCommand request, CancellationToken cancellationToken)
        {            
          var obj = _mapper.Map<BankAccountCheque>(request);
          bool isSuccess = await _repository.AddChequeAsync(obj);
          return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
         
        }
    }
}
