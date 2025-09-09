using Global.Domain.ViewModels;
using MediatR;

namespace Global.Application.Features.DBOrders.Queries.Country
{
    public class CountryByIdQuery : IRequest<CountryVM>
    {
        public int id { get; set; }
        public CountryByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
