using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Command;
using HRM.Domain.Models;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, CommadResponse>
    {
        IMapper _mapper;
        IDepartmentRepository _repository;
        public CreateDepartmentHandler(IMapper mapper, IDepartmentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.DepartmentName == request.DepartmentName || c.DepartmentCode == request.DepartmentCode).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Department name or code"), HttpStatusCode.NotAcceptable);
            else if (_repository.GetMany(c => c.OrderNo == request.OrderNo && c.OrderNo > 0).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Order No"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<Department>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }

        }
    }
}

