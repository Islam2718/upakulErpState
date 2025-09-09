using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Domain.Models;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Handlers
{
   public class CreateDesignationHandler : IRequestHandler<CreateDesignationCommand, CommadResponse>
   {
        IMapper _mapper;
        IDesignationRepository _repository;
        public CreateDesignationHandler(IMapper mapper, IDesignationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse>Handle(CreateDesignationCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.DesignationName == request.DesignationName).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Designation name"), HttpStatusCode.NotAcceptable);
            else if (_repository.GetMany(c => c.OrderNo == request.OrderNo && c.OrderNo > 0).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Order No"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<Designation>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created,ReturnId:obj.DesignationId) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
   }
}

