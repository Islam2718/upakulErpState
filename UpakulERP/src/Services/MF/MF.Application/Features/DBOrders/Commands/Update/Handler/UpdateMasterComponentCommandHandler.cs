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
  public  class UpdateMasterComponentCommandHandler : IRequestHandler<UpdateMasterComponentCommand, CommadResponse>
    {
        IMapper _mapper;
        IMasterComponentRepository _repository;
        public UpdateMasterComponentCommandHandler(IMapper mapper, IMasterComponentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateMasterComponentCommand request, CancellationToken cancellationToken)
        {
            var mastercomponentObj = _repository.GetById(request.Id);
            var obj = _mapper.Map<UpdateMasterComponentCommand, MasterComponent>(request, mastercomponentObj);
            bool isSuccess = await _repository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }
}