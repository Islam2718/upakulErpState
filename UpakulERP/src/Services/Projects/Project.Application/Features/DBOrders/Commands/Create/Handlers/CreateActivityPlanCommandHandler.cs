using System.Net;
using MediatR;
using Project.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Response;
using Project.Application.Features.DBOrders.Commands.Create.Commands;

namespace Project.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateActivityPlanCommandHandler : IRequestHandler<CreateActivityPlanCommand, CommadResponse>
    {
        IActivityPlanRepository _repository;
        public CreateActivityPlanCommandHandler(IActivityPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateActivityPlanCommand request, CancellationToken cancellationToken)
        {
            bool isSuccess =  _repository.ChangeTable(request.lst,request.ProjectId,request.loggedinEmpId);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }
    }
}
