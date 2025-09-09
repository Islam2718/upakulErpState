using MediatR;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class BankAccountMappingByIdQuery : IRequest<BankAccountMappingVM>
    {
        public int id { get; set; }
        public BankAccountMappingByIdQuery(int id)
        {
            this.id = id;
        }
    }


}
