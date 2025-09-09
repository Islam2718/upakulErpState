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
    public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand, CommadResponse>
    {
        IMapper _mapper;
        IEducationRepository _repository;
        public DeleteEducationCommandHandler(IMapper mapper, IEducationRepository rpository)
        {
            _mapper = mapper;
            _repository = rpository;
        }
        public async Task<CommadResponse> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.EducationId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteEducationCommand, Education>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }
}
