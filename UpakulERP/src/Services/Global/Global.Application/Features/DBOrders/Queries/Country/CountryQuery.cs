using MediatR;
using Utility.Domain;

namespace Global.Application.Features.DBOrders.Queries.Country
{
    public class CountryQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public CountryQuery(int? id)
        {
            this.id = id ?? 0;
        }
    }
}
