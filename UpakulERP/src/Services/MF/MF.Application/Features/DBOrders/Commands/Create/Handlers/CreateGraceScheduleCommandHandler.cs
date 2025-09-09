using AutoMapper;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;
using MF.Application.Features.DBOrders.Commands.Create.Commands;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateGraceScheduleCommand : IRequestHandler<Commands.CreateGraceScheduleCommand, CommadResponse>
    {
        IMapper _mapper;
        IGraceScheduleRepository _repository;

        public CreateGraceScheduleCommand(IMapper mapper, IGraceScheduleRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(Commands.CreateGraceScheduleCommand request, CancellationToken cancellationToken)
        {
            // Optional duplicate check (customize as needed)
            if (_repository.GetMany(x =>
                //x.Reason == request.Reason &&
               ( x.OfficeId ?? 0) == (request.OfficeId ?? 0) &&
               ( x.GroupId ?? 0 )== (request.GroupId ?? 0) &&
                (x.LoanId ?? 0) == (request.LoanId ?? 0) &&
                x.GraceFrom == DateTime.Parse(request.GraceFrom) &&
                x.GraceTo == DateTime.Parse(request.GraceTo)).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("GraceSchedule"), HttpStatusCode.NotAcceptable);

            var graceSchedule = _mapper.Map<GraceSchedule>(request);
            bool isSuccess = await _repository.AddAsync(graceSchedule);

            return isSuccess
                ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created)
                : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest);
        }
    }
}
