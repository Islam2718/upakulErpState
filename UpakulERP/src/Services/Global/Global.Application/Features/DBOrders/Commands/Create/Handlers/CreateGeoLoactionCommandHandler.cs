using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Create.Command;
using Global.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Create.Handler
{
    public class CreateGeoLoactionCommandHandler : IRequestHandler<CreateGeoLoactionCommand, CommadResponse>
    {
        IMapper _mapper;
        IGeoLocationRepository _repository;
        public CreateGeoLoactionCommandHandler(IMapper mapper, IGeoLocationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateGeoLoactionCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.GeoLocationCode == request.GeoLocationCode && c.GeoLocationCode != "").Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Code"), HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<GeoLocation>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created, ReturnId: obj.GeoLocationId) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
