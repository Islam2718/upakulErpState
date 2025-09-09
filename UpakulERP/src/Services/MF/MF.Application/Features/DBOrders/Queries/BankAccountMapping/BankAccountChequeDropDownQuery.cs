using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class BankAccountChequeDropDownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public int officeId { get; set; }
        public BankAccountChequeDropDownQuery(int id)
        {
            this.id= id;
        }
    }
}
