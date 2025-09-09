using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
    public class CreateOccupationCommandHandler : IRequestHandler<CreateOccupationCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IOccupationRepository _repository;
        public CreateOccupationCommandHandler(IMapper mapper, IOccupationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateOccupationCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.OccupationName == request.OccupationName).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Duplicate Occupation"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<Occupation>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }

    }


}
