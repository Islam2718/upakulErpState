using MediatR;
using Utility.Domain;

namespace Global.Application.Features.DBOrders.Queries.Bank
{
    public class BankDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public BankDropdownQuery(int id)
        {
            this.id = id;
        }
    }
}
