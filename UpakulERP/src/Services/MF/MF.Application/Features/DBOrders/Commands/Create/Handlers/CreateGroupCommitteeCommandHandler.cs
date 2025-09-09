using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateGroupCommitteeCommandHandler : IRequestHandler<CreateGroupCommitteeCommand, CommadResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGroupCommitteeRepository _repository;

        public CreateGroupCommitteeCommandHandler(IMapper mapper, IGroupCommitteeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateGroupCommitteeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //if (_repository.GetMany(c => c.CommitteePositionId == request.).Any())
                //    return new CommadResponse(MessageTexts.duplicate_entry("Duplicate Entry"), HttpStatusCode.NotAcceptable);



                bool isSuccess = await _repository.AddRangeAsync(request);
                return isSuccess
                    ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created)
                    : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return new CommadResponse(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }

   

}
