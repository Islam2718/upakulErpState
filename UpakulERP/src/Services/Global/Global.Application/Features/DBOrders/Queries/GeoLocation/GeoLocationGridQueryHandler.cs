using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global.Application.Contacts.Persistence;
using Global.Domain.Models;
using Global.Domain.Models.Views;
using MediatR;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Queries.GeoLocation
{
    public class GeoLocationGridQueryHandler : IRequestHandler<GeoLocationGridQuery, PaginatedResponse<VWGeoLocation>>
    {
        private readonly IGeoLocationRepository _repository;

        public GeoLocationGridQueryHandler(IGeoLocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<VWGeoLocation>> Handle(GeoLocationGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }


}
