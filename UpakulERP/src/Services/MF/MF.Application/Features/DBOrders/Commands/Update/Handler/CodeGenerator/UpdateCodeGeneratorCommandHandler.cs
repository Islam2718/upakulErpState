using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler.CodeGenerator
{
    public class UpdateCodeGeneratorCommandHandler : IRequestHandler<UpdateCodeGeneratorCommand, CommadResponse>
    {
        private readonly IIdGeneratorRepository _repository;

        public UpdateCodeGeneratorCommandHandler(IIdGeneratorRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateCodeGeneratorCommand request, CancellationToken cancellationToken)
        {
            // Use await for consistency (in case you make GetByIdAsync later)
            var codeGeneratorObj = _repository.GetById(request.Id);

            if (codeGeneratorObj == null)
            {
                return new CommadResponse("CodeGenerator not found", HttpStatusCode.NotFound);
            }

            // Manual field mapping
            codeGeneratorObj.TypeName = request.TypeName;
            codeGeneratorObj.Description = request.Description;
            codeGeneratorObj.CodeLength = request.CodeLength;
            codeGeneratorObj.StartNo = request.StartNo;
            codeGeneratorObj.EndNo = request.EndNo;
            codeGeneratorObj.MainJoinCode = request.MainJoinCode;
            codeGeneratorObj.VirtualJoinCode = request.VirtualJoinCode;

            // Optional: Add audit fields if needed
            // codeGeneratorObj.UpdatedBy = request.UpdatedBy;
            // codeGeneratorObj.UpdatedOn = DateTime.UtcNow;

            var isSuccess = await _repository.UpdateAsync(codeGeneratorObj);

            return isSuccess
                ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted, request.Id)
                : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest);
        }
    }
}
