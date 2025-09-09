using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteOccupationCommandHandler : IRequestHandler<DeleteOccupationCommand, CommadResponse>
    {
        IMapper _mapper;
        IOccupationRepository _repository;
        public DeleteOccupationCommandHandler(IMapper mapper, IOccupationRepository rpository)
        {
            _mapper = mapper;
            _repository = rpository;
        }
        public async Task<CommadResponse> Handle(DeleteOccupationCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.OccupationId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteOccupationCommand, Occupation>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }



}
