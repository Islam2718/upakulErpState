using System.Net;
using Accounts.Application.Contacts.Persistence;
using Accounts.Application.Features.DBOrders.Commands.Create.Commands;
using Accounts.Domain.Models;
using AutoMapper;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateBudgetComponentCommandHandler : IRequestHandler<CreateBudgetComponentCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IBudgetComponentRepository _repository;
        public CreateBudgetComponentCommandHandler(IMapper mapper, IBudgetComponentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateBudgetComponentCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.ComponentName == request.ComponentName).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Component Name"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<BudgetComponent>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
