using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{

    public class UpdateOccupationCommandHandler : IRequestHandler<UpdateOccupationCommand, CommadResponse>
    {
        IMapper _mapper;
        IOccupationRepository _repository;

        public UpdateOccupationCommandHandler(IMapper mapper, IOccupationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateOccupationCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.OccupationName == request.OccupationName).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Duplicate Occupation Name"), HttpStatusCode.NotAcceptable);
            else
            {
                var varObj = _repository.GetById(request.OccupationId);
                var obj = _mapper.Map<UpdateOccupationCommand, Occupation>(request, varObj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
            }
        }
    }


}
