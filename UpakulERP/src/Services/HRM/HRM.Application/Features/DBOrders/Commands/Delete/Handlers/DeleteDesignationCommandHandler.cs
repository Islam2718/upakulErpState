using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Delete.Commands;
using HRM.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Delete.Handlers
{
    
    public class DeleteDesignationCommandHandler : IRequestHandler<DeleteDesignationCommand, CommadResponse>
    {
        IMapper _mapper;
        IDesignationRepository _repository;
        public DeleteDesignationCommandHandler(IMapper mapper, IDesignationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteDesignationCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.DesignationId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteDesignationCommand, Designation>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }


}
