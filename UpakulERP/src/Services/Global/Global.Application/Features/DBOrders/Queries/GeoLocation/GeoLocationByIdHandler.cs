using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Domain.Models.Views;
using Global.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Application.Features.DBOrders.Queries.GeoLocation
{
    class GeoLocationByIdHandler : IRequestHandler<GeoLocationByIdQuery, VWGeoLocation>
    {
        IGeoLocationRepository _repository;
        IMapper _mapper;
        public GeoLocationByIdHandler(IGeoLocationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<VWGeoLocation> Handle(GeoLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var obj =  _repository.GetByIdFromView(request.id);
            return _mapper.Map<VWGeoLocation>(obj);
        }
    }
}
