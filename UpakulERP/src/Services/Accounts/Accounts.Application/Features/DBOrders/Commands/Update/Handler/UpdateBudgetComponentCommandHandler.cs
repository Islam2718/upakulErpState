using Accounts.Application.Contacts.Persistence;
using Accounts.Application.Features.DBOrders.Commands.Update.Command;
using Accounts.Domain.Models;
using AutoMapper;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Update.Handler
{

    public class UpdateBudgetComponentCommandHandler : IRequestHandler<UpdateBudgetComponentCommand, CommadResponse>
    {
        IMapper _mapper;
        IBudgetComponentRepository _repository;
        public UpdateBudgetComponentCommandHandler(IMapper mapper, IBudgetComponentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateBudgetComponentCommand request, CancellationToken cancellationToken)
        {
            var varObj = _repository.GetById(request.Id);
            var obj = _mapper.Map<UpdateBudgetComponentCommand, BudgetComponent>(request, varObj);
            bool isSuccess = await _repository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
         
        }
    }
}
