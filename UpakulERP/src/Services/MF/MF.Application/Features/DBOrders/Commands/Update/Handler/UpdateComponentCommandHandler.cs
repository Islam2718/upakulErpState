using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateComponentCommandHandler : IRequestHandler<UpdateComponentCommand, CommadResponse>
    {
        IMapper _mapper;
        IComponentRepository _repository;
        public UpdateComponentCommandHandler(IMapper mapper, IComponentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateComponentCommand request, CancellationToken cancellationToken)
        {
            var componentObj = _repository.GetById(request.Id);
            var obj = _mapper.Map<UpdateComponentCommand, Component>(request,componentObj);
            bool isSuccess = await _repository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }
}
