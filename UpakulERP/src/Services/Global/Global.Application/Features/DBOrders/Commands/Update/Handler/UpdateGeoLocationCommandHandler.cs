using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Global.Domain.Models;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateGeoLocationCommandHandler : IRequestHandler<UpdateGeoLocationCommand, CommadResponse>
    {
        IMapper _mapper;
        IGeoLocationRepository _repository;
        public UpdateGeoLocationCommandHandler(IMapper mapper, IGeoLocationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateGeoLocationCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentId == request.GeoLocationId)
                return new CommadResponse($"Invaild data format.", HttpStatusCode.NotAcceptable);

           else if (_repository.GetMany(c => c.GeoLocationCode == request.GeoLocationCode && c.GeoLocationCode != "" && c.GeoLocationId!=request.GeoLocationId).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("Code"), HttpStatusCode.NotAcceptable);
            else if (_repository.GetMany(x => x.ParentId == request.GeoLocationId).Any())
                return new CommadResponse($"{request.GeoLocationCode} has child.", HttpStatusCode.NotAcceptable);
            else
            {
                var _obj = _repository.GetById(request.GeoLocationId);
                var obj = _mapper.Map<UpdateGeoLocationCommand, GeoLocation>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
