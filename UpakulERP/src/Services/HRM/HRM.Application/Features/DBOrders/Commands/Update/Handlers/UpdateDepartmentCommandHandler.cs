using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Handlers
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, CommadResponse>
    {
        IMapper _mapper;
        IDepartmentRepository _repository;
        public UpdateDepartmentCommandHandler(IMapper mapper, IDepartmentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => (c.DepartmentName == request.DepartmentName || (c.DepartmentCode ?? "") == (request.DepartmentCode ?? "")) && c.DepartmentId != request.DepartmentId).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Department name or code"), HttpStatusCode.NotAcceptable);
            else if (_repository.GetMany(c => c.OrderNo == request.OrderNo && c.OrderNo > 0 && c.DepartmentId != request.DepartmentId).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Order No"), HttpStatusCode.NotAcceptable);
            else
            {
                var varObj = _repository.GetById(request.DepartmentId);
                var obj = _mapper.Map<UpdateDepartmentCommand, Department>(request, varObj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
            }
        }
    }

}
