using System.Net;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IGroupRepository _repository;
        public CreateGroupCommandHandler(IMapper mapper, IGroupRepository samityRepository)
        {
            _mapper = mapper;
            _repository = samityRepository;
        }

        public async Task<CommadResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
                var obj = _mapper.Map<Group>(request);
                obj.GroupCode = DateTime.Now.ToString("yyddMMHHmm");
                if (_repository.GetMany(c => c.GroupCode == obj.GroupCode).Any())
                    return new CommadResponse(MessageTexts.duplicate_entry("Group code"), HttpStatusCode.NotAcceptable);

                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }

    }
}
