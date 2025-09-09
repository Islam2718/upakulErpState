using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Delete.Command;
using Global.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommand, CommadResponse>
    {
        IMapper _mapper;
        IOfficeRepository _repository;
        public DeleteOfficeCommandHandler(IMapper mapper, IOfficeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
        {
            if(_repository.GetMany(x=>x.ParentId==request.OfficeId).Any())
                return new CommadResponse(MessageTexts.child_found("Office"), HttpStatusCode.NotAcceptable);


            var _obj = _repository.GetById(request.OfficeId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteOfficeCommand, Office>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }
}
