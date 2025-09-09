using System.Net;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Create.Commands;
using Global.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, CommadResponse>
    {
        IMapper _mapper;
        ICountryRepository _repository;
        public CreateCountryHandler(IMapper mapper, ICountryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.CountryCode == request.CountryCode || c.CountryName == request.CountryName).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Countru name or Short code"), HttpStatusCode.NotAcceptable);
            
            var obj = _mapper.Map<Country>(request);
            bool isSuccess = await _repository.AddAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }
    }
}
