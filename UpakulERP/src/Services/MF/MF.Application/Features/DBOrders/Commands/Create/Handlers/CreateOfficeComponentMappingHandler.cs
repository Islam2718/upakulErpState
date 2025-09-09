using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateOfficeComponentMappingHandler : IRequestHandler<CreateOfficeComponentMappingCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IOfficeComponentMappingRepository _repository;  
        public CreateOfficeComponentMappingHandler(IMapper mapper, IOfficeComponentMappingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateOfficeComponentMappingCommand request, CancellationToken cancellationToken)
        {
            bool isSuccess = false;   
            isSuccess = await _repository.CreateOrUpdateAsync(request.ComponentId, request.loggedInEmployeeId, request.SelectedBranch);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }
    }


}
