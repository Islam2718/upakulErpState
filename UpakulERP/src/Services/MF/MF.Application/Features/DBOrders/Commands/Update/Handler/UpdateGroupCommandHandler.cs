using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
   public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, CommadResponse>
    {
        IMapper _mapper;
        IGroupRepository _samityRepository;
        public UpdateGroupCommandHandler(IMapper mapper, IGroupRepository samityRepository)
        {
            _mapper = mapper;
            _samityRepository = samityRepository;
        }

        public async Task<CommadResponse> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            //if (_samityRepository.GetMany(c => c.SamityCode == request.SamityCode).Any())
            //    return new CommadResponse(MessageTexts.duplicate_entry("Samity code"), HttpStatusCode.NotAcceptable);
            //else
            //{
            var varObj = _samityRepository.GetById(request.GroupId);
            var obj = _mapper.Map<UpdateGroupCommand, Domain.Models.Group>(request, varObj);
            bool isSuccess = await _samityRepository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
            //}
        }
    }


}
