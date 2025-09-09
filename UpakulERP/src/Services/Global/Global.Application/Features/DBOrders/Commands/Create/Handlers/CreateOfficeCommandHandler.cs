using System.Net;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Create.Commands;
using Global.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Create.Handler
{
    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, CommadResponse>
    {
        IMapper _mapper;
        IOfficeRepository _repository;
        public CreateOfficeCommandHandler(IMapper mapper, IOfficeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.OfficeCode == request.OfficeCode).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Code"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<Office>(request);
                bool isSuccess = await _repository.AddAsync(obj);

                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created, ReturnId: obj.OfficeId) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
