using System.Net;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateGroupWiseEmployeeAssignCommandHandler : IRequestHandler<UpdateGroupWiseEmployeeAssignCommand, CommadResponse>
    {
        IMapper _mapper;
        IGroupWiseEmployeeAssignRepository _repository;
        public UpdateGroupWiseEmployeeAssignCommandHandler(IMapper mapper, IGroupWiseEmployeeAssignRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateGroupWiseEmployeeAssignCommand request, CancellationToken cancellationToken)
        {
            bool isSuccess = false;
            var obj = _repository.ReleaseById(request.Id);
            if (request.ReleaseNote != null)
            {
                obj.ReleaseNote = request.ReleaseNote;
                obj.ReleaseDate = request.ReleaseDate;
                obj.UpdatedBy = request.UpdatedBy;
                obj.UpdatedOn = DateTime.Now;
                isSuccess = await _repository.UpdateAsync(obj);
            }
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));

        }
    }

}
