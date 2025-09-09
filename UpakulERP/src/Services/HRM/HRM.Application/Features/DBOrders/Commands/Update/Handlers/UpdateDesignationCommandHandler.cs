using System.Net;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Handlers
{
    public class UpdateDesignationCommandHandler : IRequestHandler<UpdateDesignationCommand, CommadResponse>
    {
        IMapper _mapper;
        IDesignationRepository _repository;
        public UpdateDesignationCommandHandler(IMapper mapper, IDesignationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateDesignationCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => (c.DesignationName == request.DesignationName || c.DesignationCode == request.DesignationCode) && c.DesignationId != request.DesignationId).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Designation name or code"), HttpStatusCode.NotAcceptable);
            else if (_repository.GetMany(c => c.OrderNo == request.OrderNo && c.OrderNo > 0 && c.DesignationId != request.DesignationId).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Order No"), HttpStatusCode.NotAcceptable);
            else
            {
                var varObj = _repository.GetById(request.DesignationId);
                var obj = _mapper.Map<UpdateDesignationCommand, Designation>(request, varObj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
            }
        }
    }

}
