using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Command;
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
   public class CreateLeaveSetupCommandHandler : IRequestHandler<CreateLeaveSetupCommand, CommadResponse>
    {
        IMapper _mapper;
        ILeaveSetupRepository _leavesetupRepository;
        public CreateLeaveSetupCommandHandler(IMapper mapper, ILeaveSetupRepository leavesetupRepository)
        {
            _mapper = mapper;
            _leavesetupRepository = leavesetupRepository;
        }

        public async Task<CommadResponse> Handle(CreateLeaveSetupCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<LeaveSetup>(request);
            bool isSuccess = await _leavesetupRepository.AddAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }
    }
}