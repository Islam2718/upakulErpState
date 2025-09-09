using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteMemberCommandHandler : IRequestHandler<UpdateMemberApprovedCommand, CommadResponse>
    {
        IMapper _mapper;
        IMemberRepository _repository;
        public DeleteMemberCommandHandler(IMapper mapper, IMemberRepository rpository)
        {
            _mapper = mapper;
            _repository = rpository;
        }
        public async Task<CommadResponse> Handle(UpdateMemberApprovedCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.MemberId);
            if (_obj != null)
            {
                var obj = _mapper.Map<UpdateMemberApprovedCommand, Domain.Models.Member>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }


}
