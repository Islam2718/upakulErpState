using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Domain.Models.Training;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateTrainingCommandHandler : IRequestHandler<CreateTrainingCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        ITrainingRepository _repository;
        public CreateTrainingCommandHandler(IMapper mapper, ITrainingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateTrainingCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.Institute == request.Institute).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Institute"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<Training>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }


}
