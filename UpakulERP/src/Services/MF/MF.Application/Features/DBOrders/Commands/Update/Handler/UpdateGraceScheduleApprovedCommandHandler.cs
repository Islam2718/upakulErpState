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
    public class UpdateGraceScheduleApprovedCommandHandler : IRequestHandler<UpdateGraceScheduleApprovedCommand, CommadResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGraceScheduleRepository _repository;

        public UpdateGraceScheduleApprovedCommandHandler(IMapper mapper, IGraceScheduleRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateGraceScheduleApprovedCommand request, CancellationToken cancellationToken)
        {
            var varObj = _repository.GetById(request.Id);

            if (varObj == null)
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);

            var obj = _mapper.Map<UpdateGraceScheduleApprovedCommand, GraceSchedule>(request, varObj);
            bool isSuccess = await _repository.UpdateAsync(obj);

            return isSuccess
                          ? new CommadResponse(MessageTexts.approved_success, HttpStatusCode.OK)
                          : new CommadResponse(MessageTexts.approved_failed, HttpStatusCode.BadRequest);
        }
    }
}