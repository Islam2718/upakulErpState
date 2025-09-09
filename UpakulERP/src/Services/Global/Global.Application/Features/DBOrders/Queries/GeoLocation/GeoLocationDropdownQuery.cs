using MediatR;
using Utility.Domain;

namespace Global.Application.Features.DBOrders.Queries.GeoLocation
{
    public class GeoLocationDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int pid {  get; set; }
        public GeoLocationDropdownQuery(int? pid)
        {
            this.pid = pid??0;
        }
    }
}
