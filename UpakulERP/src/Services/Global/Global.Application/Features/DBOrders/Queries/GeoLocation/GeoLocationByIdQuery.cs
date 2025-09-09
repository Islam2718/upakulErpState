using Global.Domain.Models.Views;
using Global.Domain.ViewModels;
using MediatR;

namespace Global.Application.Features.DBOrders.Queries.GeoLocation
{
    public class GeoLocationByIdQuery : IRequest<VWGeoLocation>
    {
        public int id { get; set; }
        public GeoLocationByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
