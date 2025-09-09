using MediatR;
using Utility.Domain;

namespace Global.Application.Features.DBOrders.Queries.Bank
{
    public class BankQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public BankQuery(int? id)
        {
            this.id = id ?? 0;
        }
    }
}
