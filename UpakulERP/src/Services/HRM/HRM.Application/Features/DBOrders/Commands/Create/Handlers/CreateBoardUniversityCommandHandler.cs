using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateBoardUniversityCommandHandler : IRequestHandler<CreateBoardUniversityCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IBoardUniversityRepository _repository;
        public CreateBoardUniversityCommandHandler(IMapper mapper, IBoardUniversityRepository boardUniversityRepository)
        {
            _mapper = mapper;
            _repository = boardUniversityRepository;
        }

        public async Task<CommadResponse> Handle(CreateBoardUniversityCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.BUName == request.BUName).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("University code"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<BoardUniversity>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
