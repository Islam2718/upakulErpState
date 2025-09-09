using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Global.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CommadResponse>
    {
        IMapper _mapper;
        ICountryRepository _repository;
        public UpdateCountryCommandHandler(IMapper mapper, ICountryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var countryObj = _repository.GetById(request.CountryId);
            var obj = _mapper.Map<UpdateCountryCommand, Country>(request, countryObj);
            bool isSuccess = await _repository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }
}
