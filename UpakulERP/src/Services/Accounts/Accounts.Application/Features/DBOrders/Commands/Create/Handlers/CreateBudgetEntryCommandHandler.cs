using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Accounts.Application.Contacts.Persistence;
using Accounts.Application.Features.DBOrders.Commands.Create.Commands;
using Accounts.Domain.Models;
using AutoMapper;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Create.Handlers
{

    public class CreateBudgetEntryCommandHandler : IRequestHandler<CreateBudgetEntryCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IBudgetEntryRepository _repository;
        public CreateBudgetEntryCommandHandler(IMapper mapper, IBudgetEntryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateBudgetEntryCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.FinancialYear == request.FinancialYear).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Financial Year"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<BudgetEntry>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }

        }
    }
}
