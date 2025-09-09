using System;
using System.Linq;
using System.Net;
using System.Threading;
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
    public class UpdateGraceScheduleCommandHandler : IRequestHandler<UpdateGraceScheduleCommand, CommadResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGraceScheduleRepository _repository;

        public UpdateGraceScheduleCommandHandler(IMapper mapper, IGraceScheduleRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateGraceScheduleCommand request, CancellationToken cancellationToken)
        {
           
            var varObj = _repository.GetById(request.Id);
            var obj = _mapper.Map<UpdateGraceScheduleCommand, GraceSchedule>(request, varObj);
            bool isSuccess = await _repository.UpdateAsync(obj);
            return isSuccess
                ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted)
                : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest);
        }
    }
}
