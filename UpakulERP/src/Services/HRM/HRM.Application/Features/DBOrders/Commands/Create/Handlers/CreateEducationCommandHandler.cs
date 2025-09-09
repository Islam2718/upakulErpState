using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Domain.Models;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IEducationRepository _repository;
        public CreateEducationCommandHandler(IMapper mapper, IEducationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.EducationName == request.EducationName).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Education name"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<Education>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
