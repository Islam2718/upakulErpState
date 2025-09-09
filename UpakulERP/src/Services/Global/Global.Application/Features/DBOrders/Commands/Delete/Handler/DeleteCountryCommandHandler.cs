using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Delete.Command;
using Global.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, CommadResponse>
    {
        IMapper _mapper;
        ICountryRepository _repository;
        public DeleteCountryCommandHandler(IMapper mapper, ICountryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.CountryId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteCountryCommand, Country>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }
}
