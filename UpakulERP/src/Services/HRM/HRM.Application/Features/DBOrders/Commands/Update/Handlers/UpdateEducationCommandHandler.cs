using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Handlers
{
    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, CommadResponse>
    {
        IMapper _mapper;
        IEducationRepository _repository;
        public UpdateEducationCommandHandler(IMapper mapper, IEducationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
          var varObj = _repository.GetById(request.EducationId);
          var obj = _mapper.Map<UpdateEducationCommand, Education>(request, varObj);
          bool isSuccess = await _repository.UpdateAsync(obj);
          return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }
}
