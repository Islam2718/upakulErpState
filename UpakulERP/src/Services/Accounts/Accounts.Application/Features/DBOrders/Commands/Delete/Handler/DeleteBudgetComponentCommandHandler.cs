using Accounts.Application.Contacts.Persistence;
using Accounts.Application.Features.DBOrders.Commands.Delete.Command;
using Accounts.Domain.Models;
using AutoMapper;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Delete.Handler
{

    public class DeleteBudgetComponentCommandHandler : IRequestHandler<DeleteBudgetComponentCommand, CommadResponse>
    {
        IMapper _mapper;
        IBudgetComponentRepository _repository;
        public DeleteBudgetComponentCommandHandler(IMapper mapper, IBudgetComponentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteBudgetComponentCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.Id);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteBudgetComponentCommand, BudgetComponent>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }
}
