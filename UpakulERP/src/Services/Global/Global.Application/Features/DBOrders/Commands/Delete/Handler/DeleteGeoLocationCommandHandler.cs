using System.Net;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Delete.Command;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Global.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteGeoLocationCommandHandler : IRequestHandler<DeleteGeoLocationCommand, CommadResponse>
    {
        IMapper _mapper;
        IGeoLocationRepository _repository;
        public DeleteGeoLocationCommandHandler(IMapper mapper, IGeoLocationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteGeoLocationCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(x => x.ParentId == request.GeoLocationId).Any())
                return new CommadResponse(MessageTexts.child_found("Geo location"), HttpStatusCode.NotAcceptable);

            var _obj = _repository.GetById(request.GeoLocationId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteGeoLocationCommand, GeoLocation>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }
}
