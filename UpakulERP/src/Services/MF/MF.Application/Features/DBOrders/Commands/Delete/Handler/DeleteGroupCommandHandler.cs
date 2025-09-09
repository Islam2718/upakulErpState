using System.Net;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, CommadResponse>
    {
        IMapper _mapper;
        IGroupRepository _repository;
        public DeleteGroupCommandHandler(IMapper mapper, IGroupRepository rpository)
        {
            _mapper = mapper;
            _repository = rpository;
        }
        public async Task<CommadResponse> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.SamityId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteGroupCommand, Group>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }
}
