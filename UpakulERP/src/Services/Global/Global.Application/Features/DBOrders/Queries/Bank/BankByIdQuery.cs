using Global.Domain.ViewModels;
using MediatR;

namespace Global.Application.Features.DBOrders.Queries.Bank
{
    public class BankByIdQuery : IRequest<BankVM>
    {
        public int id { get; set; }
        public BankByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
