using System.Net;
using MediatR;
using Project.Application.Contacts.Persistence;
using Project.Application.Features.DBOrders.Commands.Update.Commands;
using Utility.Constants;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Commands.Update.Handlers
{
    public class UpdateActivityPlanApprovedCommandHandler : IRequestHandler<UpdateActivityPlanApprovedCommand, CommadResponse>
    {
        IActivityPlanRepository _repository;
        public UpdateActivityPlanApprovedCommandHandler(IActivityPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateActivityPlanApprovedCommand request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.Id);
            obj.UpdatedBy = request.UpdatedBy;
            obj.UpdatedOn=DateTime.Now;
            obj.IsApproved=true;
            bool isSuccess = await _repository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }
}
