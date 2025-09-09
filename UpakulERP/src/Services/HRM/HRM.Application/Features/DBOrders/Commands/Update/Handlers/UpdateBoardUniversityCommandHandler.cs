using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Handlers
{
    public class UpdateBoardUniversityCommandHandler : IRequestHandler<UpdateBoardUniversityCommand, CommadResponse>
    {
        IMapper _mapper;
        IBoardUniversityRepository _repository;
        public UpdateBoardUniversityCommandHandler(IMapper mapper, IBoardUniversityRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateBoardUniversityCommand request, CancellationToken cancellationToken)
        {
           var varObj = _repository.GetById(request.BUId);
           var obj = _mapper.Map<UpdateBoardUniversityCommand, BoardUniversity>(request, varObj);
           bool isSuccess = await _repository.UpdateAsync(obj);
           return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }

}
