using System.Net;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Global.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, CommadResponse>
    {
        IMapper _mapper;
        IOfficeRepository _repository;
        public UpdateOfficeCommandHandler(IMapper mapper, IOfficeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentId == request.OfficeId)
                return new CommadResponse($"Invaild data format.", HttpStatusCode.NotAcceptable);

            else if (_repository.GetMany(c => c.OfficeCode == request.OfficeCode && c.OfficeCode != "" && c.OfficeId != request.OfficeId).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Code"), HttpStatusCode.NotAcceptable);
            else if (_repository.GetMany(x => x.ParentId == request.OfficeId).Any())
                return new CommadResponse($"{request.OfficeCode} has child.", HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _repository.GetById(request.OfficeId);
                var _obj = _mapper.Map<UpdateOfficeCommand, Office>(request, obj);
                bool isSuccess = await _repository.UpdateAsync(_obj);
                return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
