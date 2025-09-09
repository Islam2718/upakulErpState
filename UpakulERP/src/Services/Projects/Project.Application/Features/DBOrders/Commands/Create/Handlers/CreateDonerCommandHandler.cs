using System.Net;
using AutoMapper;
using MediatR;
using Project.Application.Contacts.Persistence;
using Project.Domain.Models;
using Utility.Constants;
using Utility.Response;
using Project.Application.Features.DBOrders.Commands.Create.Commands;

namespace Project.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateDonerCommandHandler : IRequestHandler<CreateDonerCommand, CommadResponse>
    {
        IMapper _mapper;
        IDonerRepository _repository;
        public CreateDonerCommandHandler(IMapper mapper, IDonerRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateDonerCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Doner>(request);
            obj.CreatedOn = DateTime.Now;
            bool isSuccess = await _repository.AddAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }
    }
}
