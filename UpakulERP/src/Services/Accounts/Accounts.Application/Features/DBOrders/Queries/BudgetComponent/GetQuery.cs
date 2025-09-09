using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;

namespace Accounts.Application.Features.DBOrders.Queries.BudgetComponent
{

    public class GetQuery : IRequest<List<CustomSelectListItem>>
    {
        public int pid { get; set; }
        public GetQuery(int? pid)
        {
            this.pid = pid ?? 0;
        }
    }
}
