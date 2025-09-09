using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Accounts.Application.Features.DBOrders.Queries.BudgetComponent
{
    public class GetByIdQuery : IRequest<BudgetComponentVM>
    {
        public int id { get; set; }
        public GetByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
