using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Project.Application.Contacts.Persistence;
using Project.Application.Features.DBOrders.Commands.Update.Commands;
using Project.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Commands.Update.Handlers
{
    public class UpdateDonerCommandHandler : IRequestHandler<UpdateDonerCommand, CommadResponse>
    {
        IMapper _mapper;
        IDonerRepository _repository;
        public UpdateDonerCommandHandler(IMapper mapper, IDonerRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateDonerCommand request, CancellationToken cancellationToken)
        {
            var varObj = _repository.GetById(request.DonerId);
            var obj = _mapper.Map<UpdateDonerCommand, Doner>(request, varObj);
            bool isSuccess = await _repository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }

}
