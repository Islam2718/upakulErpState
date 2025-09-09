using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Handler
{
  public  class DeleteMasterComponentCommandHandler : IRequestHandler<DeleteMasterComponentCommand, CommadResponse>
    {
        IMapper _mapper;
        IMasterComponentRepository _repository;
        public DeleteMasterComponentCommandHandler(IMapper mapper, IMasterComponentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(DeleteMasterComponentCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.Id);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteMasterComponentCommand, MasterComponent>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }



}
