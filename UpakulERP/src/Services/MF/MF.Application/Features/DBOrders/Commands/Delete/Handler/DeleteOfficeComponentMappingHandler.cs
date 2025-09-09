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
    public class DeleteOfficeComponentMappingHandler : IRequestHandler<DeleteOfficeComponentMappingCommand, CommadResponse>
    {
        IMapper _mapper;
        IOfficeComponentMappingRepository _repository;
        public DeleteOfficeComponentMappingHandler(IMapper mapper, IOfficeComponentMappingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteOfficeComponentMappingCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.OfficeComponentMappingId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteOfficeComponentMappingCommand, OfficeComponentMapping>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }

}
