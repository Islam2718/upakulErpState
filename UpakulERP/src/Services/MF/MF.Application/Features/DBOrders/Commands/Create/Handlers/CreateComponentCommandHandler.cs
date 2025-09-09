using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models;

using System.Net;

using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateComponentCommandHandler : IRequestHandler<CreateComponentCommand, CommadResponse>
    {
        IMapper _mapper;
        IComponentRepository _repository;
        public CreateComponentCommandHandler(IMapper mapper, IComponentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateComponentCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.ComponentCode == request.ComponentCode).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Component Code"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<Component>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
