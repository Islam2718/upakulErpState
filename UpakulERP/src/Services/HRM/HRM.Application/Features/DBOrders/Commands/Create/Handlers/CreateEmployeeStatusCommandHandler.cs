using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateEmployeeStatusCommandHandler : IRequestHandler<CreateEmployeeStatusCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IEmployeeStatusRepository _employeeStatusRepository;
        public CreateEmployeeStatusCommandHandler(IMapper mapper, IEmployeeStatusRepository employeeStatusRepository)
        {
            _mapper = mapper;
            _employeeStatusRepository = employeeStatusRepository;
        }

        public async Task<CommadResponse> Handle(CreateEmployeeStatusCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<EmployeeStatus>(request);
            bool isSuccess = await _employeeStatusRepository.AddAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }
    }
}
