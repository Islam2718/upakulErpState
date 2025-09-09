using System.Net;
using AutoMapper;
using MediatR;
using Project.Application.Contacts.Persistence;
using Project.Application.Features.DBOrders.Commands.Create.Commands;
using Project.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CommadResponse>
    {
        IMapper _mapper;
        IProjectRepository _repository;
        public CreateProjectCommandHandler(IMapper mapper, IProjectRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Projects>(request);
            obj.CreatedOn=DateTime.Now;
            bool isSuccess = await _repository.AddAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }
    }
}
